#!/bin/sh

rm -r "$2/client" > /dev/nul 2>&1
mkdir "$2/client"

rsync -auv "$1/myProducts.Web/client/app/" "$2/client/app/"
rsync -auv "$1/myProducts.Web/client/appServices/" "$2/client/appServices/"
rsync -auv "$1/myProducts.Web/client/appStartup/" "$2/client/appStartup/"
rsync -auv "$1/myProducts.Web/client/assets/" "$2/client/assets/"
rsync -auv "$1/myProducts.Web/client/libs/" "$2/client/libs/"
rsync -auv "$1/myProducts.Web/client/translations/" "$2/client/translations/"
rsync -auv "$1/myProducts.Web/client/cordova.js" "$2/client/cordova.js"
rsync -auv "$1/myProducts.Web/images/" "$2/images/"

curl -k https://windows8vm.local/ngmd/client/#/ > "$2/client/index.html"
perl -pi -w -e 's/\/ngmd\/client\///g;' "$2/client/index.html"
