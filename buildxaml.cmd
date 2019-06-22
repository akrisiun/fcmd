@REM MSTools v15.0 (VS 2017)

@REM @if not exist %msbuild% @set msbuild="%ProgramFiles% (x86)\MSBuild\14.0\Bin\MSBuild.exe" 
@set msbuild="c:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
@if not exist %msbuild%  set msbuild="%ProgramFiles% (x86)\MSBuild\15.0\Bin\MSBuild.exe"

@echo %msbuild% fcmd-xaml.sln %* /verbosity:m
%msbuild% fcmd-xaml.sln %* /verbosity:m
