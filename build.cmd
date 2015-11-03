git pull 

call msbuild fcmd-xaml.sln "/v:m"
call msbuild fcmd.sln "/v:m"

@REM cd xwt\
call msbuild fcmd-Mac.sln "/v:m"
@REM cd ..\

@PAUSE