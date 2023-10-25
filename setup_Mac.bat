@echo off
set SCRIPT_DIR=%~dp0

:: Generate the WAV files
python WwiseProject/GenerateProjectWavFiles.py

:: Generate the supported Gyms
python GenerateSupportedGyms.py