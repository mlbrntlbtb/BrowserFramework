@ECHO OFF
SETLOCAL

:: get parent folder from tools\testrunner
for %%i in ("%~dp0..") do set "parent=%%~di%%~pi"

:: set global variables
set EXE=%parent%Products\Common\Scheduler\AllowRemoteAccess.dat
SET AGENT="AutomationAgent.exe"
SET FILEEXISTING=SAMPLE
SET PROCESSRUNNING=SAMPLE

:: The "main" logic of the script
CALL :ISFILEEXISTING
CALL :ISPROCESSRUNNING

IF %FILEEXISTING% == TRUE ( IF %PROCESSRUNNING% == FALSE ( CALL :STARTPROCESS )) ELSE ( IF %PROCESSRUNNING% == TRUE ( CALL :KILLPROCESS )) 

:: force execution to quit at the end of the "main" logic
EXIT /B %ERRORLEVEL%

:ISPROCESSRUNNING
TASKLIST /nh /fi "imagename eq %AGENT%" | find /i %AGENT% > nul && ( SET PROCESSRUNNING=TRUE ) || ( SET PROCESSRUNNING=FALSE )
GOTO :EOF

:ISFILEEXISTING
IF EXIST %EXE% ( SET FILEEXISTING=TRUE ) ELSE ( SET FILEEXISTING=FALSE )
GOTO :EOF 

:STARTPROCESS
START %parent%\Tools\TestRunner\%AGENT%
GOTO :EOF

:KILLPROCESS
TASKKILL /f /im %AGENT%
GOTO :EOF

:EOF
