#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR} || exit
cd phonegap_tmp/myProducts

cordova run android

cd ${DIR}
