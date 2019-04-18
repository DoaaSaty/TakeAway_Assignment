@echo off
set current_path=%~dp0
set current_path=%current_path:\=/%
echo %current_path%

rem echo >> %current_path%automation.conf
echo AutomationDirectory=%current_path% > %current_path%automation.conf.dir

set test_dll_path=Automated.Tests/bin/Debug/Automated.Tests.dll
set test_dll_path=%current_path%%test_dll_path%
echo %test_dll_path%

set TestAutomationOutput=Test.Automation.Output
if not exist %TestAutomationOutput% mkdir %TestAutomationOutput%

set %TestAutomationOutput% = %current_path%%TestAutomationOutput%
echo %TestAutomationOutput%
cd %TestAutomationOutput%
if not exist Logs mkdir Logs
if not exist Reports mkdir Reports

set reports_directory=%current_path%%TestAutomationOutput%/Reports/
echo %reports_directory%
REM rem set reports_directory=%current_path%%reports%

cd %reports_directory%
for /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
for /f "tokens=1-2 delims=/:" %%a in ('time /t') do (set mytime=%%a-%%b)
set ReportsFolder=%mydate%-%mytime%
set ReportsFolder=%ReportsFolder: =%

if not exist %ReportsFolder% mkdir %ReportsFolder%
set ReportingDir=%reports_directory%%ReportsFolder%

set test_csproj_path=Automated.Tests/Automated.Tests.csproj
set test_csproj_path=%current_path%%test_csproj_path%

rem --moving up one level
echo %cd%
cd ../..
echo %cd%

set nunit_path=packages/NUnit.ConsoleRunner.3.8.0/tools
set nunit_path=%current_path%%nunit_path%
cd %nunit_path%
nunit3-console.exe %test_dll_path% --labels=All --out=TestResult.txt --work=%ReportingDir%

REM set specflow_path=packages/SpecFlow.2.3.1/tools
REM set specflow_path=%current_path%%specflow_path%
REM cd %specflow_path%
REM specflow.exe nunitexecutionreport %test_csproj_path% /out:%ReportingDir%/TestResult.html /xmlTestResult:%ReportingDir%/TestResult.xml /testOutput:%ReportingDir%/TestResult.txt

Pause
