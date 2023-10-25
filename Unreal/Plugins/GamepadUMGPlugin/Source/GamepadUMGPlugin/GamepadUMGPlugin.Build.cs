// Copyright 2015 by Nathan "Rama" Iyer. All Rights Reserved.
using UnrealBuildTool;

public class GamepadUMGPlugin : ModuleRules
{
	public GamepadUMGPlugin(ReadOnlyTargetRules Target) : base(Target)
    {
        PublicDependencyModuleNames.AddRange(
			new string[] { 
				"Core", 
				"CoreUObject", 
				"Engine", 
				"InputCore",
#if UE_4_26_OR_LATER
				"DeveloperSettings"
#endif
			}
		);

        PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore", "UMG" });
		PrivatePCHHeaderFile = "Private/GamepadUMGPluginPrivatePCH.h";
	}
}
