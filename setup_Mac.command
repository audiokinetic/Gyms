#!/bin/bash

# Set the SCRIPT_DIR variable to the directory of the script
SCRIPT_DIR="$(dirname "$0")"

# Navigate to the script directory
cd "$SCRIPT_DIR"

unreal="unreal"
unity="unity"
empty=""
unityPath=""
unrealPath=""

shopt -s nocasematch

if [ -z "$1" ]; then
  read -p "Use Unreal or Unity: " engine
else
  engine="$1"
fi

while [[ "$engine" != "$unreal" && "$engine" != "$unity" ]]
do
  echo Invalid Engine. Expected: Unreal or Unity
  read -p "Use Unreal or Unity: " engine
done

if [[ "$engine" == "$unreal" ]]; then
  if [ -z "$2" ]; then
    read -p "Enter your Unreal Installation root path (e.g. /Users/Shared/Epic Games//UE_5.5): " unrealPath
    # Get the Unreal installation path
  else
    unrealPath="$2"
  fi
elif [[ "$engine" == "$unity" ]]; then
  if [ -z "$2" ]; then
    read -p "Enter your Unity Installation root path (e.g. /Applications/Unity/Hub/Editor/Unity_6000.0.64f1): " unityPath
    # Get the Unity installation path
  else
    unityPath="$2"
  fi
else
  echo Invalid Engine. Expected: Unreal or Unity
  exit 1
fi

shopt -u nocasematch

if [[ "$unityPath" != "$empty" ]]; then
  echo Running Unity in batch mode to resolve packages. This may take a moment
  "$unityPath" -batchmode -resolvePackages -projectPath "Unity" -quit
fi

# Generate the WAV files
python3 WwiseProject/GenerateProjectWavFiles.py

# Generate the supported Gyms
python3 GenerateSupportedGyms.py

if [[ "$unityPath" != "$empty" ]]; then
  # Set the language for the Unity Project
  python3 Unity/Build/SetStartupLanguage.py
fi

if [[ "$unrealPath" != "$empty" ]]; then
    echo Running Unreal Build Tool. This may take a moment.
	"%unrealPath%/Engine/Build/BatchFiles/Mac/GenerateProjectFiles.sh" GymsEditor Mac Development -project=%CD%\Unreal\Gyms.uproject
fi

# Generate Banks
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"

xml_file="$SCRIPT_DIR/Unity/Assets/WwiseSettings.xml"

# Check if the XML file exists
if [[ ! -f "$xml_file" ]]; then
  echo "Error: XML file '$xml_file' not found."
  exit 1
fi

if [[ "$unityPath" != "$empty" ]]; then
  WwiseInstalationPathMac=$(xmllint --xpath 'string(//WwiseInstallationPathMac)' "$xml_file")

  if [ -z "$WwiseInstalationPathMac" ]; then
    echo "Error: WwiseInstalationPathMac is not defined in the WwiseSettings.xml."
  else
    "$WwiseInstalationPathMac/Contents/Tools/WwiseConsole.sh" generate-soundbank "$SCRIPT_DIR/WwiseProject/Gyms.wproj"
  fi
  

  # Setup addressables
  processName="Unity"
  isRunning=false
  "$unityPath/Editor/Unity.exe" -batchmode --args -projectPath "Unity" -executeMethod "AddressableInstaller.AddressableSetup" & unity_pid=$!


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
fi

read -p "Press Enter to continue..."