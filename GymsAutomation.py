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

class GymsAutomation:

    outputFile = ''
    gymsPath = ''
    gymExtension = ''
    editors = []
    platform = ''

    """
    Load file used to know which gyms to run.
    """
    def load_file(self, path):
        if not(os.path.isfile(path)):
            print('File at path {} not found'.format(path))
            return None

        file = open(path)
        lines = file.readlines()
        gyms = []
        for line in lines:
            line = line.replace('\n', '')
            gyms = gyms + line.split(';')
        file.close()
        return gyms

    def contains_gyms(self, folderPath):
        index = folderPath.rindex(os.path.sep) + 1
        return self.folder_contains_gym(folderPath[index:], folderPath)

    def folder_contains_gym(self, gymName, path):
        return os.path.exists(os.path.join(path, '{}{}'.format(gymName, self.gymExtension)))

    def open_output_file(self, outputFile):
        file = open(outputFile, "w")
        return file

    def get_folders_only(self, directories, path):
        realDirectories = []
        for directory in directories:
            fullPath = os.path.join(path, directory)
            if os.path.isdir(fullPath):
                realDirectories.append(directory)
        return realDirectories

    def gym_exists(self, gymName, path):
        if self.folder_contains_gym(gymName, path):
            return True
        directories = os.listdir(path)
        for directory in directories:
            directoryPath = os.path.join(path, directory)
            if os.path.isdir(directoryPath):
                if self.gym_exists(gymName, directoryPath):
                    return True
        return False

    def folder_exists(self, folderName, path):
        directories = os.listdir(path)
        directories = self.get_folders_only(directories, path)
        for directory in directories:
            if directory == folderName:
                return True, os.path.join(path, directory)
        for directory in directories:
            folderPath = os.path.join(path, directory)
            found, folderPath = self.folder_exists(folderName, folderPath)
            if found:
                return True, folderPath
        return False, None

    def get_folder_gyms(self, gymsList, gym):
        found, folderPath = self.folder_exists(gym, self.gymsPath)
        if found:
            gymsList = self.get_all_gyms_in_folder(folderPath, gymsList)
        return gymsList

    def get_all_gyms(self, gymsList):
        return self.get_all_gyms_in_folder(self.gymsPath, gymsList)

    def run_tests(self, gyms, enginePath, outputFile, targetPlatform, timeout):
        failingGyms = []
        file = open(outputFile, "w")
        #Unreal Automation looks for test paths with a given string to run.

        gymsToSkip = []
        gymsList = []
        for gym in gyms:
            if len(gym) == 0:
                continue
            #All
            if gym == 'All':
                gymsList = self.get_all_gyms(gymsList)
            #Folder
            elif gym.startswith('>'):
                gymsList = self.get_folder_gyms(gymsList, gym[1:])
            #Ignore
            elif gym.startswith('!'):
                if gym[1] == '>':
                    gymsToSkip = self.get_folder_gyms(gymsToSkip, gym[2:])
                else:
                    gymsToSkip = self.get_gym(gymsToSkip, gym[1:])
            #Single Gym
            else:
                gymsList = self.get_gym(gymsList, gym)

        #Remove Duplicates
        gymsList = list(dict.fromkeys(gymsList))
        #Remove Gyms to skip
        gymsList = [gym for gym in gymsList if gym not in gymsToSkip]
        failingGyms = self.write_results(enginePath, file, gymsList, targetPlatform, failingGyms, timeout)
        file.close()
        if len(failingGyms) > 0:
            logFile = open(self.get_log_path())
            lines = logFile.readlines()
            for line in lines:
                print(line)
            raise RuntimeError("{} Failed!".format(','.join(failingGyms)))

    def main(self):
        parser = argparse.ArgumentParser(
            formatter_class=RawDescriptionHelpFormatter, 
            description=
                """
                Run gyms tests

                File format: The file contains folder names or file names separated by ";". 
                "All" will run every gyms.
                Putting ">" in front of a name will run every gym within the folder.
                Putting "!" in front of a name will skip the test. This can be applied to folders as well.
                Examples:
                ">1-Basic" will run every gyms within the folder 1-Basic
                "Gym1;Gym2;Gym3" will run Gym1, Gym2 and Gym3
                ">1-Basic;Gym1" will run Gym1 as well as every gym within the 1-Basic folder.
                ">1-Basic;!Gym2 will run every gyms within the 1-Basic folder except for Gym2."
                "All;!>1-Basic will run every gyms except for the 1-Basic folder.
                """
        )
        
        requiredArguments = parser.add_argument_group("Required Arguments")
        requiredArguments.add_argument('-f', '--filePath', required=True, type=str, help='The path to the file indicating which gyms to run.')
        requiredArguments.add_argument('-u', '--enginePath', required=True, type=str, help='The path to the Engine Editor ("Path/To/Editor/{}.exe)'.format(self.editors))
        requiredArguments.add_argument('-o', '--fileOutput', required=True, type=str, help='Where the results are written.')
        self.platform_argument(requiredArguments)

        optionalArguments = parser.add_argument_group("Optional Arguments")
        optionalArguments.add_argument('-t', '--timeout', required=False, type=int, default=6000, help='The maximum duration of the test in seconds. The run will fail without finishing if it lasts longer than the given value. The default value is 10 minutes.')

        args = parser.parse_args()
        enginePath = args.enginePath
        filePath = args.filePath
        fileOutput = args.fileOutput
        self.platform = self.get_target_platform(args)
        timeout = args.timeout

        if os.path.isfile(enginePath):
            found = False
            for editor in self.editors:
                if re.search('{}.exe$'.format(editor), enginePath):
                    found = True
                    break
            if not(found):
                print("{} not found. Make sure the given path is correct.".format(self.editor))
                return
        
        gyms = self.load_file(filePath)
        if gyms == None:
            return
        self.run_tests(gyms, enginePath, fileOutput, self.platform, timeout)