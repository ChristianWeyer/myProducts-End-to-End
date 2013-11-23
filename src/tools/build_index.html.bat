curl -k https://localhost/ngmd/ > index.html

ssr -f index.html -a -s "/ngmd/" -r "/"

move index.html ..\myProducts.SelfHost\client

del *.bak
