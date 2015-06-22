@ECHO OFF
SET sn="c:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\sn.exe"
%sn% -p xwt.snk xwt.key
cat xwt.key
%sn% -tp xwt.key

@PAUSE
