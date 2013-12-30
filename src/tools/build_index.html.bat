curl -k https://localhost/ngmd/app > index.html

ssr -f index.html -a -s "/ngmd/" -r ""

copy index.html ..\myProducts.WindowsClient\client
move index.html ..\myProducts.SelfHost\client

del *.bak
