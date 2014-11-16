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
cp -r ../../src/myProducts.Web/app .
cp -r ../../src/myProducts.Web/appStartup .
cp -r ../../src/myProducts.Web/appServices .
cp -r ../../src/myProducts.Web/libs .
cp -r ../../src/myProducts.Web/assets .

cp ../node-webkit-sharedsource/* .

## Download generated index.html page
echo "GETting index.html"
curl -k https://windows8vm.local/ngmd/app > index.html
perl -pi -w -e 's/\/ngmd\///g;' index.html

## ZIP directory into .nw for node-webkit
zip -qr ../out/app.nw *

## Build for node-webkit
echo "Building for Mac"
cp -r ../node-webkit-osx/ ../out/mac
cp -r ../out/app.nw ../out/mac/node-webkit.app/Contents/Resources/
mv ../out/mac/node-webkit.app "../out/mac/myProducts.app"

echo "Building for Windows"
cp -r ../node-webkit-win32/ ../out/windows
cat ../out/windows/nw.exe ../out/app.nw > "../out/windows/myProducts.exe"
rm ../out/windows/nw.exe
rm ../out/app.nw

## Create Cordova project
cd ${DIR}
cd cordova_tmp
cordova create myProducts com.tt.apps.ngmd myProducts
rm -rf myProducts/www

## Copy existing application elements
cp -r ../tmp/ myProducts/www
cp -r ../cordova-sharedsource/ myProducts/

echo "Creating Cordova projects"

cd myProducts

cordova platform add ios
cordova platform add android

cordova plugin add org.apache.cordova.device
cordova plugin add org.apache.cordova.geolocation
cordova plugin add org.apache.cordova.splashscreen
cordova plugin add org.apache.cordova.statusbar
cordova plugin add org.apache.cordova.console

## Build for iOS
cp -r ../../cordova-ios/ ./platforms/ios
cp ./www/config.xml ./platforms/ios/myProducts

echo "Building for iOS"
cordova build ios

cd platforms/ios/build/emulator/
mv myProducts.app "../../../../../../out/ios/myProducts.app"
cd ../../../..

echo "Building for Android"

# Tweak Android to use Crosswalk
rm -Rf platforms/android/CordovaLib/*
cp -a ../../cordova-android/crosswalk-cordova/framework/* \
    platforms/android/CordovaLib/
cp -a ../../cordova-android/crosswalk-cordova/VERSION platforms/android/

export ANDROID_HOME=$(dirname $(dirname $(which android)))
cd platforms/android/CordovaLib/
android update project --subprojects --path . \
    --target "android-19"
ant debug
cd ../../..

## Finally build Android
cp -r ../../cordova-android/ ./platforms/android
cp ./www/config.xml ./platforms/android
cordova build android

cd platforms/android/ant-build/
cp myProducts-debug.apk ../../../../../out/android/

## Copy for web deployment
cd ${DIR}
cp -r tmp/ out/web/
rm out/web/package.json

## Remove tmp directory
cd ${DIR}
rm -rf tmp
