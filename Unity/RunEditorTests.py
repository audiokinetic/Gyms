"""
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
"""

import argparse
from argparse import RawDescriptionHelpFormatter
import os
import re
import subprocess

editors = ['Unity']

def run_gyms(playModeOption, wwiseSettings, enginePath, filePath, fileOutput, platform):
    set_enter_play_mode_option(playModeOption)
    print_settings(playModeOption, wwiseSettings)
    automationPath = os.path.join(os.path.dirname(__file__), "UnityAutomation.py")
    cmd_line = ('py "{}" '.format(automationPath) +
                '-o="{}" '.format(fileOutput) +
                '-f="{}" '.format(filePath) +
                '-u="{}" '.format(enginePath) +
                '-p="{}"'.format(platform)
               )
    subprocess.run(cmd_line)

def path_to_editor_settings():
    return os.path.join(os.path.dirname(__file__), "ProjectSettings", "EditorSettings.asset")

def path_to_wwise_settings():
    return os.path.join(os.path.dirname(__file__), "Assets", "WwiseSettings.xml")

def set_enter_play_mode_option(settingValue):
    set_unity_setting(path_to_editor_settings(), "m_EnterPlayModeOptions:", str(settingValue))

def set_wwise_setting(settingFile, settingName, settingValue):
    formattedName = '<' + settingName + '>'
    settingValueString = 'true'
    if(settingValue == 1):
        settingValueString = 'false'
    with open(settingFile, 'r') as f:
        content = f.readlines()
        for i in range(len(content)):
            line = content[i]
            search = re.search(formattedName, line)
            if search:
                restOfLine = line[search.end() :]
                endOfSetting = restOfLine.find('<')
                line = line.replace(restOfLine[: endOfSetting], settingValueString)
                content[i] = line
        f.close()
    with (open(settingFile, 'w')) as f:
        f.write(''.join(content))
        f.close()

def set_unity_setting(settingFile, settingName, settingValue):
    with open(settingFile, 'r') as f:
        content = f.readlines()
        for i in range(len(content)):
            line = content[i]
            search = re.search(settingName, line)
            if search:
                setting = line[(search.end() + 1) :]
                line = line.replace(setting, settingValue) + '\n'
                content[i] = line
        f.close()
    with (open(settingFile, 'w')) as f:
        f.write(''.join(content))
        f.close()

def enableEnterPlayModeOption():
    set_unity_setting(path_to_editor_settings(), "m_EnterPlayModeOptionsEnabled", ' 1')

def get_tests_options(domain, scene, editor):
    combos = []
    if(domain == 2 and scene == 2):
        for i in range(domain):
            for j in range(scene):
                combos.append((i, j))
    elif(domain < 2 and scene == 2):
        for j in range(scene):
            combos.append((domain, j))
    elif(domain == 2 and scene < 2):
        for i in range(domain):
            combos.append((i, scene))
    else:
        combos.append((domain, scene))

    editorValue = editor
    if editorValue == 2:
        editorValue = 0

    for i in range(len(combos)):
        if editor == 2:
            combos.append(combos[i] + (1, ))
        combos[i] = combos[i] + (editorValue, )
    return combos

def get_enter_play_mode_option(domain, scene):
    return domain + scene * 2

def settings_to_string(setting):
    if setting == 0:
        return 'Enabled'
    elif setting == 1:
        return 'Disabled'
    return ''

def get_settings_name(unitySettings, wwiseSettings):
    return "Load Sound Engine in Edit Mode {}, Domain Reload {} and Scene Reload {}".format(settings_to_string(wwiseSettings), settings_to_string(unitySettings % 2), settings_to_string(unitySettings >= 2))

def print_settings(unitySettings, wwiseSettings):
    print("Running Tests with: {}".format(get_settings_name(unitySettings, wwiseSettings)))

def correct_play_mode_value(value):
    intValue = int(value)
    if intValue >= 0 and intValue <= 2:
        return intValue
    raise argparse.ArgumentTypeError("{} is an invalid value. The value must be 0, 1 or 2".format(value))

def get_output_file_for_combo(fileOutput):
    outputPath, fileExtension = os.path.splitext(fileOutput)
    return outputPath + '_Temp' + fileExtension

def write_to_output_file(outputFile, inputFile, combo):
    file = open(outputFile, "a")
    unity = get_enter_play_mode_option(combo[0], combo[1])
    file.write(get_settings_name(unity, combo[2]) + ':\n')
    comboFile = open(inputFile, "r")
    lines = comboFile.readlines()
    for line in lines:
        file.write(line)
    file.write('\n')
    comboFile.close()
    file.close()

def main():
        parser = argparse.ArgumentParser(
            formatter_class=RawDescriptionHelpFormatter, 
            description=
                """
                Run gyms tests with specific Enter Play Mode settings

                File format: The file contains folder names or file names separated by ";". 
                "All" will run every gyms.
                Putting ">" in front of a name will run every gym within the folder.
                Examples:
                ">1-Basic" will run every gyms within the folder 1-Basic
                "Gym1;Gym2;Gym3" will run Gym1, Gym2 and Gym3
                ">1-Basic;Gym1" will run Gym1 as well as every gym within the 1-Basic folder.
                """
        )
        
        requiredArguments = parser.add_argument_group("Required Arguments")
        requiredArguments.add_argument('-f', '--filePath', required=True, type=str, help='The path to the file indicating which gyms to run.')
        requiredArguments.add_argument('-u', '--enginePath', required=True, type=str, help='The path to the Engine Editor ("Path/To/Editor/{}.exe)'.format(editors))
        requiredArguments.add_argument('-o', '--fileOutput', required=True, type=str, help='Where the results are written.')
        requiredArguments.add_argument('-d', '--domainReload', required=True, type=correct_play_mode_value, help='Which setting of the Domain Reload to use. 0 = Domain Reload Enabled. 1 = Domain Reload Disabled. 2 = Both (Tests will run twice)')
        requiredArguments.add_argument('-s', '--sceneReload', required=True, type=correct_play_mode_value, help='Which setting of the Scene Reload to use. 0 = Scene Reload Enabled. 1 = Scene Reload Disabled. 2 = Both (Tests will run twice)')
        requiredArguments.add_argument('-e', '--editor', required=True, type=correct_play_mode_value, help='Should the Sound Engine be Initialized in the editor? 0 = Sound Engine is Initialized in the editor. 1 = Sound Engine is not Initialized in the editor. 2 = Both (Tests will run twice)')
        requiredArguments.add_argument('-p', '--platform', required=True, type=str, help='The platform on which the tests run.')
        args = parser.parse_args()
        domainReload = args.domainReload
        sceneReload = args.sceneReload
        enginePath = args.enginePath
        filePath = args.filePath
        fileOutput = args.fileOutput
        editor = args.editor
        platform = args.platform

        combos = get_tests_options(domainReload, sceneReload, editor)
        enableEnterPlayModeOption()

        file = open(fileOutput, 'w')
        file.close()

        outputPath = get_output_file_for_combo(fileOutput)
        for i in range(len(combos)):
            set_wwise_setting(path_to_wwise_settings(), 'LoadSoundEngineInEditMode', combos[i][2])
            playmode = get_enter_play_mode_option(combos[i][0], combos[i][1])
            run_gyms(playmode, combos[i][2], enginePath, filePath, outputPath, platform)
            write_to_output_file(fileOutput, outputPath, combos[i])
        os.remove(outputPath)

if __name__ == "__main__":
    main()