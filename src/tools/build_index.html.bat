curl -k https://localhost/ngmd/app/ > index.html

ssr -f index.html -a -s "/ngmd/" -r "../"

copy index.html ..\build\client\app
move index.html ..\ClientBuild\client\app

del *.bak
