![Wwise Gyms](Documentation/Images/Icons/Icon-128.png)
# Wwise&trade; Gyms

The Wwise Gyms are examples and tests for both the Unreal&trade; and Unity&trade; Wwise&trade; Integrations.

## Minimum supported versions

The projects are developed using the following software versions:

- Wwise&trade; 2022.1.6
- Unity&trade; 2021.3.29f1
- Unreal&trade; 5.1.1

## Setup

The Gyms do not include the Wwise Integration of their engine. Use the Wwise Launcher to integrate Wwise into both projects.
In the **WwiseProject** folder, run **GenerateProjectWavFiles.py**. Open the Wwise Project in Wwise and generate the SoundBanks.
Alternatively, you can run the **setup.bat** file for your platform.

### Unreal
With the Wwise Gyms for Unreal, make sure to set the version of the uproject before you integrate the project through the Audiokinetic Launcher. To change the uproject version, right-click <b>Gyms.uproject</b> and select <b>Switch Unreal Engine Version</b>.

### Unity
With the Wwise Gyms for Unity, select <b>Install files directly into the Unity project directory</b> when you are integrating Wwise. Refer to [Integrating Wwise into a Unity Project](https://www.audiokinetic.com/en/library/wwise_launcher/?source=InstallGuide&id=integrating_wwise_into_a_unity_project) for details.

### Aditional Requirements
In order to generate the Wwise WAV files, you need to install a [Python 3 interpreter](https://www.python.org/), as well as the NumPy package. The NumPy package can be compiled from source or installed through package managers. For example, you can use pip to install NumPy by calling `pip install numpy`. Refer to [the NumPy install page](https://numpy.org/install/) for more information.

## Project Documentation

- [Wwise&trade; Gyms documentation](Documentation/Gyms/README.md)
- [Wwise&trade; Gyms for Unity&trade;](Unity/README.md)
- [Wwise&trade; Gyms for Unreal&trade;](Unreal/README.md)

## Legal

[Wwise Gyms are released under dual license with Apache 2.0.](LICENSE)
