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
import subprocess
import re
import glob

def parse_results():
    expected_results = ["[Main_Game_Event_2].*Main_Game_SoundBank.bnk.*Bulk Data", "[DLC_TextFilterAssetLibrary].*DLC_Event.bnk.*Bulk Data", "DLC_Event_With_Main_Game_Ref.bnk.*Bulk Data"]
    unintended_results = ["Post_Localized_Voice.bnk.*Bulk Data DLC_LanguageFilter"]
    path = os.path.dirname(__file__) + "/Saved/Logs/CookingOutput.txt"
    with open(path) as f:
        content = f.readlines()
    results = []
    testsNames = []
    errors = []
    for line in content:
        for expected_result in expected_results:
            if re.search(expected_result, line):
                expected_results.remove(expected_result)
                break
        for unintended_result in unintended_results:
            if re.search(unintended_result, line):
                errors.append(unintended_result)
                break
    if len(expected_results) > 0:
        raise RuntimeError("Expected Logs did not appear {} !".format(','.join(expected_results)))
    if len(errors) > 0:
        raise RuntimeError("Unintended Logs did appear {} !".format(','.join(errors)))
        
def wemFilesFound(expected):
    path = os.path.dirname(__file__)
    path = os.path.join(path, "Saved", "Cooked", "Windows", "Gyms", "Content", "WwiseAudio")
    files = glob.glob(path + '/**/*.wem', recursive=True)
    
    if expected:
        otherThanExternal = False
        externalSource = False
        for file in files:
            if "ExternalSources" in file:
                externalSource = True
            else:
                otherThanExternal = True
        return externalSource and otherThanExternal  

    #To remove once WG-76743 is fixed
    else:
        for file in files:
            if "ExternalSources" not in file:
                return False      
    return len(files) > 0 == expected
    

if __name__ == '__main__':
    parser = argparse.ArgumentParser(
        formatter_class=RawDescriptionHelpFormatter, 
        description=
            """
            Run gyms Reconcile tests

            This can only be run once. Then, it is expected to fail.
            """
    )
        
    requiredArguments = parser.add_argument_group("Required Arguments")
    requiredArguments.add_argument('-u', '--enginePath', required=True, type=str, help='The path to the Engine Editor ("Path/To/Editor/UnrealEditor-cmd.exe)')

    args = parser.parse_args()
    enginePath = args.enginePath

    path = os.path.dirname(__file__)
    path = os.path.join(path, 'Gyms.uproject')
    cmd = enginePath + " " + path + " -run=cook -targetplatform=Windows -log=CookingOutput.txt -ini:Game:[/Script/WwisePackaging.WwisePackagingSettings]:bPackageAsBulkData=False"
    print('Running: ' + cmd)
    subprocess.run(cmd, timeout=1000)
    noBulkData = wemFilesFound(True)
    
    cmd = enginePath + " " + path + " -run=cook -targetplatform=Windows -log=CookingOutput.txt -ini:Game:[/Script/WwisePackaging.WwisePackagingSettings]:bPackageAsBulkData=True"
    print('Running: ' + cmd)
    subprocess.run(cmd, timeout=1000)
    withBulkData = wemFilesFound(False)
    
    parse_results()
    
    if not noBulkData:
        raise RuntimeError("Wems were missing from cooking without bulk data")
        
    if not withBulkData :
        raise RuntimeError("Unexpected Wems were found in cooking with bulk data")