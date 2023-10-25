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

import sys, os, subprocess, shutil

# Steps that must be done by the build system BEFORE calling this script:
# - SVN checkout the Integration folder from the Unity integration repo.
# - Copy (filter out unrequired files) the content of the Integration folder into a new folder: WwiseUnityDemoScene.
# - Unzip the right Unity integration build for Windows and Mac.
# - Copy all files from the Unity integration build inside WwiseUnityDemoScene/Assets.

# Steps that must be done by the build system AFTER calling this script:
# - Package the folder WwiseUnityDemoScene with all the files contained, including the root demo scene folder.
# - Regenerate the bundle.json of the associated unity integration build so that demo scene zip can be included for the Launcher.

if (len(sys.argv) != 5):
	print("Usage: python BuildGyms.py PathToUnity.exe PathToWwiseConsole.exe PathToUnityGyms PathToWwiseProject.wproj")
	sys.exit(1)

pathToUnityExecutable = sys.argv[1]
pathToWwiseConsole = sys.argv[2]
pathToUnityGyms = sys.argv[3]
pathToWwiseProject = sys.argv[4]

print("Generating SoundBanks...")
sys.stdout.flush()
result = subprocess.run([pathToWwiseConsole, "generate-soundbank", pathToWwiseProject, "--save", "--platform", "Windows", "Mac", "--verbose"])

# 0 means success, 2 means warning. Only 1 means error
if (result == 1):
	sys.exit(1)

# Get rid of the cache folder after the soundbank generation.
shutil.rmtree(os.path.join(pathToUnityGyms, "..", "WwiseProject", ".cache"))