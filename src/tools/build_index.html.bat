curl -k https://localhost/ngmd/client/app/ > index.html

ssr -f index.html -a -s "/ngmd/client/" -r "../"

copy index.html ..\build\client\app
move index.html ..\ClientBuild\client\app

del *.bak
