curl -k https://localhost/ngmd/client/ > index.html

ssr -f index.html -a -s "/ngmd/client/" -r ""

copy index.html ..\build\client
move index.html ..\ClientBuild\client

del *.bak
