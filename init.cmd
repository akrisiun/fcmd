
@REM git clone https://github.com/akrisiun/fcmd.git fcmd
@REM cd fcmd

git pull

if exist "SharpSSH\SharpSSH.sln"  goto next1
git submodule init
git submodule update

:next1

@REM if exist "xwt"  goto next2
@REM mkdir xwt
@REM git clone https://github.com/akrisiun/xwt.git xwt

:next2