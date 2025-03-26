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

#include "WwiseGymsReconcileImpl.h"

#include "AkSettings.h"
#include "PackageTools.h"
#include "Wwise/Stats/Reconcile.h"
#include "WwiseReconcile/Public/AkUnrealAssetDataHelper.h"

DEFINE_LOG_CATEGORY(LogWwiseGymsReconcile);

bool FWwiseGymsReconcileImpl::ShouldBeSkipped(const FWwiseReconcileItem& Item)
{
	const auto ReconcileTestText = TEXT("ReconcileTests");
	if (Item.WwiseAnyRef.WwiseAnyRef)
	{
		return Item.WwiseAnyRef.WwiseAnyRef->GetObjectPath().Contains(ReconcileTestText);		
	}
	return Item.Asset.PackagePath.ToString().Contains(ReconcileTestText);
}

FString FWwiseGymsReconcileImpl::GetAssetPackagePath(const WwiseAnyRef& WwiseRef) const
{
	FString Path = FWwiseReconcileImpl::GetAssetPackagePath(WwiseRef);
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Asset Package Path: %s"), *Path);
	return Path;
}

bool FWwiseGymsReconcileImpl::AddToDelete(FWwiseReconcileItem& Item)
{
	bool bDelete = false;
	if (!ShouldBeSkipped(Item))
	{
		bDelete = FWwiseReconcileImpl::AddToDelete(Item);
	}
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Adding Asset %s to Delete: %s"), *Item.Asset.AssetName.ToString(), bDelete ? TEXT("True") : TEXT("False"));
	return bDelete;
}

bool FWwiseGymsReconcileImpl::AddToCreate(FWwiseReconcileItem& Item)
{
	bool bCreate = false;
	if (!ShouldBeSkipped(Item))
	{
		bCreate = FWwiseReconcileImpl::AddToCreate(Item);
	}
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Adding Asset %s to Create: %s"), *Item.WwiseAnyRef.WwiseAnyRef->GetName(), bCreate ? TEXT("True") : TEXT("False"));
	return bCreate;
}

bool FWwiseGymsReconcileImpl::AddToRename(FWwiseReconcileItem& Item)
{
	bool bRename = false;
	if (!ShouldBeSkipped(Item))
	{
		bRename = FWwiseReconcileImpl::AddToRename(Item);
	}
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Adding Asset %s to Rename : %s"), *Item.WwiseAnyRef.WwiseAnyRef->GetName(), bRename ? TEXT("True") : TEXT("False"));
	return bRename;
}

bool FWwiseGymsReconcileImpl::AddToUpdate(FWwiseReconcileItem& Item)
{
	bool bUpdate = false;
	if (!ShouldBeSkipped(Item))
	{
		bUpdate = FWwiseReconcileImpl::AddToUpdate(Item);
	}
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Adding Asset %s to Update : %s"), *Item.WwiseAnyRef.WwiseAnyRef->GetName(), bUpdate ? TEXT("True") : TEXT("False"));
	return bUpdate;
}

bool FWwiseGymsReconcileImpl::ShouldMove(const WwiseAnyRef& Ref, FAssetData InAssetPath, FString& OutNewAssetPath)
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
	}
	UE_LOG(LogWwiseGymsReconcile, Log, TEXT("Should Asset %s Move: %s"), *Ref.GetName(), bShouldMove ? TEXT("True") : TEXT("False"));
	return bShouldMove;
}

int32 FWwiseGymsReconcileImpl::MoveAssets(FScopedSlowTask& SlowTask)
{
	for (const auto& AssetData : AssetsToMove)
	{
		UE_LOG(LogWwiseGymsReconcile, Warning, TEXT("TOTALLY moving %s to %s"), *AssetData.WwiseAnyRef.WwiseAnyRef->GetName(), *AssetData.MovedPath);
	}
	return AssetsToMove.Num();
}
