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
rm -rf out
rm -rf cordova_tmp

## Create temp directories
mkdir tmp
mkdir cordova_tmp
mkdir out
mkdir out/android
mkdir out/iOS
mkdir out/windows
mkdir out/mac
mkdir out/web

## Copy existing source
cd ${DIR}
cd tmp
cp -r ../../src/myProducts.Web/client/app .
cp -r ../../src/myProducts.Web/client/appStartup .
cp -r ../../src/myProducts.Web/client/appServices .
cp -r ../../src/myProducts.Web/client/libs .
cp -r ../../src/myProducts.Web/client/assets .

cp ../node-webkit-sharedsource/* .

## Download generated index.html page
echo "GETting index.html"
curl -k https://windows8vm.local/ngmd/client/#/ > ./index.html
perl -pi -w -e 's/\/ngmd\/client\///g;' ./index.html

## ZIP directory into .nw for node-webkit
zip -qr ../out/app.nw *

## Build for node-webkit
echo "Building for Mac"
cp -r ../node-webkit-osx/ ../out/mac
cp -r ../out/app.nw ../out/mac/node-webkit.app/Contents/Resources/
mv ../out/mac/node-webkit.app "../out/mac/myProducts.app"

echo "Building for Windows"
cp -r ../node-webkit-win/ ../out/windows
cat ../out/windows/nw.exe ../out/app.nw > "../out/windows/myProducts.exe"
rm ../out/windows/nw.exe
rm ../out/app.nw

## Create Cordova project
cd ${DIR}
cd cordova_tmp
#cordova create myProducts com.tt.apps.ngmd myProducts
ionic start -a myProducts -i com.tt.ngmd myProducts blank
rm -rf myProducts/www

## Copy existing application elements
cp -r ../tmp/ myProducts/www
cp -r ../cordova-sharedsource/ myProducts/

echo "Creating Cordova projects"

cd myProducts

#cordova platform add ios
#cordova platform add android

ionic platform add ios
ionic platform add android
ionic browser add crosswalk

cordova plugin add org.apache.cordova.device
cordova plugin add org.apache.cordova.geolocation
cordova plugin add org.apache.cordova.splashscreen
cordova plugin add org.apache.cordova.statusbar
cordova plugin add org.apache.cordova.console

## Build for iOS
cp -r ../../cordova-ios/ ./platforms/ios

echo "Building for iOS"
#cordova build ios
ionic build ios

cd platforms/ios/build/emulator/
mv myProducts.app "../../../../../../out/ios/myProducts.app"
cd ../../../..

echo "Building for Android"

cp -r ../../cordova-android/ ./platforms/android
#cordova build android
ionic build android

cd platforms/android/build/outputs/apk
cp *.apk ../../../../../../../out/android/

## Copy for web deployment
cd ${DIR}
cp -r tmp/ out/web/
rm out/web/package.json

## Remove tmp directory
cd ${DIR}
rm -rf tmp
