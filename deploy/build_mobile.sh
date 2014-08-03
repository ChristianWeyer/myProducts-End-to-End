#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR} || exit

echo "Pulling from GitHub"
## Get latest from GitHub
cd ..
git pull origin master
cd ${DIR}

echo "Cleaning up"
## Delete temp directories
rm -rf tmp
rm -rf out_mobile
rm -rf phonegap_mobile_tmp

## Create temp directories
mkdir tmp
mkdir phonegap_mobile_tmp
mkdir out_mobile
mkdir out_mobile/android
mkdir out_mobile/iOS

## Copy existing source
cd ${DIR}
cd tmp
cp -r ../../src/myProducts.Web/app .
cp -r ../../src/myProducts.Web/mobile .
cp -r ../../src/myProducts.Web/libs .
cp -r ../../src/myProducts.Web/assets .

cp ../node-webkit-sharedsource/* .

## Download generated index.html page
echo "GETting index.html"
curl -k https://windows8vm.local/ngmd/mobile > index.html
perl -pi -w -e 's/\/ngmd\///g;' index.html

## Create phonegap project
cd ${DIR}
cd phonegap_mobile_tmp
cordova create myProducts com.tt.ngmd myProducts
rm -rf myProducts/www

## Copy existing application elements
cp -r ../tmp/ myProducts/www
cp -r ../phonegap-sharedsource/ myProducts/

echo "Creating PhoneGap projects"

cd myProducts

cordova platform add ios
cordova platform add android

cordova plugin add org.apache.cordova.device
cordova plugin add org.apache.cordova.geolocation
cordova plugin add org.apache.cordova.splashscreen
cordova plugin add org.apache.cordova.statusbar
cordova plugin add org.apache.cordova.console

## Build for iOS
cp -r ../../phonegap-ios/ ./platforms/ios/myProducts
cp ./www/config.xml ./platforms/ios/myProducts

echo "Building for iOS"
cordova build ios

cd platforms/ios/build/emulator/
mv myProducts.app "../../../../../../out_mobile/ios/myProducts.app"
cd ../../../..

## Build Android
cp -r ../../phonegap-android/ ./platforms/android/

echo "Building for Android"
cordova build android

cd platforms/android/ant-build/
cp myProducts-debug.apk ../../../../../out_mobile/android/

## Remove tmp directory
cd ${DIR}
rm -rf tmp
