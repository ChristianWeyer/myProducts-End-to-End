#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR} || exit

echo "Cleaning up"
## Delete temp directories
rm -rf tmp
rm -rf out
rm -rf phonegap_tmp

## Create temp directories
mkdir tmp
mkdir phonegap_tmp
mkdir out
mkdir out/android
mkdir out/iOS
mkdir out/windows
mkdir out/mac
mkdir out/web

## Copy existing source
cd ${DIR}
cd tmp
cp -r ../../src/MasterDetail.Web/app .
cp -r ../../src/MasterDetail.Web/images .
cp ../../src/MasterDetail.Web/index.html .
cp ../../src/MasterDetail.Web/main.js .
cp ../node-webkit-sharedsource/* .

## Download generated index.html page
#echo GETting index.html
#curl -k https://windows8vm.local/ngmd/ > index.html

## ZIP directory into .nw for node-webkit
zip -qr ../out/app.nw *

## Build for node-webkit
echo Building for Mac
cp -r ../node-webkit-osx/ ../out/mac
cp -r ../out/app.nw ../out/mac/node-webkit.app/Contents/Resources/
mv ../out/mac/node-webkit.app "../out/mac/myProducts.app"

echo Building for Windows
cp -r ../node-webkit-win32/ ../out/windows
cat ../out/windows/nw.exe ../out/app.nw > "../out/windows/myProducts.exe"
rm ../out/windows/nw.exe

rm ../out/app.nw

## Create phonegap project
cd ${DIR}
cd phonegap_tmp
phonegap create myProducts com.tt.ngmd myProducts
rm -rf myProducts/www

## Copy existing application elements
cp -r ../tmp/ myProducts/www
cp -r ../phonegap-sharedsource/ myProducts/

echo Creating PhoneGap projects

cd myProducts

phonegap build ios
phonegap build android

## Build for iOS
cp -r ../../phonegap-ios/ ./platforms/ios/myProducts
cp ./www/config.xml ./platforms/ios/myProducts

echo Building for iOS
phonegap build ios

cd platforms/ios/build/
mv myProducts.app "../../../../../out/ios/myProducts.app"
cd ../../..

## Build Android
cp -r ../../phonegap-android/ ./platforms/android/

echo Building for Android
phonegap build android

cd platforms/android/bin/
cp myProducts-debug.apk ../../../../../out/android/

## Copy for web deployment
cd ${DIR}
cp -r tmp/ out/web/
rm out/web/package.json

## Remove tmp directory
cd ${DIR}
rm -rf tmp
