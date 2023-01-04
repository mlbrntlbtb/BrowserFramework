@ECHO OFF
REM ########### Initialize ###########
SET LogDir=C:\TFS\QEAutomation\Selenium\BrowserFramework\Code\BrowserFramework\BuildScripts\BuildLogs
IF NOT ExIST %LogDir% mkdir %LogDir%
SET formattedTime=%time: =0%
SET BuildStart=%date:~-4,4%%date:~-10,2%%date:~-7,2%_%formattedTime:~-11,2%%formattedTime:~-8,2%
SET Log=%LogDir%\log_automatedTRBuild_%BuildStart%.txt
echo ************* Initializing build ************* | wtee -a %Log%
echo Current time is %BuildStart% | wtee -a %Log%
echo.
SET TRBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv"
SET TRTfs="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\TF"
SET TRsrcDir=C:\TFS\QEAutomation\Selenium\BrowserFramework\Code\BrowserFramework
SET TRbinDir=C:\TFS\QEAutomation\Selenium\BrowserFramework\Code\BrowserFramework\TestRunner\bin\Debug
SET TRbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\TestRunner.exe
SET TSbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\TestRunnerScheduler.exe
SET CLbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\CommonLib.dll
SET AJbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\AjeraLib.dll
SET CPbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\CostpointLib.dll
SET GWbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\GovWinLib.dll
SET HSbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\HRSmartLib.dll
SET MNbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\MaconomyNavigatorLib.dll
SET MIbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\MaconomyiAccessLib.dll
SET MTbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\MaconomyTouchLib.dll
SET NGbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\ngCRMLib.dll
SET VCbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\VisionCRMLib.dll
SET VTbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\VisionTimeLib.dll
SET ATbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\AjeraTimeLib.dll
SET TLbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\TrafficLiveLib.dll
SET STCRMbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\StormTouchCRMLib.dll
SET STTEbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\StormTouchTimeExpenseLib.dll
SET STWEBbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\StormWebLib.dll
SET PIMbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\ProjectInformationManagementLib.dll
SET BPMbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\BPMLib.dll
SET PIMMbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\PIMMobileLib.dll
SET SFTbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\SFTLib.dll
SET WBbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\WorkBookLib.dll
SET PIMTEbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\PIMTimeAndExpenseLib.dll
SET ACTSbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\AcumenTouchStoneLib.dll
SET TEMbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\TEMobileLib.dll
SET CSbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\ConceptShareLib.dll
SET CBIbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\CBILib.dll
SET CPTbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\CPTouchLib.dll
SET SBCbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\SBCLib.dll
SET WTCbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\WebTimeClockLib.dll
SET DCObin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\DCOLib.dll
SET FEbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\FieldEaseLib.dll
SET DPTbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\DeltekProjectsToolLib.dll
SET KPbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\KnowledgePointLib.dll
SET HTMLbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\HTMLBuilder.dll
SET OSHbin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\OSH_Client.dll
SET TRDCorebin=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\TRDiagnosticsCore.dll
SET TRToolsDir=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner
SET AutoMail=C:\TFS\QEAutomation\Selenium\Costpoint\7.1\Tools\AutoMail.exe
SET Init=C:\TFS\QEAutomation\Selenium\BrowserFramework\Tools\TestRunner\Init.dat

@ECHO OFF
REM ########### Initialize ###########

IF /I "%1"=="" SET Desc=Manual
IF /I "%1"=="-a" SET Desc=Automated

REM ########### Get Latest framework files ###########
echo ******** Getting latest files from TFS ******* | wtee -a %Log%
%TRTfs% get %TRsrcDir% /recursive | wtee -a %Log%
REM ########### Get Latest framework files ###########

REM ########### Build ###########
@ECHO ON
echo.
echo ***** Building Test Runner executable and dependencies ***** | wtee -a %Log%
%TRBuild% %TRsrcDir%\TestRunner\TestRunner.csproj /rebuild Debug /Project %TRsrcDir%\TestRunner\TestRunner.csproj | wtee -a %Log%
echo.
@ECHO OFF
REM ########### Build ###########

REM ########### Update assemblies ###########
echo.
echo ***** Updating framework assemblies **** | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %TRbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %TSbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %CLbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %AJbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %CPbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %GWbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %HSbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %MNbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %MIbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %MTbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %NGbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %VCbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %VTbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %ATbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %TLbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %STCRMbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %STTEbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %STWEBbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %PIMbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %BPMbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %PIMMbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %SFTbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %WBbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %PIMTEbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %ACTSbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %TEMbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %CSbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %CBIbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %CPTbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %SBCbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %WTCbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %DCObin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %FEbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %DPTbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %KPbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %HTMLbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %OSHbin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %TRDCorebin% | wtee -a %Log%
%TRTfs% checkout /lock:checkout /recursive %Init% | wtee -a %Log%
copy /b /y %TRbinDir%\TestRunner.exe %TRbin%
copy /b /y %TRbinDir%\TestRunnerScheduler.exe %TSbin%
copy /b /y %TRbinDir%\CommonLib.dll %CLbin%
copy /b /y %TRbinDir%\AjeraLib.dll %AJbin%
copy /b /y %TRbinDir%\CostpointLib.dll %CPbin%
copy /b /y %TRbinDir%\GovWinLib.dll %GWbin%
copy /b /y %TRbinDir%\HRSmartLib.dll %HSbin%
copy /b /y %TRbinDir%\MaconomyNavigatorLib.dll %MNbin%
copy /b /y %TRbinDir%\MaconomyTouchLib.dll %MTbin%
copy /b /y %TRbinDir%\MaconomyiAccessLib.dll %MIbin%
copy /b /y %TRbinDir%\ngCRMLib.dll %NGbin%
copy /b /y %TRbinDir%\VisionCRMLib.dll %VCbin%
copy /b /y %TRbinDir%\VisionTimeLib.dll %VTbin%
copy /b /y %TRbinDir%\AjeraTimeLib.dll %ATbin%
copy /b /y %TRbinDir%\TrafficLiveLib.dll %TLbin%
copy /b /y %TRbinDir%\StormTouchCRMLib.dll %STCRMbin%
copy /b /y %TRbinDir%\StormTouchTimeExpenseLib.dll %STTEbin%
copy /b /y %TRbinDir%\StormWebLib.dll %STWEBbin%
copy /b /y %TRbinDir%\ProjectInformationManagementLib.dll %PIMbin%
copy /b /y %TRbinDir%\BPMLib.dll %BPMbin%
copy /b /y %TRbinDir%\PIMMobileLib.dll %PIMMbin%
copy /b /y %TRbinDir%\SFTLib.dll %SFTbin%
copy /b /y %TRbinDir%\WorkBookLib.dll %WBbin%
copy /b /y %TRbinDir%\PIMTimeAndExpenseLib.dll %PIMTEbin%
copy /b /y %TRbinDir%\AcumenTouchStoneLib.dll %ACTSbin%
copy /b /y %TRbinDir%\TEMobileLib.dll %TEMbin%
copy /b /y %TRbinDir%\ConceptShareLib.dll %CSbin%
copy /b /y %TRbinDir%\CBILib.dll %CBIbin%
copy /b /y %TRbinDir%\CPTouchLib.dll %CPTbin%
copy /b /y %TRbinDir%\SBCLib.dll %SBCbin%
copy /b /y %TRbinDir%\WebTimeClockLib.dll %WTCbin%
copy /b /y %TRbinDir%\DCOLib.dll %DCObin%
copy /b /y %TRbinDir%\FieldEaseLib.dll %FEbin%
copy /b /y %TRbinDir%\DeltekProjectsToolLib.dll %DPTbin%
copy /b /y %TRbinDir%\KnowledgePointLib.dll %KPbin%
copy /b /y %TRbinDir%\HTMLBuilder.dll %HTMLbin%
copy /b /y %TRbinDir%\OSH_Client.dll %OSHbin%
copy /b /y %TRbinDir%\TRDiagnosticsCore.dll %TRDCorebin%
copy /b /y %TRbinDir%\Init.dat %Init%
%TRTfs% checkin /recursive /noprompt /override:("Automated build") /comment:"Automated build" %TRToolsDir% | wtee -a %Log%
REM ########### Update assemblies ###########

REM ########### Emailing Report ###########
echo.
echo ******** Emailing report ******* | wtee -a %Log%
HTMLReportBuilder %Log% %Desc%" Build: Test Runner" MakatiAutomation@deltek.com InnaReyzin@deltek.com DominickPalmisano@deltek.com LorraineHari@deltek.com KrystalJSuarez@deltek.com PaulaJeanMarieGarcia@deltek.com
%AutoMail% /F %LogDir%\Email.xml
echo.
echo ***** Build was successfully executed! **** | wtee -a %Log%
:END