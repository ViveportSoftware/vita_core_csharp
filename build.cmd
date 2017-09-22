@echo off

rem External Tools: powershell (from PowerShell)
set POWERSHELL=powershell

rem Detect powershell
%POWERSHELL% /? 1>nul 2>&1
if errorlevel 1 (
   echo %POWERSHELL%: NOT FOUND
   exit /b 1
)
echo %POWERSHELL%: Found in path

rem %POWERSHELL% -ExecutionPolicy Unrestricted -File .\build.ps1 -Configuration Release -Target Publish-NuGet-Package -ScriptArgs '--revision="100"'
%POWERSHELL% -ExecutionPolicy Unrestricted -File .\build.ps1