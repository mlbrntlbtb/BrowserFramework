REM Stop running related executables
REM ##############################################
taskkill /f /im AutomationAgent.exe
taskkill /f /im DocDiff.exe
taskkill /f /im scheduler.exe
taskkill /f /im SchedulingAgent.exe
taskkill /f /im TestLibrary.exe
taskkill /f /im TestRunner.exe
taskkill /f /im TestRunnerCmd.exe
taskkill /f /im TestRunnerScheduler.exe

REM Get latest 
REM ##############################################
set currentpath=%cd%
cd ..
set toolspath="%cd%"
cd ..
set pathname=%cd%
set commonpath="%pathname%\Products\Common"
set productname=%1
set productpath="%pathname%\Products\%productname%"

cd %currentpath%
start /wait SourceControl.bat get /recursive /overwrite %toolspath%
start /wait SourceControl.bat get /recursive /overwrite %commonpath%
start /wait SourceControl.bat get /recursive /overwrite %productpath%

REM Start schedulingagent 
REM ##############################################
start SchedulingAgent.exe false

exit