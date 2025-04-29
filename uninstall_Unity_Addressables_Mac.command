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

# Uninstall addressables
processName="Unity"
isRunning=false
"$unityPath" -batchmode --args -projectPath "Unity" -executeMethod "AddressableInstaller.UninstallPackage" & unity_pid=$!

checkProcess() {
  if pgrep -x "$processName" > /dev/null; then
    if [ "$isRunning" == "false" ]; then
      echo "Uninstalling the Wwise Addressables package..."
      isRunning=true
    fi
    sleep 1
    checkProcess
  else
    if [ "$isRunning" == "true" ]; then
      echo "The Wwise Addressable package was successfully removed."
    else
      echo "An error happened and $processName was not running."
    fi
  fi
}

checkProcess

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

read -p "Press Enter to continue..."