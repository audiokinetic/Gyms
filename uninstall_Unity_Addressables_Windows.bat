@echo off
set SCRIPT_DIR=%~dp0

if "%~1"=="" (
    set /p unityPath=Enter your Unity installation path:
) else (
    set unityPath=%1
)
start "" %unityPath% -batchmode -projectPath "Unity" -executeMethod AddressableInstaller.UninstallPackage

:: wait until unity is done uninstalling the wwise addressables package
set "processName=Unity.exe"
set "isRunning=false"
:checkProcess
tasklist /fi "ImageName eq %processName%" /fo csv 2> NUL | find /I "%processName%" > NUL
if "%ERRORLEVEL%"=="0" (
    if not "%isRunning%"=="true" (
        echo Uninstalling the Wwise Addressables package...
        set "isRunning=true"
    )

    timeout /t 1 /nobreak > nul
    goto checkProcess
) else (
    if "%isRunning%"=="true" (
        echo The Wwise Addressable package was successfully removed.

    ) else (
        echo An error hapen and %processName% was not running.
    )
)

:: Generate the SoundBanks
"%WWISEROOT%\Authoring\x64\Release\bin\WwiseConsole.exe" generate-soundbank "%SCRIPT_DIR%WwiseProject/Gyms.wproj"
pause