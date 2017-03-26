@REM MSTools v15.0 (VS 2017)

set msbuild="%ProgramFiles%\MSBuild\15.0\Bin\MSBuild.exe"
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\14.0\Bin\MSBuild.exe" 
@if not exist %msbuild% @set msbuild="%ProgramFiles%\MSBuild\12.0\Bin\MSBuild.exe"

%msbuild% fcmd-xaml.sln "/v:m"

@PAUSE