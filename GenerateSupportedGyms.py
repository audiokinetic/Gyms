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

import os

def get_all_gyms_in_folder(folderPath, fileText, depth):
    if(depth > 0):
        for i in range(depth - 1):
            fileText = fileText + '    '
        gym = folderPath[folderPath.rfind(os.path.sep) + 1 :]
        fileText = fileText + '- ' + gym + '\n'
    subfolders = os.listdir(folderPath)
    subfolders.sort(key=str.lower)
    for subfolder in subfolders:
        fullPath = os.path.join(folderPath, subfolder)
        if os.path.isdir(fullPath):
            fileText = get_all_gyms_in_folder(fullPath, fileText, depth + 1)
    return fileText

def get_all_gyms(path):
    fileText = ''
    fileText = get_all_gyms_in_folder(path, fileText, 0)
    return fileText

if __name__ == "__main__":
    unityGyms = get_all_gyms(os.path.join(os.path.dirname(__file__), "Unity", "Assets", "Gyms"))
    file = open(os.path.join("Unity", "SupportedGyms.md"), "w")
    file.write(unityGyms)
    file.close()
    file = open(os.path.join("Unreal", "SupportedGyms.md"), "w")
    unrealGyms = get_all_gyms(os.path.join(os.path.dirname(__file__), "Unreal", "Content", "Gyms"))
    file.write(unrealGyms)
    file.close()