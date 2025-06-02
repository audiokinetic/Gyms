/*******************************************************************************
The content of this file includes portions of the proprietary AUDIOKINETIC Wwise
Technology released in source code form as part of the game integration package.
The content of this file may not be used without valid licenses to the
AUDIOKINETIC Wwise Technology.
Note that the use of the game engine is subject to the Unreal(R) Engine End User
License Agreement at https://www.unrealengine.com/en-US/eula/unreal
 
License Usage
 
Licensees holding valid licenses to the AUDIOKINETIC Wwise Technology may use
this file in accordance with the end user license agreement provided with the
software or, alternatively, in accordance with the terms contained
in a written agreement between you and Audiokinetic Inc.
Copyright (c) 2024 Audiokinetic Inc.
*******************************************************************************/

#include "WwiseGymsTestReconcileImpl.h"

#include "AkSettings.h"
#include "WwiseReconcile/Public/AkUnrealAssetDataHelper.h"

bool FWwiseGymsTestReconcileImpl::ShouldBeSkipped(const FWwiseReconcileItem& Item)
{
	return !FWwiseGymsReconcileImpl::ShouldBeSkipped(Item);
}

bool FWwiseGymsTestReconcileImpl::ShouldMove(const WwiseAnyRef& Ref, FAssetData InAssetPath, FString& OutNewAssetPath)
{
	bool bShouldMove = false;
	FWwiseReconcileItem ReconcileItem;
	ReconcileItem.WwiseAnyRef.WwiseAnyRef = &Ref;
	if (!ShouldBeSkipped(ReconcileItem) && InAssetPath.IsValid())
	{
		auto WwisePath = Ref.GetObjectPath();
		auto AkSettings = GetMutableDefault<UAkSettings>();
		const FString DefaultPath = AkSettings->DefaultAssetCreationPath;
		WwisePath = WwisePath.String.Replace(TEXT("\\"), TEXT("/"));
		WwisePath = WwisePath.String.Replace(TEXT(" "), TEXT("_"));
		FString ExpectedPath = DefaultPath / FPaths::GetPath(WwisePath.String);

		if (!InAssetPath.GetObjectPathString().Contains(WwisePath.String))
		{
			bShouldMove = true;
			OutNewAssetPath = ExpectedPath;
		}
		UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Should Asset %s Move: %s"), *Ref.GetName(), bShouldMove ? TEXT("True") : TEXT("False"));
	}
	return bShouldMove;
}

int32 FWwiseGymsTestReconcileImpl::MoveAssets(FScopedSlowTask& SlowTask)
{
	for (const auto& AssetData : AssetsToMove)
	{
		UE_LOG(LogWwiseGymsReconcile, Warning, TEXT("TOTALLY moving %s to %s"), *AssetData.WwiseAnyRef.WwiseAnyRef->GetName(), *AssetData.MovedPath);
	}
	return AssetsToMove.Num();
}