![Wwise Gyms](Documentation/Images/Icons/Icon-128.png)
# Wwise&trade; Gyms

The Wwise Gyms are examples and tests for both the Unreal&trade; and Unity&trade; Wwise&trade; Integrations.

## Supported versions

The Gym projects support the major version of Wwise indicated in the current git branch, as well as its minor versions. The following software versions are supported:

- Wwise&trade; 2024.1 and all minor releases
- The latest three Unity&trade; LTS versions
- The latest three Unreal&trade; versions

## Setup

The Gyms do not include the Wwise Integration of their engine. Use the Audiokinetic Launcher to integrate Wwise into both projects.
Execute the **setup_Windows.bat** or **setup_Mac.command** depending on your platform.
Alternatively, to execute actions independently, run the **`GenerateProjectWavFiles.py`** script to generate the audio files, then open the Wwise Project in Wwise Authoring and generate the SoundBanks.

### Unreal
With the Wwise Gyms for Unreal, make sure to set the version of the uproject before you integrate the project through the Audiokinetic Launcher. To change the uproject version, right-click <b>Gyms.uproject</b> and select <b>Switch Unreal Engine Version</b>.

### Unity
With the Wwise Gyms for Unity, integrate Wwise into your projects as described in [Integrating Wwise into a Unity Project](https://www.audiokinetic.com/en/library/wwise_launcher/?source=InstallGuide&id=integrating_wwise_into_a_unity_project).
Additionally, by default, the Unity Gyms use the Wwise Unity Addressable package. To set up the package, do one of the following:

- Use the **setup_Windows.bat** or **setup_Mac.command** to complete the installation.
- Use the Wwise Unity Addressable Installer in the Unity Editor.
- Follow the installation process under the **Setting Up and Building with the Wwise Addressables Package** section on the [Installing the Wwise Unity Addressables Package](https://www.audiokinetic.com/en/public-library/?source=Unity&id=pg_addressables_setup.html#adressables_setup_build) page.

If you do not want to use the Wwise Unity Addressable package, you can uninstall it in one of the following ways:

- Use the **uninstall_Unity_Addressable_Windows.bat** or **uninstall_Unity_Addressable_Mac.command**.
- Uninstall it directly from the Unity Package Manager and then regenerate the Soundbanks.

#### RunAutoBankAddressableTest.bat
A bat file executing a set of tests for the Unity Addressables Auto-Bank feature. The test will create a copy of the Gyms Wwise project before editing it. This set of test requires the Python (Waapi-Client). Execute ```py -3 -m pip install waapi-client``` to install the module. Refer to [Python (Waapi-Client)](https://www.audiokinetic.com/en/library/edge/?source=SDK&id=waapi_client_python_rpc.html) for details. Before running the .bat file, make sure that:

- The Unity Editor is closed.
- The Gyms project is open in Wwise Authoring.
- The settings in (Unity/UnityWaapiTestSettings.txt) match your configuration.

The (Unity/UnityWaapiTestSettings.txt) contains the following settings:

- -f: The input file that contains details about which test to run. For more the information, see the Input File Formatting section in (Unity/Testing.md).
- -u: The path to the Unity Editor.
- -o: The output path for the test results.
- -j: The path to the folder that contains the temporary copy of the gym project. Must always be "PathToTheGymProject\Unity_Test_Temp_Copy".
- -w: The WAAPI configuration (IP:Port).

- -d: Whether or not domainReload is enabled (0: enabled, 1: disabled).
- -s: Whether or not sceneReload is enabled (0: enabled, 1: disabled).
- -e: Whether the sound engine is initialized in the editor (0: initialized in the editor, 1: not initialized in the editor).
- -p: Target platform on which to run the test, either Playmode or StandaloneWindows64.

### Aditional Requirements
In order to generate the Wwise WAV files, you need to install a [Python 3 interpreter](https://www.python.org/), as well as the NumPy package. The NumPy package can be compiled from source or installed through package managers. For example, you can use pip to install NumPy by calling `pip install numpy`. Refer to [the NumPy install page](https://numpy.org/install/) for more information.

## Project Documentation

- [Wwise&trade; Gyms documentation](Documentation/Gyms/README.md)
- [Wwise&trade; Gyms for Unity&trade;](Unity/README.md)
- [Wwise&trade; Gyms for Unreal&trade;](Unreal/README.md)

## Legal

[Wwise Gyms are released under dual license with Apache 2.0.](LICENSE)
