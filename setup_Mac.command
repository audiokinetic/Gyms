#!/bin/bash

# Set the SCRIPT_DIR variable to the directory of the script
SCRIPT_DIR="$(dirname "$0")"

# Navigate to the script directory
cd "$SCRIPT_DIR"

# Generate the WAV files
python3 WwiseProject/GenerateProjectWavFiles.py

# Generate the supported Gyms
python3 GenerateSupportedGyms.py

# Set the language for the Unity Project
python Unity/Build/SetStartupLanguage.py