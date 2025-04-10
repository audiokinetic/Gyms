@echo off
set SCRIPT_DIR=%~dp0

:: Generate the WAV files
python WwiseProject/GenerateProjectWavFiles.py

:: Generate the supported Gyms
python GenerateSupportedGyms.py

:: Generate the SoundBanks
"%WWISEROOT%\Authoring\x64\Release\bin\WwiseConsole.exe" generate-soundbank "%SCRIPT_DIR%WwiseProject/Gyms.wproj"

:: Set the language for the Unity Project
python Unity/Build/SetStartupLanguage.py

if "%~1"=="" (
    set /p unityPath=Enter your Unity installation path:
) else (
    set unityPath=%1
)
start "" %unityPath% -batchmode -projectPath "Unity" -executeMethod AddressableInstaller.AddressableSetup

:: wait until unity is done setting up addressables
set "processName=Unity.exe"
set "isRunning=false"
:checkProcess
tasklist /fi "ImageName eq %processName%" /fo csv 2> NUL | find /I "%processName%" > NUL
if "%ERRORLEVEL%"=="0" (
    if not "%isRunning%"=="true" (
        echo Setting up Unity Addressables
        set "isRunning=true"
    )

    timeout /t 1 /nobreak > nul
    goto checkProcess
) else (
    if "%isRunning%"=="true" (
        echo Addressables setup was completed successfully.

    ) else (
        echo An error hapen and %processName% was not running.
    )
)
pause