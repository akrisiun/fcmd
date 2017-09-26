
@REM MSTools v14.0

set msbuild="%ProgramFiles(x86)%\MSBuild\15.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" 
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"

cd xwt\
%msbuild% Xwt-Gtk3.sln "/v:m"

cd ..\
%msbuild% fcmd.sln "/v:m"

@PAUSE