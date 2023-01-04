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
taskkill /f /im Emulator.exe

REM Get latest 
REM ##############################################
set currentpath=%cd%
cd ..
set toolspath="C:\BrowserFramework\Tools\TestRunner"
cd ..
set commonpath="C:\BrowserFramework\Products\Common"
set productpath="C:\BrowserFramework\Products\MaconomyTouch"

cd %toolspath%
start /wait SourceControl.bat get /recursive /overwrite %toolspath%
start /wait SourceControl.bat get /recursive /overwrite %commonpath%
start /wait SourceControl.bat get /recursive /overwrite %productpath%

REM Start schedulingagent 
REM ##############################################
start TestIT.exe TEST_FOLDER "C:\BrowserFramework\Products\MaconomyTouch\Tests\UnitTest" marlynyonaha@deltek.com;annemarquez@deltek.com;JanCuevas@deltek.com;JuanPaoloValle@deltek.com

exit