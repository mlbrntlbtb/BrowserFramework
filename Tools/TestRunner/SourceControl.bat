REM USAGE
REM ##############################################
REM %1 --> Command (add, delete, checkout, checkin)
REM %2 --> Options (/noprompt, /comments, etc)
REM %3 --> File
REM ##############################################
IF EXIST "%VS120COMNTOOLS%..\IDE\TF.exe" (goto VS2013)
IF EXIST "%VS100COMNTOOLS%..\IDE\TF.exe" (goto VS2010)
IF EXIST "%TF2017%\TF.exe" (goto VS2017)
:VS2010
SET myPath="%VS100COMNTOOLS%..\IDE\TF.exe"
goto MAIN
:VS2013
SET myPath="%VS120COMNTOOLS%..\IDE\TF.exe"
goto MAIN
:VS2017
SET myPath="%TF2017%\TF.exe"
:MAIN
%myPath% %1 /noprompt %2 %3 %4 > SourceControl.log
exit
