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

#include "Wwise/WwiseUnitTests.h"
#if WWISE_UNIT_TESTS && UE_EDITOR

#include "Wwise/Packaging/WwiseAssetLibraryFilter.h"
#include "Wwise/Packaging/WwiseAssetLibraryFilteringSharedData.h"
#include "Wwise/Packaging/WwiseAssetLibraryProcessor.h"
#include "Wwise/Packaging/Filters/WwiseAssetLibraryFilterLanguage.h"

#include <array>

WWISE_TEST_CASE(AssetFilter_Language, "Wwise::AssetLibraryEditor::AssetFilter_Language", "[ApplicationContextMask][SmokeFilter]")
{
	SECTION("Test setup")
	{
		static const FString ExpectedProjectName(TEXT("Gyms"));
		if(UNLIKELY(FString(FApp::GetProjectName()) != ExpectedProjectName))
		{
			WWISE_TEST_LOG("FAppProjectName %s != ExpectedProjectName %s. Skipping test.", FApp::GetProjectName(), *ExpectedProjectName);
			return;
		}
	}

	// Setup for all tests
#ifdef WITH_WWISE_PROJECT_DATABASE
	const WwiseDBString FrenchDbString{ "fr_FR"_wwise_db };
	const WwiseDBString EnglishDbString{ "en_US"_wwise_db };
	const WwiseDBString InvalidDbString;
	const auto French{ *FrenchDbString };
	const auto English{ *EnglishDbString };
	const auto Invalid{ *InvalidDbString };
#else
	const FName French("fr_FR");
	const FName English("en_US");
	const FName Invalid;
#endif

	// By design, Gyms start with 5 assets per language. This value can differ depending on the Gyms version.
	constexpr uint32 NumFrenchAssets = 5;
	constexpr uint32 NumEnglishAssets = 5;

	uint32 FrenchID = AK_INVALID_UNIQUE_ID;
	uint32 EnglishID = AK_INVALID_UNIQUE_ID;
	auto ProjectDB = FWwiseProjectDatabase::Get();
	if (UNLIKELY(!ProjectDB))
	{
		WWISE_TEST_LOG("%s: No ProjectDB. Skipping test.", TEXT("AssetFilter_Language"));
		return;
	}
	
	if(UNLIKELY(!ProjectDB->IsProjectDatabaseParsed()))
	{
		WWISE_TEST_LOG("%s: No generated SoundBanks. Skipping test.", TEXT("AssetFilter_Language"));
		return;
	}
	
	SECTION("Initialize ProjectDB")
	{
#ifdef WITH_WWISE_PROJECT_DATABASE
		const WwiseDataStructureScopeLock DataStructure(*ProjectDB);
		FrenchID = DataStructure.GetLanguageId(FrenchDbString);
		EnglishID = DataStructure.GetLanguageId(EnglishDbString);
#else
		const FWwiseDataStructureScopeLock DataStructure(*ProjectDB);
		FrenchID = DataStructure.GetLanguageId(French);
		EnglishID = DataStructure.GetLanguageId(English);
#endif
		CHECK(FrenchID != AK_INVALID_UNIQUE_ID);
		CHECK(EnglishID != AK_INVALID_UNIQUE_ID);
	}

	// Filtering requires a FWwiseAssetLibraryProcessor and a FWwiseAssetLibraryFilteringSharedData
	FWwiseAssetLibraryProcessor* Processor = FWwiseAssetLibraryProcessor::Get();
	TUniquePtr<FWwiseAssetLibraryFilteringSharedData> FilteringSharedData;
	if(Processor)
	{
		FilteringSharedData = TUniquePtr<FWwiseAssetLibraryFilteringSharedData>(Processor->InstantiateSharedData(*ProjectDB));
		Processor->RetrieveAssetMap(*FilteringSharedData);
	}

	// Construct the test objects
	FWwiseAssetLibraryInfo LibraryInfo;
	auto* Filter = NewObject<UWwiseAssetLibraryFilterLanguage>(GetTransientPackage());
	FWwiseAssetLibraryProcessor::GetRelevantAssets(TEXT("/Game/"), FilteringSharedData->AssetsData);
	FilteringSharedData->bConsiderAssetsData = false;
	SECTION("Initialize AssetLibraryProcessor, Filtering SharedData and filter")
	{
		CHECK(Processor);
		CHECK(FilteringSharedData.IsValid());
		CHECK(FilteringSharedData->Sources.Num() > 0);
		CHECK(Filter);
	}

	SECTION("None Filter")
	{
		LibraryInfo.Filters.Add(nullptr);
		LibraryInfo.FilteredAssets.Empty();
		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		CHECK(LibraryInfo.FilteredAssets.Num() == (FilteringSharedData->Sources.Num()-FilteringSharedData->SkippedAssetsCount));
	}

	// Set up Shared Testing Filter. This is used for all the tests below
	LibraryInfo.Filters.Empty();
	LibraryInfo.Filters.Add(Filter);
	
	SECTION("One Language Filter")
	{
		Filter->SelectedLanguages.Empty();
		LibraryInfo.FilteredAssets.Empty();
		
		Filter->SelectedLanguages.Add(French);

		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		CHECK(LibraryInfo.FilteredAssets.Num() >= NumFrenchAssets)
		for(const auto FilteredAsset : LibraryInfo.FilteredAssets)
		{
			CHECK(FilteredAsset.LanguageId == FrenchID);
		}
	}
	
	SECTION("Two Languages Filter")
	{
		Filter->SelectedLanguages.Empty();
		LibraryInfo.FilteredAssets.Empty();

		Filter->SelectedLanguages.Add(French);
		Filter->SelectedLanguages.Add(English);

		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		CHECK(LibraryInfo.FilteredAssets.Num() >= NumFrenchAssets + NumEnglishAssets)
		for(const auto FilteredAsset : LibraryInfo.FilteredAssets)
		{
			CHECK(FilteredAsset.LanguageId == FrenchID || FilteredAsset.LanguageId == EnglishID);
		}
	}

	SECTION("Invalid Filter")
	{
		Filter->SelectedLanguages.Empty();
		LibraryInfo.FilteredAssets.Empty();

		Filter->SelectedLanguages.Add(Invalid);
		
		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		CHECK(LibraryInfo.FilteredAssets.Num() == 0)
	}

	SECTION("Empty Filter")
	{
		Filter->SelectedLanguages.Empty();
		LibraryInfo.FilteredAssets.Empty();

		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		int EmptyFilterCount = LibraryInfo.FilteredAssets.Num();
		
		Filter->SelectedLanguages.Empty();
		Filter->SelectedLanguages.Add("SFX");
		LibraryInfo.FilteredAssets.Empty();

		Processor->FilterLibraryAssets(*FilteringSharedData, LibraryInfo, false);
		
		CHECK(LibraryInfo.FilteredAssets.Num() == EmptyFilterCount);
	}
}

WWISE_TEST_CASE(AssetMaps_MediaAndSoundBanks, "Wwise::AssetLibraryEditor::AssetMaps_MediaAndSoundBanks", "[ApplicationContextMask][SmokeFilter]")
{
	SECTION("Test setup")
	{
		static const FString ExpectedProjectName(TEXT("Gyms"));
		if (UNLIKELY(FString(FApp::GetProjectName()) != ExpectedProjectName))
		{
			WWISE_TEST_LOG("FAppProjectName %s != ExpectedProjectName %s. Skipping test.", FApp::GetProjectName(),
			               *ExpectedProjectName);
			return;
		}
	}

	// Setup for all tests
#ifdef WITH_WWISE_PROJECT_DATABASE
	const WwiseDBString FrenchDbString{"fr_FR"_wwise_db};
	const WwiseDBString EnglishDbString{"en_US"_wwise_db};
	const WwiseDBString InvalidDbString;
	const auto French{*FrenchDbString};
	const auto English{*EnglishDbString};
	const auto Invalid{*InvalidDbString};
#else
	const FName French("fr_FR");
	const FName English("en_US");
	const FName Invalid;
#endif

	// By design, Gyms start with 5 assets per language. This value can differ depending on the Gyms version.
	constexpr uint32 NumFrenchAssets = 5;
	constexpr uint32 NumEnglishAssets = 5;

	uint32 FrenchID = AK_INVALID_UNIQUE_ID;
	uint32 EnglishID = AK_INVALID_UNIQUE_ID;
	auto ProjectDB = FWwiseProjectDatabase::Get();
	if (UNLIKELY(!ProjectDB))
	{
		WWISE_TEST_LOG("%s: No ProjectDB. Skipping test.", TEXT("AssetFilter_Language"));
		return;
	}

	if (UNLIKELY(!ProjectDB->IsProjectDatabaseParsed()))
	{
		WWISE_TEST_LOG("%s: No generated SoundBanks. Skipping test.", TEXT("AssetFilter_Language"));
		return;
	}

	SECTION("Initialize ProjectDB")
	{
#ifdef WITH_WWISE_PROJECT_DATABASE
		const WwiseDataStructureScopeLock DataStructure(*ProjectDB);
		FrenchID = DataStructure.GetLanguageId(FrenchDbString);
		EnglishID = DataStructure.GetLanguageId(EnglishDbString);
#else
		const FWwiseDataStructureScopeLock DataStructure(*ProjectDB);
		FrenchID = DataStructure.GetLanguageId(French);
		EnglishID = DataStructure.GetLanguageId(English);
#endif
		CHECK(FrenchID != AK_INVALID_UNIQUE_ID);
		CHECK(EnglishID != AK_INVALID_UNIQUE_ID);
	}

	// Filtering requires a FWwiseAssetLibraryProcessor and a FWwiseAssetLibraryFilteringSharedData
	FWwiseAssetLibraryProcessor* Processor = FWwiseAssetLibraryProcessor::Get();
	TUniquePtr<FWwiseAssetLibraryFilteringSharedData> FilteringSharedData;
	if (Processor)
	{
		FilteringSharedData = TUniquePtr<FWwiseAssetLibraryFilteringSharedData>(
			Processor->InstantiateSharedData(*ProjectDB));
		Processor->RetrieveAssetMap(*FilteringSharedData);
	}
	TArray<FAssetData> AssetsData;
	// Assets relevant to testing are based in Game/WwiseAudio
	FWwiseAssetLibraryProcessor::GetRelevantAssets("/Game/WwiseAudio/", AssetsData);
	FilteringSharedData->AssetsData = AssetsData;

	TUniquePtr<FWwiseAssetLibraryFilteringSharedData> FilteringSharedMediaData;
	FilteringSharedMediaData = TUniquePtr<FWwiseAssetLibraryFilteringSharedData>(
		Processor->InstantiateSharedData(*ProjectDB));

	Processor->RetrieveMediaMap(*FilteringSharedMediaData);

	TUniquePtr<FWwiseAssetLibraryFilteringSharedData> FilteringSharedSoundBankData;
	FilteringSharedSoundBankData = TUniquePtr<FWwiseAssetLibraryFilteringSharedData>(
		Processor->InstantiateSharedData(*ProjectDB));

	Processor->RetrieveSoundBankMap(*FilteringSharedSoundBankData);

	SECTION("Valid Media")
	{
		TSet<WwiseDBShortId> ExistingKeys;
		for (const auto& Source: FilteringSharedMediaData->Sources)
		{
			CHECK(Source.GetMedia());
		}
	}

	SECTION("Valid SoundBanks")
	{
		TSet<WwiseDBShortId> ExistingKeys;
		for (const auto& Source: FilteringSharedSoundBankData->Sources)
		{
			CHECK(Source.GetSoundBank());
		}
	}

	SECTION("Only Media In Media Map")
	{
		for (const auto& Source: FilteringSharedMediaData->Sources)
		{
			CHECK(Source.GetType() == WwiseRefType::Media)
		}
	}

	SECTION("Only SoundBanks In SoundBank Map")
	{
		for (const auto& Source: FilteringSharedSoundBankData->Sources)
		{
			CHECK(Source.GetType() == WwiseRefType::SoundBank)
		}
	}

	SECTION("Memory Media Filtered Out")
	{
		for (const auto& Source: FilteringSharedMediaData->Sources)
		{
			auto Media = Source.GetMedia();
			if (Media)
			{
				CHECK(Media->bStreaming || Media->Location == WwiseMetadataMediaLocation::Loose)
			}
		}
	}
}

#endif // WWISE_UNIT_TESTS