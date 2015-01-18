#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR} || exit

adb install out/android/myProducts-debug.apk

cd ${DIR}
