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

import sys, os, os.path, platform, subprocess, shutil
from distutils.dir_util import copy_tree, remove_tree

# Base class for building the Wwise Audio Lab
class PlatformBuilder:
	def __init__(self, platformName, configuration):
		self.editorPlatformName = platformName
		self.configuration = configuration
		self.projectFile = os.path.join(os.getcwd(), "Gyms", "Unreal", "Gyms.uproject")
		self.wwiseProjectFile = os.path.join(os.getcwd(), "Gyms", "WwiseProject", "Gyms.wproj")
		self.packagePath = os.path.join(os.getcwd(), "Packaged_Gyms")
		self.unrealVersion = os.environ["UE_VERSION"]

		if self.unrealVersion >= '5.1':
			os.environ["DEVELOPER_DIR"] = "/Applications/Xcode14.1.app/Contents/Developer"
		else:
			os.environ["DEVELOPER_DIR"] = "/Applications/Xcode13.4.1.app/Contents/Developer"

	def GenerateProjectFiles(self):
		print("Generating Project files...")
		cmd = [self.unrealBuildToolPath, "-projectfiles", "-project=" + self.projectFile, "-game",  "-progress"]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("UnrealBuildTool failed.")
			
		except Exception as err:
			raise RuntimeError("UnrealBuildTool failed; {}".format(err))

	def BuildGame(self):
		print("Building game...")
		editor = "-ue4exe=UE4Editor-Cmd.exe"
		if self.unrealVersion >= '5.0':
			editor = "-ue4exe = UnrealEditor-cmd.exe"
		cmd = [self.unrealAutomationToolPath, "-ScriptsForProject=" + self.projectFile, "BuildCookRun", "-nocompile", "-nocompileeditor", "-installed", "-nop4", "-project=" + self.projectFile, "-cook", "-stage", "-archive", "-archivedirectory=" + self.packagePath, "-package", "-clientconfig=Development", editor, "-clean", "-prereqs", "-nodebuginfo", "-targetplatform=" + self.gamePlatformName, "-build",  "-CrashReporter", "-utf8output"]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("UnrealAutomationTool failed.")
			
		except Exception as err:
			raise RuntimeError("UnrealAutomationTool failed; {}".format(err))

	def PackageGame(self):
		self.GenerateProjectFiles()
		self.BuildEditor()
		self.BuildGame()
		self.RenameBuildFolder()

# Class to build Windows plugin, uses the binary install of UE
class WindowsBuilder(PlatformBuilder):
	def __init__(self):
		PlatformBuilder.__init__(self, "Win64", "Development Editor")
		self.gamePlatformName = "Win64"
		
		self.unrealInstallPath = "C:\\Epic Games\\UE_" + self.unrealVersion

		if self.unrealVersion >= '5.0':
			self.unrealBuildToolPath = self.unrealInstallPath + "\\Engine\\Binaries\\DotNET\\UnrealBuildTool\\UnrealBuildTool.exe"
		else:
			self.unrealBuildToolPath = self.unrealInstallPath + "\\Engine\\Binaries\\DotNET\\UnrealBuildTool.exe"        
		self.compiler = "C:\VS2019\MSBuild\Current\Bin\MSBuild.exe"
		self.codeSolutionPath = os.path.join(os.getcwd(), "Gyms", "Unreal", "Gyms.sln")
		self.unrealAutomationToolPath = os.path.join(self.unrealInstallPath, "Engine", "Build", "BatchFiles", "RunUAT.bat")
		if self.unrealVersion >= '5.0':
			self.unrealEditorPath = os.path.join(self.unrealInstallPath, "Engine", "Binaries", "Win64", "UnrealEditor-cmd.exe")
		else:
			self.unrealEditorPath = os.path.join(self.unrealInstallPath, "Engine", "Binaries", "Win64", "UE4Editor-cmd.exe")
		self.wwiseConsolePath = os.path.join(os.getcwd(), "Wwise", "Authoring", "x64", "Release", "bin", "WwiseConsole.exe")
		
	def BuildEditor(self):
		print("Compiling editor code for platform: " + self.editorPlatformName)
		cmd = [self.compiler, self.codeSolutionPath, "/t:Rebuild", "/p:Configuration={},Platform={}".format(self.configuration, self.editorPlatformName)]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("{} failed.".format(self.compiler))
		except Exception as err:
			raise RuntimeError("{} failed; {}".format(self.compiler, err))

	def GenerateSoundBanks(self):
		print("Inserting license in Wwise project...")
		shutil.copy(self.wwiseProjectFile, self.wwiseProjectFile + ".bak")
		injectLicensePath = os.path.join(os.getcwd(), "Wwise", "Tools", "RoboQA", "bin", "injectlicense.py")
		licenseFilePath = os.path.join(os.getcwd(), "Wwise", "Tools", "RoboQA", "bin", "roboqa.txt")
		cmd = ["python", injectLicensePath, "-license", licenseFilePath, "-project", self.wwiseProjectFile]
		print("Command is: {}".format(cmd))
		result = subprocess.call(cmd)
		if (result != 0):
			sys.exit(1)

		print("Migrating project...")
		cmd = [self.wwiseConsolePath, "migrate", self.wwiseProjectFile, "--quiet"]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("Project migration failed.")
			
		except Exception as err:
			raise RuntimeError("Project migration failed; {}".format(err))

		print("Generating SoundBanks...")
		cmd = [self.unrealEditorPath, self.projectFile, "-run=GenerateSoundBanks", "-platforms=Windows", "-wwiseConsolePath=" + self.wwiseConsolePath + "", "-rebuild"]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			#Gyms currently have events that will throw an error by design
			#if ret != 0:
			#	raise RuntimeError("UnrealEditor failed.")
			
		except Exception as err:
			raise RuntimeError("UnrealEditor failed; {}".format(err))

		print("Removing license from Wwise project...")
		shutil.copy(self.wwiseProjectFile + ".bak", self.wwiseProjectFile)
		os.remove(self.wwiseProjectFile + ".bak")

	def GenerateSoundBanksForPackage(self):
		self.GenerateProjectFiles()
		self.BuildEditor()
		self.GenerateSoundBanks()

	def RenameBuildFolder(self):
		shutil.rmtree(os.path.join(self.packagePath, "Gyms"))
		shutil.move(os.path.join(self.packagePath, "WindowsNoEditor"), os.path.join(self.packagePath, "Gyms"))
		#shutil.copyfile(os.path.join(os.getcwd(), "Gyms", "Build", "Gyms - VR.lnk"), os.path.join(self.packagePath, "Gyms", "Gyms - VR.lnk"))

		

class MacBuilder(PlatformBuilder):
	def __init__(self):
		PlatformBuilder.__init__(self, "macosx", "Development Editor")
		self.gamePlatformName = 'Mac'
		self.unrealInstallPath = "/Users/Shared/Epic Games/UE_" + self.unrealVersion
		self.unrealBuildToolPath = self.unrealInstallPath + "/Engine/Build/BatchFiles/Mac/GenerateProjectFiles.sh"
		self.compiler = "xcodebuild"
		self.codeSolutionPath = os.path.join(os.getcwd(), "Gyms", "Unreal", "Intermediate", "ProjectFiles", "Gyms.xcodeproj")
		self.unrealAutomationToolPath = self.unrealInstallPath + "/Engine/Build/BatchFiles/RunUAT.command"

	def GenerateProjectFiles(self):
		print("Generating Project files...")
		currentPath = os.getcwd()
		os.chdir(self.unrealInstallPath + "/Engine/Build/BatchFiles/Mac")
		cmd = [self.unrealBuildToolPath, "-project=" + self.projectFile, "-game"]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("{} failed.".format(cmd))
		except Exception as err:
			raise RuntimeError("UnrealBuildTool failed; {}".format(err))
		os.chdir(currentPath)

	def BuildEditor(self):
		print("Compiling plugin code for platform: " + self.editorPlatformName)
		cmd = [self.compiler, "-project", self.codeSolutionPath, "-configuration", self.configuration, "-sdk", self.editorPlatformName, "-arch", "x86_64", "build" ]
		try:
			print("Command is: {}".format(cmd))
			process = subprocess.Popen(cmd, shell=False)
			ret = process.wait()
			if ret != 0:
				raise RuntimeError("{} failed.".format(self.compiler))
		except Exception as err:
			raise RuntimeError("{} failed; {}".format(self.compiler, err))

	def RenameBuildFolder(self):
		shutil.rmtree(os.path.join(self.packagePath, "Gyms"))
		shutil.move(os.path.join(self.packagePath, "MacNoEditor"), os.path.join(self.packagePath, "Gyms"))
		shutil.copyfile(os.path.join(os.getcwd(), "Gyms", "Build", "KeepThisFolder.txt"), os.path.join(self.packagePath, "Gyms", "Gyms.app", "Contents", "UE4", "Gyms", "Binaries", "Mac", "KeepThisFolder.txt"))
 
def main(argv=None):

	skipSDKCopy = False
	generateSoundBanks = True
	for i in range(0, len(argv)):
		if argv[i] == 'GenerateSoundBanks':
			generateSoundBanks = True
		if argv[i] == 'SkipSDK':
			skipSDKCopy = True

	if not skipSDKCopy:
		fromDirectory = os.path.join(os.getcwd(), "Gyms", "Unreal", "Plugins", "Wwise", "ThirdParty", "SDK")
		toDirectory = os.path.join(os.getcwd(), "Gyms", "Unreal", "Plugins", "Wwise", "ThirdParty")
		copy_tree(fromDirectory, toDirectory)
		remove_tree(fromDirectory)
	
	os.environ["MLSDK"] = ""
	builder = None
	if platform.system() == 'Darwin':
		builder = MacBuilder()
	elif platform.system() == 'Windows':
		builder = WindowsBuilder()
	else:
		raise RuntimeError("Unable to build; unknown platform")
		
	if generateSoundBanks:
		builder.GenerateSoundBanksForPackage()
	else:
		builder.PackageGame()
	
if __name__ == '__main__':
	sys.exit(main(sys.argv))
		