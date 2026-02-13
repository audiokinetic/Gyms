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

#pragma once

#include "WwiseProjectDatabase/Public/Wwise/Ref/WwiseAnyRef.h"
#include "WwiseReconcile/Public/Wwise/WwiseReconcileImpl.h"

WWISEGYMSRECONCILE_API DECLARE_LOG_CATEGORY_EXTERN(LogWwiseGymsReconcile, Log, All);

class WWISEGYMSRECONCILE_API FWwiseGymsReconcileImpl : public FWwiseReconcileImpl
{
protected:
	virtual bool ShouldBeSkipped(const FWwiseReconcileItem& Item) const;
public:
	virtual FString GetAssetPackagePath(const FWwiseAnyRef& WwiseRef) const override;
	virtual bool AddToDelete(FWwiseReconcileItem& Item) override;
	virtual bool AddToCreate(FWwiseReconcileItem& Item) override;
	virtual bool AddToRename(FWwiseReconcileItem& Item) override;
	virtual bool AddToUpdate(FWwiseReconcileItem& Item) override;
};
