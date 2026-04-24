@echo off
set SCRIPT_DIR=%~dp0

:start
if "%~1"=="" (
    set /p engine=Use Unreal or Unity:
) else (
    set engine=%1
)

setlocal enabledelayedexpansion

if /i "%engine%"=="Unity" (
	if "%~2"=="" (
        set /p unityPathComma="Enter your Unity Installation root path (e.g. C:/Program Files/Unity/Hub/Editor/Unity_6000.0.64f1):"
	) else (
	    set unityPathComma=%2
	)
	set "unityPath=!unityPathComma:"=!\Editor\Unity.exe"
) else ( 
	if /i "%engine%"=="Unreal" (
		if "%~2"=="" (
		    set /p unrealPathComma="Enter your Unreal Installation root path (e.g. C:/Program Files/Epic Games/UE_5.5):"
        ) else (
	        set unrealPathComma=%2
	    )
		set "unrealPath=!unrealPathComma:"=!"
	) else (
		echo Invalid Engine. Expected: Unreal or Unity
		goto :start
	)
)

if NOT "%unityPath%"=="" (
	echo %unityPath%
    echo Running Unity in batch mode to resolve packages. This may take a moment.
    "%unityPath%" -batchmode -resolvePackages -projectPath "Unity" -quit
)

if NOT "%unrealPath%"=="" (
    echo Running Unreal Build Tool. This may take a moment.
	"%unrealPath%\Engine\Binaries\DotNET\UnrealBuildTool\UnrealBuildTool.exe" GymsEditor Win64 Development -project=%CD%\Unreal\Gyms.uproject
)

:: Generate the WAV files
python WwiseProject/GenerateProjectWavFiles.py

:: Generate the supported Gyms
python GenerateSupportedGyms.py

:: Generate the SoundBanks
"%WWISEROOT%\Authoring\x64\Release\bin\WwiseConsole.exe" generate-soundbank "%SCRIPT_DIR%WwiseProject/Gyms.wproj"

if NOT "%unityPath%"=="" (
    :: Set the language for the Unity Project
    python Unity/Build/SetStartupLanguage.py

    start "" "%unityPath%" -batchmode -projectPath "Unity" -executeMethod AddressableInstaller.AddressableSetup

    :: wait until unity is done setting up addressables
    set "processName=Unity.exe"
    set "isRunning=false"
    :checkProcess
    tasklist /fi "ImageName eq !processName!" /fo csv 2> NUL | find /I "!processName!" > NUL
    if "%ERRORLEVEL%"=="0" (
        if not "!isRunning!"=="true" (
            echo Setting up Unity Addressables
            set "isRunning=true"
        )

        timeout /t 1 /nobreak > nul
        goto checkProcess
    ) else (
        if "!isRunning!"=="true" (
            echo Addressables setup was completed successfully.

        ) else (
            echo An error occurred and prevented !processName! from running.
        )
    )
)
:commonexit
pause