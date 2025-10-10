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

    public delegate void AkCommandCallbackFunc(System.IntPtr in_pCookie);


    public enum AkCommand : int
    {
        AkCommand_EndOfBuffer = 0,
        AkCommand_PostEvent = 1,
        AkCommand_RegisterGameObject = 2,
        AkCommand_UnregisterGameObject = 3,
        AkCommand_Callback = 4,
        AkCommand_SetRTPC = 5,
        AkCommand_ResetRTPC = 6,
        AkCommand_SetPosition = 7,
        AkCommand_SetListeners = 8,
        AkCommand_SetDefaultListeners = 9,
        AkCommand_ResetListeners = 10,
        AkCommand_SetListenerSpatialization = 11,
        AkCommand_SetGameObjectAuxSendValues = 12,
        AkCommand_SetGameObjectOutputBusVolume = 13,
        AkCommand_SetObjectObstructionAndOcclusion = 14,
        AkCommand_SetMultipleObstructionAndOcclusion = 15,
        AkCommand_SetScalingFactor = 16,
        AkCommand_SetMultiplePositions = 17,
        AkCommand_SetDistanceProbe = 18,
        AkCommand_StopAll = 19,
        AkCommand_ExecuteActionOnEvent = 20,
        AkCommand_ExecuteActionOnPlayingID = 21,
        AkCommand_SeekOnEvent = 22,
        AkCommand_SetState = 23,
        AkCommand_SetSwitch = 24,
        AkCommand_PostTrigger = 25,
        AkCommand_PostMIDIOnEvent = 26,
        AkCommand_StopMIDIOnEvent = 27,
        AkCommand_DynamicSequence_Open = 28,
        AkCommand_DynamicSequence_Op = 29,
        AkCommand_DynamicSequence_Seek = 30,
        AkCommand_AddOutput = 31,
        AkCommand_RemoveOutput = 32,
        AkCommand_ReplaceOutput = 33,
        AkCommand_SetBusAudioDevice = 34,
        AkCommand_SetBusConfig = 35,
        AkCommand_ResetBusConfig = 36,
        AkCommand_SetEffect = 37,
        AkCommand_SetOutputVolume = 38,
        AkCommand_SetPanningRule = 39,
        AkCommand_SetSpeakerAngles = 40,
        AkCommand_ControlOutputCapture = 41,
        AkCommand_AddOutputCaptureMarker = 42,
        AkCommand_ControlOfflineRendering = 43,
        AkCommand_SetRandomSeed = 44,
        AkCommand_ControlEventStreamCache = 45,
        AkCommand_ControlSuspendedState = 46,
        AkCommand_MuteBackgroundMusic = 47,
        AkCommand_SendPluginCustomGameData = 48,
        AkCommand_SetSidechainMixConfig = 49,
        AkCommand_ResetGlobalValues = 50,
        AkCommand_SA_Begin = 51,
        AkCommand_SA_RegisterListener = 51,
        AkCommand_SA_UnregisterListener = 52,
        AkCommand_SA_SetImageSource = 53,
        AkCommand_SA_RemoveImageSource = 54,
        AkCommand_SA_ClearImageSources = 55,
        AkCommand_SA_SetGeometry = 56,
        AkCommand_SA_RemoveGeometry = 57,
        AkCommand_SA_SetGeometryInstance = 58,
        AkCommand_SA_RemoveGeometryInstance = 59,
        AkCommand_SA_SetRoom = 60,
        AkCommand_SA_RemoveRoom = 61,
        AkCommand_SA_SetPortal = 62,
        AkCommand_SA_RemovePortal = 63,
        AkCommand_SA_SetPortalObstructionAndOcclusion = 64,
        AkCommand_SA_SetGameObjectToPortalObstruction = 65,
        AkCommand_SA_SetPortalToPortalObstruction = 66,
        AkCommand_SA_SetReverbZone = 67,
        AkCommand_SA_RemoveReverbZone = 68,
        AkCommand_SA_SetGameObjectInRoom = 69,
        AkCommand_SA_UnsetGameObjectInRoom = 70,
        AkCommand_SA_SetGameObjectRadius = 71,
        AkCommand_SA_SetAdjacentRoomBleed = 72,
        AkCommand_SA_SetEarlyReflectionsAuxSend = 73,
        AkCommand_SA_SetEarlyReflectionsVolume = 74,
        AkCommand_SA_SetReflectionsOrder = 75,
        AkCommand_SA_SetDiffractionOrder = 76,
        AkCommand_SA_SetMaxGlobalReflectionPaths = 77,
        AkCommand_SA_SetMaxEmitterRoomAuxSends = 78,
        AkCommand_SA_SetMaxDiffractionPaths = 79,
        AkCommand_SA_SetSmoothingConstant = 80,
        AkCommand_SA_SetTransmissionOperation = 81,
        AkCommand_SA_SetNumberOfPrimaryRays = 82,
        AkCommand_SA_SetLoadBalancingSpread = 83,
        AkCommand_SA_ResetStochasticEngine = 84,
        AkCommand_SA_End = 85,
        AkCommand_NUM = 85,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=44, Pack=4)]
    public struct AkCmd_PostEvent: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_PostEvent;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _actionTargetPlayingID;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal uint _flags;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal System.IntPtr _callback;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal System.IntPtr _callbackCookie;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal uint _numExternalSources;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint ActionTargetPlayingID
        {
            get
            {
                return _actionTargetPlayingID;
            }
            set
            {
                _actionTargetPlayingID = value;
            }
        }

        public uint Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public AkEventCallbackFunc Callback
        {
            set
            {
                _callback = value != null ? System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(value): System.IntPtr.Zero;
            }
        }

        public System.IntPtr CallbackCookie
        {
            get
            {
                return _callbackCookie;
            }
            set
            {
                _callbackCookie = value;
            }
        }

        public uint NumExternalSources
        {
            get
            {
                return _numExternalSources;
            }
            set
            {
                _numExternalSources = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_RegisterGameObject: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_RegisterGameObject;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_UnregisterGameObject: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_UnregisterGameObject;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_Callback: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_Callback;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal System.IntPtr _callback;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _callbackCookie;

        public AkCommandCallbackFunc Callback
        {
            set
            {
                _callback = value != null ? System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(value): System.IntPtr.Zero;
            }
        }

        public System.IntPtr CallbackCookie
        {
            get
            {
                return _callbackCookie;
            }
            set
            {
                _callbackCookie = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=28, Pack=4)]
    public struct AkCmd_SetRTPC: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetRTPC;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _rtpcID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _rtpcValue;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal int _transitionTime;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal ushort _fadeCurve;
        [System.Runtime.InteropServices.FieldOffset(26)]
        internal ushort _bypassInternalValueInterpolation;

        public uint RtpcID
        {
            get
            {
                return _rtpcID;
            }
            set
            {
                _rtpcID = value;
            }
        }

        public float RtpcValue
        {
            get
            {
                return _rtpcValue;
            }
            set
            {
                _rtpcValue = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }

        public ushort FadeCurve
        {
            get
            {
                return _fadeCurve;
            }
            set
            {
                _fadeCurve = value;
            }
        }

        public ushort BypassInternalValueInterpolation
        {
            get
            {
                return _bypassInternalValueInterpolation;
            }
            set
            {
                _bypassInternalValueInterpolation = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCmd_ResetRTPC: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ResetRTPC;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _rtpcID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal int _transitionTime;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal ushort _fadeCurve;
        [System.Runtime.InteropServices.FieldOffset(22)]
        internal ushort _bypassInternalValueInterpolation;

        public uint RtpcID
        {
            get
            {
                return _rtpcID;
            }
            set
            {
                _rtpcID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }

        public ushort FadeCurve
        {
            get
            {
                return _fadeCurve;
            }
            set
            {
                _fadeCurve = value;
            }
        }

        public ushort BypassInternalValueInterpolation
        {
            get
            {
                return _bypassInternalValueInterpolation;
            }
            set
            {
                _bypassInternalValueInterpolation = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=60, Pack=4)]
    public struct AkCmd_SetPosition: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetPosition;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkWorldTransform _position;
        [System.Runtime.InteropServices.FieldOffset(56)]
        internal uint _flags;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public AkWorldTransform Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public uint Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SetMultiplePositions: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetMultiplePositions;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _flags;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _multiPositionType;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _numPositions;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public uint MultiPositionType
        {
            get
            {
                return _multiPositionType;
            }
            set
            {
                _multiPositionType = value;
            }
        }

        public uint NumPositions
        {
            get
            {
                return _numPositions;
            }
            set
            {
                _numPositions = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetListeners: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetListeners;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _operation;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _numListenerIDs;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        public uint NumListenerIDs
        {
            get
            {
                return _numListenerIDs;
            }
            set
            {
                _numListenerIDs = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SetDefaultListeners: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetDefaultListeners;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _operation;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _numListenerIDs;

        public uint Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        public uint NumListenerIDs
        {
            get
            {
                return _numListenerIDs;
            }
            set
            {
                _numListenerIDs = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_ResetListeners: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ResetListeners;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetListenerSpatialization: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetListenerSpatialization;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _listenerID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkChannelConfig _channelConfig;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _isSpatialized;

        public ulong ListenerID
        {
            get
            {
                return _listenerID;
            }
            set
            {
                _listenerID = value;
            }
        }

        public AkChannelConfig ChannelConfig
        {
            get
            {
                return _channelConfig;
            }
            set
            {
                _channelConfig = value;
            }
        }

        public uint IsSpatialized
        {
            get
            {
                return _isSpatialized;
            }
            set
            {
                _isSpatialized = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SetGameObjectAuxSendValues: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetGameObjectAuxSendValues;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _numValues;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint NumValues
        {
            get
            {
                return _numValues;
            }
            set
            {
                _numValues = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SetGameObjectOutputBusVolume: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetGameObjectOutputBusVolume;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _emitterID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _listenerID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal float _controlValue;

        public ulong EmitterID
        {
            get
            {
                return _emitterID;
            }
            set
            {
                _emitterID = value;
            }
        }

        public ulong ListenerID
        {
            get
            {
                return _listenerID;
            }
            set
            {
                _listenerID = value;
            }
        }

        public float ControlValue
        {
            get
            {
                return _controlValue;
            }
            set
            {
                _controlValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SetScalingFactor: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetScalingFactor;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _scalingFactor;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public float ScalingFactor
        {
            get
            {
                return _scalingFactor;
            }
            set
            {
                _scalingFactor = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCmd_SetObjectObstructionAndOcclusion: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetObjectObstructionAndOcclusion;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _emitterID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _listenerID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal AkObstructionOcclusionValues _value;

        public ulong EmitterID
        {
            get
            {
                return _emitterID;
            }
            set
            {
                _emitterID = value;
            }
        }

        public ulong ListenerID
        {
            get
            {
                return _listenerID;
            }
            set
            {
                _listenerID = value;
            }
        }

        public AkObstructionOcclusionValues Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SetMultipleObstructionAndOcclusion: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetMultipleObstructionAndOcclusion;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _emitterID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _listenerID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _numValues;

        public ulong EmitterID
        {
            get
            {
                return _emitterID;
            }
            set
            {
                _emitterID = value;
            }
        }

        public ulong ListenerID
        {
            get
            {
                return _listenerID;
            }
            set
            {
                _listenerID = value;
            }
        }

        public uint NumValues
        {
            get
            {
                return _numValues;
            }
            set
            {
                _numValues = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetDistanceProbe: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetDistanceProbe;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _distanceProbeID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public ulong DistanceProbeID
        {
            get
            {
                return _distanceProbeID;
            }
            set
            {
                _distanceProbeID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_StopAll: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_StopAll;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCmd_ExecuteActionOnEvent: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ExecuteActionOnEvent;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal int _transitionTime;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal byte _fadeCurve;
        [System.Runtime.InteropServices.FieldOffset(21)]
        internal byte _actionType;

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public byte FadeCurve
        {
            get
            {
                return _fadeCurve;
            }
            set
            {
                _fadeCurve = value;
            }
        }

        public byte ActionType
        {
            get
            {
                return _actionType;
            }
            set
            {
                _actionType = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_ExecuteActionOnPlayingID: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ExecuteActionOnPlayingID;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _actionType;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal int _transitionTime;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal byte _fadeCurve;

        public uint ActionType
        {
            get
            {
                return _actionType;
            }
            set
            {
                _actionType = value;
            }
        }

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }

        public byte FadeCurve
        {
            get
            {
                return _fadeCurve;
            }
            set
            {
                _fadeCurve = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCmd_SeekOnEvent: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SeekOnEvent;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal int _position__absolute;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _position__relative;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal byte _isPositionRelative;
        [System.Runtime.InteropServices.FieldOffset(21)]
        internal byte _seekToNearestMarker;

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public int PositionAbsolute
        {
            get
            {
                return _position__absolute;
            }
            set
            {
                _position__absolute = value;
            }
        }

        public float PositionRelative
        {
            get
            {
                return _position__relative;
            }
            set
            {
                _position__relative = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public byte IsPositionRelative
        {
            get
            {
                return _isPositionRelative;
            }
            set
            {
                _isPositionRelative = value;
            }
        }

        public byte SeekToNearestMarker
        {
            get
            {
                return _seekToNearestMarker;
            }
            set
            {
                _seekToNearestMarker = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SetState: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetState;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _stateGroupID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _stateID;

        public uint StateGroupID
        {
            get
            {
                return _stateGroupID;
            }
            set
            {
                _stateGroupID = value;
            }
        }

        public uint StateID
        {
            get
            {
                return _stateID;
            }
            set
            {
                _stateID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetSwitch: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetSwitch;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _switchGroupID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _switchID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;

        public uint SwitchGroupID
        {
            get
            {
                return _switchGroupID;
            }
            set
            {
                _switchGroupID = value;
            }
        }

        public uint SwitchID
        {
            get
            {
                return _switchID;
            }
            set
            {
                _switchID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_PostTrigger: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_PostTrigger;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _triggerID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;

        public uint TriggerID
        {
            get
            {
                return _triggerID;
            }
            set
            {
                _triggerID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=44, Pack=4)]
    public struct AkCmd_PostMIDIOnEvent: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_PostMIDIOnEvent;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _flags;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal System.IntPtr _callback;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal System.IntPtr _callbackCookie;
        [System.Runtime.InteropServices.FieldOffset(36)]
        internal byte _isOffsetAbsolute;
        [System.Runtime.InteropServices.FieldOffset(37)]
        internal byte _isNewSequence;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal uint _numMidiPosts;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public AkEventCallbackFunc Callback
        {
            set
            {
                _callback = value != null ? System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(value): System.IntPtr.Zero;
            }
        }

        public System.IntPtr CallbackCookie
        {
            get
            {
                return _callbackCookie;
            }
            set
            {
                _callbackCookie = value;
            }
        }

        public byte IsOffsetAbsolute
        {
            get
            {
                return _isOffsetAbsolute;
            }
            set
            {
                _isOffsetAbsolute = value;
            }
        }

        public byte IsNewSequence
        {
            get
            {
                return _isNewSequence;
            }
            set
            {
                _isNewSequence = value;
            }
        }

        public uint NumMidiPosts
        {
            get
            {
                return _numMidiPosts;
            }
            set
            {
                _numMidiPosts = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_StopMIDIOnEvent: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_StopMIDIOnEvent;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=36, Pack=4)]
    public struct AkCmd_DynamicSequence_Open: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_DynamicSequence_Open;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal byte _type;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _flags;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal System.IntPtr _callback;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal System.IntPtr _callbackCookie;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public byte Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public uint Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public AkEventCallbackFunc Callback
        {
            set
            {
                _callback = value != null ? System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(value): System.IntPtr.Zero;
            }
        }

        public System.IntPtr CallbackCookie
        {
            get
            {
                return _callbackCookie;
            }
            set
            {
                _callbackCookie = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_DynamicSequence_Op: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_DynamicSequence_Op;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _operation;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal int _transitionTime;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal byte _fadeCurve;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public uint Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }

        public byte FadeCurve
        {
            get
            {
                return _fadeCurve;
            }
            set
            {
                _fadeCurve = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_DynamicSequence_Seek: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_DynamicSequence_Seek;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal int _position__absolute;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _position__relative;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal byte _isPositionRelative;
        [System.Runtime.InteropServices.FieldOffset(9)]
        internal byte _seekToNearestMarker;

        public uint PlayingID
        {
            get
            {
                return _playingID;
            }
            set
            {
                _playingID = value;
            }
        }

        public int PositionAbsolute
        {
            get
            {
                return _position__absolute;
            }
            set
            {
                _position__absolute = value;
            }
        }

        public float PositionRelative
        {
            get
            {
                return _position__relative;
            }
            set
            {
                _position__relative = value;
            }
        }

        public byte IsPositionRelative
        {
            get
            {
                return _isPositionRelative;
            }
            set
            {
                _isPositionRelative = value;
            }
        }

        public byte SeekToNearestMarker
        {
            get
            {
                return _seekToNearestMarker;
            }
            set
            {
                _seekToNearestMarker = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_AddOutput: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_AddOutput;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkOutputSettings _settings;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _numListenerIDs;

        public AkOutputSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public uint NumListenerIDs
        {
            get
            {
                return _numListenerIDs;
            }
            set
            {
                _numListenerIDs = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_RemoveOutput: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_RemoveOutput;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _outputDeviceID;

        public ulong OutputDeviceID
        {
            get
            {
                return _outputDeviceID;
            }
            set
            {
                _outputDeviceID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCmd_ReplaceOutput: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ReplaceOutput;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _outputDeviceID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkOutputSettings _settings;

        public ulong OutputDeviceID
        {
            get
            {
                return _outputDeviceID;
            }
            set
            {
                _outputDeviceID = value;
            }
        }

        public AkOutputSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SetBusAudioDevice: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetBusAudioDevice;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _busID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _audioDeviceSharesetID;

        public uint BusID
        {
            get
            {
                return _busID;
            }
            set
            {
                _busID = value;
            }
        }

        public uint AudioDeviceSharesetID
        {
            get
            {
                return _audioDeviceSharesetID;
            }
            set
            {
                _audioDeviceSharesetID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SetBusConfig: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetBusConfig;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _busID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkChannelConfig _channelConfig;

        public uint BusID
        {
            get
            {
                return _busID;
            }
            set
            {
                _busID = value;
            }
        }

        public AkChannelConfig ChannelConfig
        {
            get
            {
                return _channelConfig;
            }
            set
            {
                _channelConfig = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_ResetBusConfig: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ResetBusConfig;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _busID;

        public uint BusID
        {
            get
            {
                return _busID;
            }
            set
            {
                _busID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_ResetGlobalValues: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ResetGlobalValues;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _unused;

        public uint Unused
        {
            get
            {
                return _unused;
            }
            set
            {
                _unused = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SetSidechainMixConfig: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetSidechainMixConfig;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _sidechainMixID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkChannelConfig _channelConfig;

        public uint SidechainMixID
        {
            get
            {
                return _sidechainMixID;
            }
            set
            {
                _sidechainMixID = value;
            }
        }

        public AkChannelConfig ChannelConfig
        {
            get
            {
                return _channelConfig;
            }
            set
            {
                _channelConfig = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetEffect: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetEffect;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _nodeID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal byte _nodeType;
        [System.Runtime.InteropServices.FieldOffset(9)]
        internal byte _fxIndex;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _fxSharesetID;

        public ulong NodeID
        {
            get
            {
                return _nodeID;
            }
            set
            {
                _nodeID = value;
            }
        }

        public byte NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                _nodeType = value;
            }
        }

        public byte FxIndex
        {
            get
            {
                return _fxIndex;
            }
            set
            {
                _fxIndex = value;
            }
        }

        public uint FxSharesetID
        {
            get
            {
                return _fxSharesetID;
            }
            set
            {
                _fxSharesetID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SetOutputVolume: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetOutputVolume;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _outputID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _volume;

        public ulong OutputID
        {
            get
            {
                return _outputID;
            }
            set
            {
                _outputID = value;
            }
        }

        public float Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SetPanningRule: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetPanningRule;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _panningRule;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _outputID;

        public byte PanningRule
        {
            get
            {
                return _panningRule;
            }
            set
            {
                _panningRule = value;
            }
        }

        public ulong OutputID
        {
            get
            {
                return _outputID;
            }
            set
            {
                _outputID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SetSpeakerAngles: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetSpeakerAngles;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _numAngles;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _heightAngle;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _outputID;

        public uint NumAngles
        {
            get
            {
                return _numAngles;
            }
            set
            {
                _numAngles = value;
            }
        }

        public float HeightAngle
        {
            get
            {
                return _heightAngle;
            }
            set
            {
                _heightAngle = value;
            }
        }

        public ulong OutputID
        {
            get
            {
                return _outputID;
            }
            set
            {
                _outputID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_ControlOutputCapture: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ControlOutputCapture;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _isEnabled;

        public uint IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_AddOutputCaptureMarker: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_AddOutputCaptureMarker;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _markerDataSize;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _samplePos;

        public uint MarkerDataSize
        {
            get
            {
                return _markerDataSize;
            }
            set
            {
                _markerDataSize = value;
            }
        }

        public uint SamplePos
        {
            get
            {
                return _samplePos;
            }
            set
            {
                _samplePos = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_ControlOfflineRendering: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ControlOfflineRendering;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _isEnabled;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _frameTimeInSeconds;

        public uint IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }

        public float FrameTimeInSeconds
        {
            get
            {
                return _frameTimeInSeconds;
            }
            set
            {
                _frameTimeInSeconds = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SetRandomSeed: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SetRandomSeed;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _seedValue;

        public uint SeedValue
        {
            get
            {
                return _seedValue;
            }
            set
            {
                _seedValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_ControlEventStreamCache: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ControlEventStreamCache;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _eventID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal byte _isCached;
        [System.Runtime.InteropServices.FieldOffset(5)]
        internal sbyte _activePriority;
        [System.Runtime.InteropServices.FieldOffset(6)]
        internal sbyte _inactivePriority;

        public uint EventID
        {
            get
            {
                return _eventID;
            }
            set
            {
                _eventID = value;
            }
        }

        public byte IsCached
        {
            get
            {
                return _isCached;
            }
            set
            {
                _isCached = value;
            }
        }

        public sbyte ActivePriority
        {
            get
            {
                return _activePriority;
            }
            set
            {
                _activePriority = value;
            }
        }

        public sbyte InactivePriority
        {
            get
            {
                return _inactivePriority;
            }
            set
            {
                _inactivePriority = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_ControlSuspendedState: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_ControlSuspendedState;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _isSuspended;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _renderWhileSuspended;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal int _transitionTime;

        public byte IsSuspended
        {
            get
            {
                return _isSuspended;
            }
            set
            {
                _isSuspended = value;
            }
        }

        public byte RenderWhileSuspended
        {
            get
            {
                return _renderWhileSuspended;
            }
            set
            {
                _renderWhileSuspended = value;
            }
        }

        public int TransitionTime
        {
            get
            {
                return _transitionTime;
            }
            set
            {
                _transitionTime = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_MuteBackgroundMusic: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_MuteBackgroundMusic;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _isMuted;

        public uint IsMuted
        {
            get
            {
                return _isMuted;
            }
            set
            {
                _isMuted = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=28, Pack=4)]
    public struct AkCmd_SendPluginCustomGameData: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SendPluginCustomGameData;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _busID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _pluginType;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _companyID;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal uint _pluginID;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal uint _dataSize;

        public uint BusID
        {
            get
            {
                return _busID;
            }
            set
            {
                _busID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint PluginType
        {
            get
            {
                return _pluginType;
            }
            set
            {
                _pluginType = value;
            }
        }

        public uint CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }

        public uint PluginID
        {
            get
            {
                return _pluginID;
            }
            set
            {
                _pluginID = value;
            }
        }

        public uint DataSize
        {
            get
            {
                return _dataSize;
            }
            set
            {
                _dataSize = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_RegisterListener: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RegisterListener;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal byte _primaryListener;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public bool PrimaryListener
        {
            get
            {
                return _primaryListener != 0;
            }
            set
            {
                _primaryListener = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_UnregisterListener: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_UnregisterListener;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=80, Pack=4)]
    public struct AkCmd_SA_SetImageSource: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetImageSource;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _imageSourceID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkImageSourceSettings _info;
        [System.Runtime.InteropServices.FieldOffset(68)]
        internal uint _auxBusID;
        [System.Runtime.InteropServices.FieldOffset(72)]
        internal ulong _gameObjectID;

        public uint ImageSourceID
        {
            get
            {
                return _imageSourceID;
            }
            set
            {
                _imageSourceID = value;
            }
        }

        public AkImageSourceSettings Info
        {
            get
            {
                return _info;
            }
            set
            {
                _info = value;
            }
        }

        public uint AuxBusID
        {
            get
            {
                return _auxBusID;
            }
            set
            {
                _auxBusID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SA_RemoveImageSource: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemoveImageSource;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _imageSourceID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _auxBusID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _gameObjectID;

        public uint ImageSourceID
        {
            get
            {
                return _imageSourceID;
            }
            set
            {
                _imageSourceID = value;
            }
        }

        public uint AuxBusID
        {
            get
            {
                return _auxBusID;
            }
            set
            {
                _auxBusID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_ClearImageSources: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_ClearImageSources;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _auxBusID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;

        public uint AuxBusID
        {
            get
            {
                return _auxBusID;
            }
            set
            {
                _auxBusID = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_SetGeometry: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetGeometry;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _geometrySetID;

        public ulong GeometrySetID
        {
            get
            {
                return _geometrySetID;
            }
            set
            {
                _geometrySetID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_RemoveGeometry: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemoveGeometry;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _geometrySetID;

        public ulong GeometrySetID
        {
            get
            {
                return _geometrySetID;
            }
            set
            {
                _geometrySetID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=80, Pack=4)]
    public struct AkCmd_SA_SetGeometryInstance: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetGeometryInstance;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _geometryInstanceID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkGeometryInstanceParams _params;

        public ulong GeometryInstanceID
        {
            get
            {
                return _geometryInstanceID;
            }
            set
            {
                _geometryInstanceID = value;
            }
        }

        public AkGeometryInstanceParams Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_RemoveGeometryInstance: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemoveGeometryInstance;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _geometryInstanceID;

        public ulong GeometryInstanceID
        {
            get
            {
                return _geometryInstanceID;
            }
            set
            {
                _geometryInstanceID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=68, Pack=4)]
    public struct AkCmd_SA_SetRoom: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetRoom;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _roomID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkRoomParams _params;

        public ulong RoomID
        {
            get
            {
                return _roomID;
            }
            set
            {
                _roomID = value;
            }
        }

        public AkRoomParams Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_RemoveRoom: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemoveRoom;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _roomID;

        public ulong RoomID
        {
            get
            {
                return _roomID;
            }
            set
            {
                _roomID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=92, Pack=4)]
    public struct AkCmd_SA_SetPortal: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetPortal;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _portalID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkPortalParams _params;

        public ulong PortalID
        {
            get
            {
                return _portalID;
            }
            set
            {
                _portalID = value;
            }
        }

        public AkPortalParams Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SA_SetPortalObstructionAndOcclusion: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetPortalObstructionAndOcclusion;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _portalID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _obstruction;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal float _occlusion;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal byte _transition;

        public ulong PortalID
        {
            get
            {
                return _portalID;
            }
            set
            {
                _portalID = value;
            }
        }

        public float Obstruction
        {
            get
            {
                return _obstruction;
            }
            set
            {
                _obstruction = value;
            }
        }

        public float Occlusion
        {
            get
            {
                return _occlusion;
            }
            set
            {
                _occlusion = value;
            }
        }

        public bool Transition
        {
            get
            {
                return _transition != 0;
            }
            set
            {
                _transition = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SA_SetGameObjectToPortalObstruction: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetGameObjectToPortalObstruction;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _portalID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal float _obstruction;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public ulong PortalID
        {
            get
            {
                return _portalID;
            }
            set
            {
                _portalID = value;
            }
        }

        public float Obstruction
        {
            get
            {
                return _obstruction;
            }
            set
            {
                _obstruction = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SA_SetPortalToPortalObstruction: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetPortalToPortalObstruction;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _portalID0;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _portalID1;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal float _obstruction;

        public ulong PortalID0
        {
            get
            {
                return _portalID0;
            }
            set
            {
                _portalID0 = value;
            }
        }

        public ulong PortalID1
        {
            get
            {
                return _portalID1;
            }
            set
            {
                _portalID1 = value;
            }
        }

        public float Obstruction
        {
            get
            {
                return _obstruction;
            }
            set
            {
                _obstruction = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_RemovePortal: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemovePortal;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _portalID;

        public ulong PortalID
        {
            get
            {
                return _portalID;
            }
            set
            {
                _portalID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkCmd_SA_SetReverbZone: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetReverbZone;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _reverbZoneRoomID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _parentRoomID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal float _transitionRegionWidth;

        public ulong ReverbZoneRoomID
        {
            get
            {
                return _reverbZoneRoomID;
            }
            set
            {
                _reverbZoneRoomID = value;
            }
        }

        public ulong ParentRoomID
        {
            get
            {
                return _parentRoomID;
            }
            set
            {
                _parentRoomID = value;
            }
        }

        public float TransitionRegionWidth
        {
            get
            {
                return _transitionRegionWidth;
            }
            set
            {
                _transitionRegionWidth = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_RemoveReverbZone: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_RemoveReverbZone;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _reverbZoneRoomID;

        public ulong ReverbZoneRoomID
        {
            get
            {
                return _reverbZoneRoomID;
            }
            set
            {
                _reverbZoneRoomID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SA_SetGameObjectInRoom: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetGameObjectInRoom;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ulong _roomID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public ulong RoomID
        {
            get
            {
                return _roomID;
            }
            set
            {
                _roomID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_UnsetGameObjectInRoom: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_UnsetGameObjectInRoom;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkCmd_SA_SetGameObjectRadius: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetGameObjectRadius;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _outerRadius;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal float _innerRadius;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public float OuterRadius
        {
            get
            {
                return _outerRadius;
            }
            set
            {
                _outerRadius = value;
            }
        }

        public float InnerRadius
        {
            get
            {
                return _innerRadius;
            }
            set
            {
                _innerRadius = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_SetEarlyReflectionsAuxSend: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetEarlyReflectionsAuxSend;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _auxBusID;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public uint AuxBusID
        {
            get
            {
                return _auxBusID;
            }
            set
            {
                _auxBusID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_SetEarlyReflectionsVolume: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetEarlyReflectionsVolume;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjectID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _volume;

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }

        public float Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetAdjacentRoomBleed: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetAdjacentRoomBleed;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _adjacentRoomBleed;

        public float AdjacentRoomBleed
        {
            get
            {
                return _adjacentRoomBleed;
            }
            set
            {
                _adjacentRoomBleed = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_SetReflectionsOrder: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetReflectionsOrder;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _reflectionsOrder;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal byte _updatePaths;

        public uint ReflectionsOrder
        {
            get
            {
                return _reflectionsOrder;
            }
            set
            {
                _reflectionsOrder = value;
            }
        }

        public bool UpdatePaths
        {
            get
            {
                return _updatePaths != 0;
            }
            set
            {
                _updatePaths = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkCmd_SA_SetDiffractionOrder: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetDiffractionOrder;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _diffractionOrder;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal byte _updatePaths;

        public uint DiffractionOrder
        {
            get
            {
                return _diffractionOrder;
            }
            set
            {
                _diffractionOrder = value;
            }
        }

        public bool UpdatePaths
        {
            get
            {
                return _updatePaths != 0;
            }
            set
            {
                _updatePaths = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetMaxGlobalReflectionPaths: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetMaxGlobalReflectionPaths;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _maxReflectionPaths;

        public uint MaxReflectionPaths
        {
            get
            {
                return _maxReflectionPaths;
            }
            set
            {
                _maxReflectionPaths = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetMaxEmitterRoomAuxSends: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetMaxEmitterRoomAuxSends;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _maxEmitterRoomAuxSends;

        public uint MaxEmitterRoomAuxSends
        {
            get
            {
                return _maxEmitterRoomAuxSends;
            }
            set
            {
                _maxEmitterRoomAuxSends = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_SetMaxDiffractionPaths: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetMaxDiffractionPaths;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _maxDiffractionPaths;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;

        public uint MaxDiffractionPaths
        {
            get
            {
                return _maxDiffractionPaths;
            }
            set
            {
                _maxDiffractionPaths = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkCmd_SA_SetSmoothingConstant: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetSmoothingConstant;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _smoothingConstantMs;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ulong _gameObjectID;

        public float SmoothingConstantMs
        {
            get
            {
                return _smoothingConstantMs;
            }
            set
            {
                _smoothingConstantMs = value;
            }
        }

        public ulong GameObjectID
        {
            get
            {
                return _gameObjectID;
            }
            set
            {
                _gameObjectID = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetTransmissionOperation: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetTransmissionOperation;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _operation;

        public uint Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetNumberOfPrimaryRays: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetNumberOfPrimaryRays;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNbPrimaryRays;

        public uint UNbPrimaryRays
        {
            get
            {
                return _uNbPrimaryRays;
            }
            set
            {
                _uNbPrimaryRays = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_SetLoadBalancingSpread: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_SetLoadBalancingSpread;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNbFrames;

        public uint UNbFrames
        {
            get
            {
                return _uNbFrames;
            }
            set
            {
                _uNbFrames = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkCmd_SA_ResetStochasticEngine: IAkCommandType
    {
        // IAkCommandType interface
        public AkCommand CommandType => AkCommand.AkCommand_SA_ResetStochasticEngine;

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uUnused;

        public uint UUnused
        {
            get
            {
                return _uUnused;
            }
            set
            {
                _uUnused = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkCommandBufferHeader
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _bufferSize;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _lastCommandOffset;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _completionCallback;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal System.IntPtr _completionCallbackCookie;

        public uint BufferSize
        {
            get
            {
                return _bufferSize;
            }
            set
            {
                _bufferSize = value;
            }
        }

        public uint LastCommandOffset
        {
            get
            {
                return _lastCommandOffset;
            }
            set
            {
                _lastCommandOffset = value;
            }
        }

        public AkCommandCallbackFunc CompletionCallback
        {
            get
            {
                if (_completionCallback == System.IntPtr.Zero)
                {
                    return null;
                }

                return System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer<AkCommandCallbackFunc>(_completionCallback);
            }
            set
            {
                _completionCallback = value != null ? System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(value): System.IntPtr.Zero;
            }
        }

        public System.IntPtr CompletionCallbackCookie
        {
            get
            {
                return _completionCallbackCookie;
            }
            set
            {
                _completionCallbackCookie = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=2)]
    public struct AkCommandHeader
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ushort _code;
        [System.Runtime.InteropServices.FieldOffset(2)]
        internal ushort _size;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ushort _flags;
        [System.Runtime.InteropServices.FieldOffset(6)]
        internal ushort _result;

        public ushort Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public ushort Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public ushort Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public ushort Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=8)]
    internal struct AkCommandBufferIterator
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal System.IntPtr _header;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _payload;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal System.IntPtr _buffer;

        public System.IntPtr Payload
        {
            get
            {
                return _payload;
            }
            set
            {
                _payload = value;
            }
        }

        public System.IntPtr Buffer
        {
            get
            {
                return _buffer;
            }
            set
            {
                _buffer = value;
            }
        }
    }

}