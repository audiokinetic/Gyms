@echo off
set SCRIPT_DIR=%~dp0

:: Generate the WAV files
python WwiseProject/GenerateProjectWavFiles.py

:: Generate the supported Gyms
python GenerateSupportedGyms.py

:: Generate the SoundBanks
"%WWISEROOT%\Authoring\x64\Release\bin\WwiseConsole.exe" generate-soundbank "%SCRIPT_DIR%WwiseProject/Gyms.wproj"
pause