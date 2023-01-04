REM USAGE
REM ##############################################
REM %1 --> Command (add, delete, checkout, checkin)
REM %2 --> Options (/noprompt, /comments, etc)
REM %3 --> File
REM ##############################################
IF EXIST "%VS120COMNTOOLS%..\IDE\TF.exe" (SET myPath="%VS120COMNTOOLS%..\IDE\TF.exe") ELSE (SET myPath="%VS100COMNTOOLS%..\IDE\TF.exe")
%myPath% %1 /noprompt %2 %3 %4 > SourceControl.log
exit
