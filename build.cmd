git pull 

cd xwt\
call msbuild Xwt-Gtk3.sln "/v:m"

cd ..\
call msbuild fcmd.sln "/v:m"

@PAUSE