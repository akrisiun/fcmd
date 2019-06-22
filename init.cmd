
@REM git clone https://github.com/akrisiun/fcmd.git fcmd
@REM cd fcmd

if exist "SharpSSH"  goto next1
mkdir SharpSSH
git clone https://github.com/akrisiun/SharpSSH.git SharpSSH

:next1

@REM if exist "xwt"  goto next2
@REM mkdir xwt
@REM git clone https://github.com/akrisiun/xwt.git xwt
:next2