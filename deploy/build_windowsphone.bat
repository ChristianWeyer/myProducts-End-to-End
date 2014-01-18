@echo off

echo NOTE: has to be called in Windows after ./build.sh on Unix
echo Please wait ... compiling for WP8

cd phonegap_tmp
cd myProducts
call cordova platform add wp8

:: xcopy /e /s ..\..\phonegap-wp8\*.* platforms\wp8

call cordova build wp8

cd ..\..