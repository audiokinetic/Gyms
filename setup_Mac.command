#!/bin/bash

# Set the SCRIPT_DIR variable to the directory of the script
SCRIPT_DIR="$(dirname "$0")"

# Navigate to the script directory
cd "$SCRIPT_DIR"

# Get the Unity installation path
if [ -z "$1" ]; then
  read -p "Enter your Unity installation path: " unityPath
else
  unityPath="$1"
fi

echo Running Unity in batch mode to resolve packages. This may take a moment
"$unityPath" -batchmode -resolvePackages -projectPath "Unity" -quit

# Generate the WAV files
python3 WwiseProject/GenerateProjectWavFiles.py

# Generate the supported Gyms
python3 GenerateSupportedGyms.py

# Set the language for the Unity Project
python3 Unity/Build/SetStartupLanguage.py

# Generate Banks
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"

xml_file="$SCRIPT_DIR/Unity/Assets/WwiseSettings.xml"

# Check if the XML file exists
if [ ! -f "$xml_file" ]; then
  echo "Error: XML file '$xml_file' not found."
  exit 1
fi

WwiseInstalationPathMac=$(xmllint --xpath 'string(//WwiseInstallationPathMac)' "$xml_file")

if [ -z "$WwiseInstalationPathMac" ]; then
  echo "Error: WwiseInstalationPathMac is not defined in the WwiseSettings.xml."
else
  "$WwiseInstalationPathMac/Contents/Tools/WwiseConsole.sh" generate-soundbank "$SCRIPT_DIR/WwiseProject/Gyms.wproj"
fi

# Setup addressables
processName="Unity"
isRunning=false
"$unityPath" -batchmode --args -projectPath "Unity" -executeMethod "AddressableInstaller.AddressableSetup" & unity_pid=$!

checkProcess() {
  if pgrep -x "$processName" > /dev/null; then
    if [ "$isRunning" == "false" ]; then
      echo "Setting up Addressables..."
      isRunning=true
    fi
    sleep 1
    checkProcess
  else
    if [ "$isRunning" == "true" ]; then
      echo "Successfully completed the operation."
    else
      echo "An error occurred and prevented $processName from running."
    fi
  fi
}

checkProcess

read -p "Press Enter to continue..."