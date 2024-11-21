import os
import shutil
from waapi import WaapiClient, CannotConnectToWaapiException
import subprocess
import argparse
from argparse import RawDescriptionHelpFormatter

def find_event_children_count(eventName):
    query = {
            "waql": "from type event where name = \"" + eventName + "\" select children ",
    }

    results = client.call("ak.wwise.core.object.get", query)
    return len(results["return"])

def create_set_query(eventName, soundPath, index):
    query = {
                    "objects": [
                        {
                            "object": "\\Events\\Default Work Unit\\AutoBankSample",
                            "onNameConflict": "merge",
                            "children": [
                                {
                                    "type": "Event",
                                    "name": eventName,
                                    "children": [
                                        {
                                            "name": str(index),
                                            "type": "Action",
                                            "@ActionType": 1,
                                            "@Target": soundPath
                                        }
                                    ]
                                }
                            ]
                        }
                    ]
    }
    return query

def set_SFX_for_event(eventName, soundPath, index):
    query = create_set_query(eventName,soundPath,index)
    client.call("ak.wwise.core.object.set", query)

def add_SFX_for_event(eventName, soundPath):
    childrenCount = find_event_children_count(eventName)
    query = create_set_query(eventName, soundPath, childrenCount+1)

    client.call("ak.wwise.core.object.set", query)
    childrenCount = find_event_children_count(eventName)

def delete_SFX_for_event(eventName, index):
    query = {
            "object": "\\Events\\Default Work Unit\\AutoBankSample\\" + eventName + "\\" + index,
    }
    client.call("ak.wwise.core.object.delete", query)

def generate_sound_bank():
    query = {
        "command": "GenerateAllSoundbanksAllPlatformsAutoClose"
    }

    results = client.call("ak.wwise.ui.commands.execute", query)
    return results

def copy_directory(src, dst, ignore_patterns=None):
    if ignore_patterns is None:
        ignore_patterns = set()

    src = os.path.abspath(src)
    dst = os.path.abspath(dst)

    if not os.path.exists(dst):
        os.makedirs(dst)

    for item in os.listdir(src):
        src_item = os.path.join(src, item)
        dst_item = os.path.join(dst, item)

        if os.path.isdir(src_item):
            if item not in ignore_patterns:
                copy_directory(src_item, dst_item, ignore_patterns)
        else:
            shutil.copy2(src_item, dst_item)

def read_settings(filename):
    global_settings = {}
    runs_settings = []

    with open(filename, 'r') as file:
        for line in file:
            line = line.strip()
            if line.startswith('-'):
                key, value = line.split(':', maxsplit=1)
                global_settings[key.strip()] = value.strip()
            elif line.startswith('Run'):
                settings = {}
                for line in file:
                    line = line.strip()
                    if line:
                        if line.startswith('-'):
                            key, value = line.split(':', maxsplit = 1)
                            settings[key.strip()] = value.strip()
                    else:
                        break
                runs_settings.append(settings)

    return global_settings, runs_settings

def runEditorTest(global_settings, runs_settings):
    for runs in runs_settings:
        automationPath = os.path.join(os.path.dirname(__file__), "RunEditorTests.py")
        cmd_line = ('py "{}" '.format(automationPath) +
                    '-o="{}" '.format(global_settings['-o']) +
                    '-f="{}" '.format(global_settings['-f']) +
                    '-u="{}" '.format(global_settings['-u']) +
                    '-j="{}" '.format(global_settings['-j'])+
                    '-p="{}" '.format(runs['-p']) +
                    '-d="{}" '.format(runs['-d']) +
                    '-e="{}" '.format(runs['-e']) +
                    '-s="{}" '.format(runs['-s']) 
                )
        subprocess.run(cmd_line)
            

if __name__ == "__main__":
    parser = argparse.ArgumentParser(
        formatter_class=RawDescriptionHelpFormatter, 
        description=
            """
            Run gyms tests that requires Waapi
            """
    )
    requiredArguments = parser.add_argument_group("Required Arguments")
    requiredArguments.add_argument('-st', '--settingPath', required=True, type=str, help='The path to the file containing the settings for the run.')
    args = parser.parse_args()
    global_settings, runs_settings = read_settings(args.settingPath)
    source_dir = "Unity"
    destination_dir = "Unity_Test_Temp_Copy"

    # Set of patterns to ignore 
    ignore_patterns = {".git", ".svn"}
    src = os.path.abspath(source_dir)
    dst = os.path.abspath(destination_dir)
    if not os.path.exists(dst):
        os.makedirs(dst)
        # Copy the directory
        print("Copying " + src + " into " + dst)
        copy_directory(source_dir, destination_dir, ignore_patterns)

    try:
        url = global_settings['-w']
        with WaapiClient(url) as client:
           
            eventName = "Play_AutoBankSFX"
            soundPath = "\\Actor-Mixer Hierarchy\\AutoBankSample\\Utility\\BonusSound"
            soundPath2 = "\\Actor-Mixer Hierarchy\\Default Work Unit\\UtilitySound"

            generate_sound_bank()
            runEditorTest(global_settings, runs_settings)

            #Adding the bonus sound to the Play_AutoBankSFX event
            add_SFX_for_event(eventName, soundPath)
            generate_sound_bank()
            runEditorTest(global_settings, runs_settings)

            #Replacing the bonus sound on the Play_AutoBankSFX event by the utility sound
            set_SFX_for_event(eventName, soundPath2, find_event_children_count(eventName))
            generate_sound_bank()
            runEditorTest(global_settings, runs_settings)

            #Deleting the utility sound on the Play_AutoBankSFX
            delete_SFX_for_event(eventName, str(find_event_children_count(eventName)))
            generate_sound_bank()

            client.disconnect()
    except CannotConnectToWaapiException:
        print("Could not connect to Waapi: Is Wwise running and Wwise Authoring API enabled?")
    
    #Deleting the temporary copy. This will be marked as failing because the folder is being used by the unity hub. The content of the folder will still be deleted 
    try:
        shutil.rmtree(dst)
    except OSError as o:
        print(f"Error, {o.strerror}: {dst}")