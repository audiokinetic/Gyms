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

def parse_results():
    expected_results = ["AddedEvent to Create: True", "TooLongPathEvent to Create: True", "UpdatedEvent to Update : True", "RenamedEvent to Rename : True", "RenamedEventAssetWithSameName to Rename : True", "DeletedEvent to Delete: True", "Could not create asset \'TooLongPathEvent\' at location", "Asset RenamedEventAssetWithSameName already exists at", "Should Asset RenamedEventAssetWithSameName Move: False", "Should Asset MovedEvent Move: True", "TOTALLY moving MovedEvent to /Game/WwiseAudio/Events/ReconcileTests"]
    path = os.path.dirname(__file__) + "/Saved/Logs/ReconcileOutput.txt"
    with open(path) as f:
        content = f.readlines()
    results = []
    testsNames = []
    for line in content:
        for expected_result in expected_results:
            if re.search(expected_result, line):
                expected_results.remove(expected_result)
                break
    if len(expected_results) > 0:
        raise RuntimeError("Expected Logs did not appear {} !".format(','.join(expected_results)))

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
    requiredArguments = parser.add_argument_group("Optional Arguments")
    requiredArguments.add_argument('--dryrun', required=False, action=argparse.BooleanOptionalAction, help='Debug Only. This force Unreal to do a Dryrun of the operations.')

    args = parser.parse_args()
    enginePath = args.enginePath
    dryRun = ''
    if args.dryrun != None:
        dryRun = ' -dryrun'

    path = os.path.dirname(__file__)
    path = os.path.join(path, 'Gyms.uproject')
    cmd = enginePath + " " + path + " -run=\"WwiseReconcileCommandlet\" -modes=all" + dryRun + " -ini:Engine:[Audio]:WwiseReconcileModuleName=WwiseGymsTestReconcile -log=ReconcileOutput.txt"
    print('Running: ' + cmd)
    subprocess.run(cmd, timeout=1000)
    parse_results()
        