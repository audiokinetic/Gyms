/*******************************************************************************
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
*******************************************************************************/
namespace Wwise.AkCommandBuffer
{

    public enum AKRESULT : int
    {
        AK_NotImplemented = 0,
        AK_Success = 1,
        AK_Fail = 2,
        AK_PartialSuccess = 3,
        AK_NotCompatible = 4,
        AK_AlreadyConnected = 5,
        AK_InvalidFile = 7,
        AK_AudioFileHeaderTooLarge = 8,
        AK_MaxReached = 9,
        AK_InvalidID = 14,
        AK_IDNotFound = 15,
        AK_InvalidInstanceID = 16,
        AK_NoMoreData = 17,
        AK_InvalidStateGroup = 20,
        AK_ChildAlreadyHasAParent = 21,
        AK_InvalidLanguage = 22,
        AK_CannotAddItselfAsAChild = 23,
        AK_InvalidParameter = 31,
        AK_ElementAlreadyInList = 35,
        AK_PathNotFound = 36,
        AK_PathNoVertices = 37,
        AK_PathNotRunning = 38,
        AK_PathNotPaused = 39,
        AK_PathNodeAlreadyInList = 40,
        AK_PathNodeNotInList = 41,
        AK_DataNeeded = 43,
        AK_NoDataNeeded = 44,
        AK_DataReady = 45,
        AK_NoDataReady = 46,
        AK_InsufficientMemory = 52,
        AK_Cancelled = 53,
        AK_UnknownBankID = 54,
        AK_BankReadError = 56,
        AK_InvalidSwitchType = 57,
        AK_FormatNotReady = 63,
        AK_WrongBankVersion = 64,
        AK_FileNotFound = 66,
        AK_DeviceNotReady = 67,
        AK_BankAlreadyLoaded = 69,
        AK_RenderedFX = 71,
        AK_ProcessNeeded = 72,
        AK_ProcessDone = 73,
        AK_MemManagerNotInitialized = 74,
        AK_StreamMgrNotInitialized = 75,
        AK_SSEInstructionsNotSupported = 76,
        AK_Busy = 77,
        AK_UnsupportedChannelConfig = 78,
        AK_PluginMediaNotAvailable = 79,
        AK_MustBeVirtualized = 80,
        AK_CommandTooLarge = 81,
        AK_RejectedByFilter = 82,
        AK_InvalidCustomPlatformName = 83,
        AK_DLLCannotLoad = 84,
        AK_DLLPathNotFound = 85,
        AK_NoJavaVM = 86,
        AK_OpenSLError = 87,
        AK_PluginNotRegistered = 88,
        AK_DataAlignmentError = 89,
        AK_DeviceNotCompatible = 90,
        AK_DuplicateUniqueID = 91,
        AK_InitBankNotLoaded = 92,
        AK_DeviceNotFound = 93,
        AK_PlayingIDNotFound = 94,
        AK_InvalidFloatValue = 95,
        AK_FileFormatMismatch = 96,
        AK_NoDistinctListener = 97,
        AK_ResourceInUse = 99,
        AK_InvalidBankType = 100,
        AK_AlreadyInitialized = 101,
        AK_NotInitialized = 102,
        AK_FilePermissionError = 103,
        AK_UnknownFileError = 104,
        AK_TooManyConcurrentOperations = 105,
        AK_InvalidFileSize = 106,
        AK_Deferred = 107,
        AK_FilePathTooLong = 108,
        AK_InvalidState = 109,
        AKRESULT_Last = 110,
    }

    public enum AkGroupType : int
    {
        AkGroupType_Switch = 0,
        AkGroupType_State = 1,
    }

    public enum AkAudioDeviceState : int
    {
        AkDeviceState_Unknown = 0,
        AkDeviceState_Active = 1,
        AkDeviceState_Disabled = 2,
        AkDeviceState_NotPresent = 4,
        AkDeviceState_Unplugged = 8,
        AkDeviceState_Last = 9,
        AkDeviceState_All = 15,
    }

    public enum AkConnectionType : int
    {
        ConnectionType_Direct = 0,
        ConnectionType_GameDefSend = 1,
        ConnectionType_UserDefSend = 2,
        ConnectionType_ReflectionsSend = 3,
        ConnectionType_Last = 4,
    }

    public enum AkAttenuationCurveType : int
    {
        AttenuationCurveID_VolumeDry = 0,
        AttenuationCurveID_VolumeAuxGameDef = 1,
        AttenuationCurveID_VolumeAuxUserDef = 2,
        AttenuationCurveID_LowPassFilter = 3,
        AttenuationCurveID_HighPassFilter = 4,
        AttenuationCurveID_HighShelf = 5,
        AttenuationCurveID_Spread = 6,
        AttenuationCurveID_Focus = 7,
        AttenuationCurveID_ObstructionVolume = 8,
        AttenuationCurveID_ObstructionLPF = 9,
        AttenuationCurveID_ObstructionHPF = 10,
        AttenuationCurveID_ObstructionHSF = 11,
        AttenuationCurveID_OcclusionVolume = 12,
        AttenuationCurveID_OcclusionLPF = 13,
        AttenuationCurveID_OcclusionHPF = 14,
        AttenuationCurveID_OcclusionHSF = 15,
        AttenuationCurveID_DiffractionVolume = 16,
        AttenuationCurveID_DiffractionLPF = 17,
        AttenuationCurveID_DiffractionHPF = 18,
        AttenuationCurveID_DiffractionHSF = 19,
        AttenuationCurveID_TransmissionVolume = 20,
        AttenuationCurveID_TransmissionLPF = 21,
        AttenuationCurveID_TransmissionHPF = 22,
        AttenuationCurveID_TransmissionHSF = 23,
        AttenuationCurveID_MaxCount = 24,
        AttenuationCurveID_Project = 254,
        AttenuationCurveID_None = 255,
    }

    public enum AkCurveInterpolation : int
    {
        AkCurveInterpolation_Log3 = 0,
        AkCurveInterpolation_Sine = 1,
        AkCurveInterpolation_Log1 = 2,
        AkCurveInterpolation_InvSCurve = 3,
        AkCurveInterpolation_Linear = 4,
        AkCurveInterpolation_SCurve = 5,
        AkCurveInterpolation_Exp1 = 6,
        AkCurveInterpolation_SineRecip = 7,
        AkCurveInterpolation_Exp3 = 8,
        AkCurveInterpolation_LastFadeCurve = 8,
        AkCurveInterpolation_Constant = 9,
        AkCurveInterpolation_Last = 10,
    }

    public enum AkBankTypeEnum : int
    {
        AkBankType_User = 0,
        AkBankType_Event = 30,
        AkBankType_Bus = 31,
        AkBankType_Last = 32,
    }

    public enum AkSpeakerPanningType : int
    {
        AK_DirectSpeakerAssignment = 0,
        AK_BalanceFadeHeight = 1,
        AK_SteeringPanner = 2,
        AkSpeakerPanning_Last = 3,
    }

    public enum Ak3DPositionType : int
    {
        AK_3DPositionType_Emitter = 0,
        AK_3DPositionType_EmitterWithAutomation = 1,
        AK_3DPositionType_ListenerWithAutomation = 2,
        AK_3DPositionType_Last = 3,
    }

    public enum AkPanningRule : int
    {
        AkPanningRule_Speakers = 0,
        AkPanningRule_Headphones = 1,
        AkPanningRule_Last = 2,
    }

    public enum Ak3DSpatializationMode : int
    {
        AK_SpatializationMode_None = 0,
        AK_SpatializationMode_PositionOnly = 1,
        AK_SpatializationMode_PositionAndOrientation = 2,
        AK_SpatializationMode_Last = 3,
    }

    [System.Flags]
    public enum AkMeteringFlags : int
    {
        AK_NoMetering = 0b0,
        AK_EnableBusMeter_Peak = 0b1,
        AK_EnableBusMeter_TruePeak = 0b10,
        AK_EnableBusMeter_RMS = 0b100,
        AK_EnableBusMeter_KPower = 0b10000,
        AK_EnableBusMeter_3DMeter = 0b100000,
        AK_EnableBusMeter_Last = 0b100001,
    }

    public enum AkPluginType : int
    {
        AkPluginTypeNone = 0,
        AkPluginTypeCodec = 1,
        AkPluginTypeSource = 2,
        AkPluginTypeEffect = 3,
        AkPluginTypeMixer = 6,
        AkPluginTypeSink = 7,
        AkPluginTypeGlobalExtension = 8,
        AkPluginTypeMetadata = 9,
        AkPluginType_Last = 10,
        AkPluginTypeMask = 15,
    }

    public enum AkNodeType : int
    {
        AkNodeType_Default = 0,
        AkNodeType_Bus = 1,
        AkNodeType_AudioDevice = 2,
        AkNodeType_Last = 3,
    }

    public enum AkMultiPositionType : int
    {
        AkMultiPositionType_SingleSource = 0,
        AkMultiPositionType_MultiSources = 1,
        AkMultiPositionType_MultiDirections = 2,
        AkMultiPositionType_Last = 3,
    }

    [System.Flags]
    public enum AkSetPositionFlags : int
    {
        AkSetPositionFlags_Emitter = 0b1,
        AkSetPositionFlags_Listener = 0b10,
        AkSetPositionFlags_Default = 0b11,
    }

    public enum AkListenerOp : int
    {
        AkListenerOp_Set = 0,
        AkListenerOp_Add = 1,
        AkListenerOp_Remove = 2,
    }

    public enum AkActionOnEventType : int
    {
        AkActionOnEventType_Stop = 0,
        AkActionOnEventType_Pause = 1,
        AkActionOnEventType_Resume = 2,
        AkActionOnEventType_Break = 3,
        AkActionOnEventType_ReleaseEnvelope = 4,
        AkActionOnEventType_Last = 5,
    }

    public enum AkDynamicSequenceType : int
    {
        AkDynamicSequenceType_SampleAccurate = 0,
        AkDynamicSequenceType_NormalTransition = 1,
        AkDynamicSequenceType_Last = 2,
    }

    public enum AkDynamicSequenceOp : int
    {
        AkDynamicSequenceOp_Play = 0,
        AkDynamicSequenceOp_Pause = 1,
        AkDynamicSequenceOp_Resume = 2,
        AkDynamicSequenceOp_Stop = 3,
        AkDynamicSequenceOp_Break = 4,
        AkDynamicSequenceOp_Close = 5,
        AkDynamicSequenceOp_Last = 6,
    }

    public enum AkChannelConfigType : int
    {
        AK_ChannelConfigType_Anonymous = 0,
        AK_ChannelConfigType_Standard = 1,
        AK_ChannelConfigType_Ambisonic = 2,
        AK_ChannelConfigType_Objects = 3,
        AK_ChannelConfigType_Last = 4,
        AK_ChannelConfigType_UseDeviceMain = 14,
        AK_ChannelConfigType_UseDevicePassthrough = 15,
    }

    public enum AkMotionDeviceType : int
    {
        AkMotionDeviceType_Controller = 0,
        AkMotionDeviceType_Mobile = 1,
        AkMotionDeviceType_Last = 2,
    }

    public enum AkMotionInputProfile : int
    {
        AkMotionInputProfile_GenericRumble = 0,
        AkMotionInputProfile_GenericLoResHaptics = 1,
        AkMotionInputProfile_GenericHiResHaptics = 2,
        AkMotionInputProfile_Last = 3,
    }

    public enum AkEngineRenderingMode : int
    {
        AkEngineRenderingMode_Online = 0,
        AkEngineRenderingMode_Offline = 1,
        AkEngineRenderingMode_OfflineThreaded = 2,
    }

    public static class AkEnumsConstants
    {

        public const int AKCURVEINTERPOLATION_NUM_STORAGE_BIT = 5;

        public const int AK_PANNER_NUM_STORAGE_BITS = 3;

        public const int AK_POSSOURCE_NUM_STORAGE_BITS = 3;

        public const int AK_SPAT_NUM_STORAGE_BITS = 3;

        public const int AK_MAX_BITS_METERING_FLAGS = 5;
    }
}