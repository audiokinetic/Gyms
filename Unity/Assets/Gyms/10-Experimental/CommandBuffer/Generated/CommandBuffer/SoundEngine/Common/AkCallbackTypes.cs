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

    public delegate void AkEventCallbackFunc(AkCallbackType in_eType, ref AkEventCallbackInfo in_pEventInfo, System.IntPtr in_pCallbackInfo, System.IntPtr in_pCookie);

    public delegate void AkCallbackFunc(AkCallbackType param0, ref AkEventCallbackInfo param1, System.IntPtr param2, System.IntPtr param3);

    internal delegate void AkBusCallbackFunc(ref AkSpeakerVolumeMatrixCallbackInfo in_pCallbackInfo, System.IntPtr in_pCookie);

    public delegate void AkBankCallbackFunc(uint in_bankID, System.IntPtr in_pInMemoryBankPtr, AKRESULT in_eLoadResult, System.IntPtr in_pCookie);

    public delegate void AkGlobalCallbackFunc(System.IntPtr in_pContext, AkGlobalCallbackLocation in_eLocation, System.IntPtr in_pCookie);

    public delegate void AkResourceMonitorCallbackFunc(ref AkResourceMonitorDataSummary in_pdataSummary);

    public delegate void AkDeviceStatusCallbackFunc(System.IntPtr in_pContext, uint in_idAudioDeviceShareset, uint in_idDeviceID, AkAudioDeviceEvent in_idEvent, AKRESULT in_AkResult);


    public enum AkCallbackType : int
    {
        AK_EndOfEvent = 1,
        AK_EndOfDynamicSequenceItem = 2,
        AK_Marker = 4,
        AK_Duration = 8,
        AK_SpeakerVolumeMatrix = 16,
        AK_Starvation = 32,
        AK_MusicPlaylistSelect = 64,
        AK_MusicPlayStarted = 128,
        AK_MusicSyncBeat = 256,
        AK_MusicSyncBar = 512,
        AK_MusicSyncEntry = 1024,
        AK_MusicSyncExit = 2048,
        AK_MusicSyncGrid = 4096,
        AK_MusicSyncUserCue = 8192,
        AK_MusicSyncPoint = 16384,
        AK_MIDIEvent = 32768,
        AK_DynamicSequenceSelect = 65536,
        AK_Callback_Last = 131072,
        AK_MusicSyncAll = 32512,
        AK_CallbackBits = 1048575,
        AK_EnableGetMusicPlayPosition = 2097152,
        AK_EnableGetSourceStreamBuffering = 4194304,
        AK_SourceInfo_Last = 8388608,
    }

    public enum AkAudioDeviceEvent : int
    {
        AkAudioDeviceEvent_Initialization = 0,
        AkAudioDeviceEvent_Removal = 1,
        AkAudioDeviceEvent_SystemRemoval = 2,
        AkAudioDeviceEvent_Last = 3,
    }

    public enum AkGlobalCallbackLocation : int
    {
        AkGlobalCallbackLocation_Register = 1,
        AkGlobalCallbackLocation_Begin = 2,
        AkGlobalCallbackLocation_PreProcessMessageQueueForRender = 4,
        AkGlobalCallbackLocation_PostMessagesProcessed = 8,
        AkGlobalCallbackLocation_BeginRender = 16,
        AkGlobalCallbackLocation_EndRender = 32,
        AkGlobalCallbackLocation_End = 64,
        AkGlobalCallbackLocation_Term = 128,
        AkGlobalCallbackLocation_Monitor = 256,
        AkGlobalCallbackLocation_MonitorRecap = 512,
        AkGlobalCallbackLocation_Init = 1024,
        AkGlobalCallbackLocation_Suspend = 2048,
        AkGlobalCallbackLocation_WakeupFromSuspend = 4096,
        AkGlobalCallbackLocation_ProfilerConnect = 8192,
        AkGlobalCallbackLocation_ProfilerDisconnect = 16384,
        AkGlobalCallbackLocation_Num = 15,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=36, Pack=4)]
    public struct AkSegmentInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal int _iCurrentPosition;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal int _iPreEntryDuration;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal int _iActiveDuration;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal int _iPostExitDuration;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal int _iRemainingLookAheadTime;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal float _fBeatDuration;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal float _fBarDuration;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal float _fGridDuration;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal float _fGridOffset;

        public int ICurrentPosition
        {
            get
            {
                return _iCurrentPosition;
            }
            set
            {
                _iCurrentPosition = value;
            }
        }

        public int IPreEntryDuration
        {
            get
            {
                return _iPreEntryDuration;
            }
            set
            {
                _iPreEntryDuration = value;
            }
        }

        public int IActiveDuration
        {
            get
            {
                return _iActiveDuration;
            }
            set
            {
                _iActiveDuration = value;
            }
        }

        public int IPostExitDuration
        {
            get
            {
                return _iPostExitDuration;
            }
            set
            {
                _iPostExitDuration = value;
            }
        }

        public int IRemainingLookAheadTime
        {
            get
            {
                return _iRemainingLookAheadTime;
            }
            set
            {
                _iRemainingLookAheadTime = value;
            }
        }

        public float FBeatDuration
        {
            get
            {
                return _fBeatDuration;
            }
            set
            {
                _fBeatDuration = value;
            }
        }

        public float FBarDuration
        {
            get
            {
                return _fBarDuration;
            }
            set
            {
                _fBarDuration = value;
            }
        }

        public float FGridDuration
        {
            get
            {
                return _fGridDuration;
            }
            set
            {
                _fGridDuration = value;
            }
        }

        public float FGridOffset
        {
            get
            {
                return _fGridOffset;
            }
            set
            {
                _fGridOffset = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=8)]
    public struct AkEventCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _gameObjID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _playingID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _eventID;

        public ulong GameObjID
        {
            get
            {
                return _gameObjID;
            }
            set
            {
                _gameObjID = value;
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
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkMIDIEventCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkMIDIEvent _midiEvent;

        public AkMIDIEvent MidiEvent
        {
            get
            {
                return _midiEvent;
            }
            set
            {
                _midiEvent = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=8)]
    internal struct AkMarkerCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uIdentifier;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _uPosition;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _strLabel;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _uLabelSize;

        public uint UIdentifier
        {
            get
            {
                return _uIdentifier;
            }
            set
            {
                _uIdentifier = value;
            }
        }

        public uint UPosition
        {
            get
            {
                return _uPosition;
            }
            set
            {
                _uPosition = value;
            }
        }

        public uint ULabelSize
        {
            get
            {
                return _uLabelSize;
            }
            set
            {
                _uLabelSize = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkDurationCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _fDuration;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _fEstimatedDuration;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _audioNodeID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _mediaID;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal byte _bStreaming;

        public float FDuration
        {
            get
            {
                return _fDuration;
            }
            set
            {
                _fDuration = value;
            }
        }

        public float FEstimatedDuration
        {
            get
            {
                return _fEstimatedDuration;
            }
            set
            {
                _fEstimatedDuration = value;
            }
        }

        public uint AudioNodeID
        {
            get
            {
                return _audioNodeID;
            }
            set
            {
                _audioNodeID = value;
            }
        }

        public uint MediaID
        {
            get
            {
                return _mediaID;
            }
            set
            {
                _mediaID = value;
            }
        }

        public bool BStreaming
        {
            get
            {
                return _bStreaming != 0;
            }
            set
            {
                _bStreaming = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=8)]
    public struct AkDynamicSequenceItemCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _audioNodeID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _pCustomInfo;

        public uint AudioNodeID
        {
            get
            {
                return _audioNodeID;
            }
            set
            {
                _audioNodeID = value;
            }
        }

        public System.IntPtr PCustomInfo
        {
            get
            {
                return _pCustomInfo;
            }
            set
            {
                _pCustomInfo = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=56, Pack=8)]
    internal struct AkSpeakerVolumeMatrixCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal System.IntPtr _pVolumes;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkChannelConfig _inputConfig;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkChannelConfig _outputConfig;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal System.IntPtr _pfBaseVolume;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal System.IntPtr _pfEmitterListenerVolume;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal ulong _mixConnectionGameObjId;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal System.IntPtr _pContext;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal System.IntPtr _pMixerContext;

        public AkChannelConfig InputConfig
        {
            get
            {
                return _inputConfig;
            }
            set
            {
                _inputConfig = value;
            }
        }

        public AkChannelConfig OutputConfig
        {
            get
            {
                return _outputConfig;
            }
            set
            {
                _outputConfig = value;
            }
        }

        public ulong MixConnectionGameObjId
        {
            get
            {
                return _mixConnectionGameObjId;
            }
            set
            {
                _mixConnectionGameObjId = value;
            }
        }

        public System.IntPtr PContext
        {
            get
            {
                return _pContext;
            }
            set
            {
                _pContext = value;
            }
        }

        public System.IntPtr PMixerContext
        {
            get
            {
                return _pMixerContext;
            }
            set
            {
                _pMixerContext = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkMusicPlaylistCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _playlistID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _uNumPlaylistItems;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _uPlaylistSelection;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _uPlaylistItemDone;

        public uint PlaylistID
        {
            get
            {
                return _playlistID;
            }
            set
            {
                _playlistID = value;
            }
        }

        public uint UNumPlaylistItems
        {
            get
            {
                return _uNumPlaylistItems;
            }
            set
            {
                _uNumPlaylistItems = value;
            }
        }

        public uint UPlaylistSelection
        {
            get
            {
                return _uPlaylistSelection;
            }
            set
            {
                _uPlaylistSelection = value;
            }
        }

        public uint UPlaylistItemDone
        {
            get
            {
                return _uPlaylistItemDone;
            }
            set
            {
                _uPlaylistItemDone = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=48, Pack=8)]
    internal struct AkMusicSyncCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkSegmentInfo _segmentInfo;
        [System.Runtime.InteropServices.FieldOffset(36)]
        internal AkCallbackType _musicSyncType;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal System.IntPtr _pszUserCueName;

        public AkSegmentInfo SegmentInfo
        {
            get
            {
                return _segmentInfo;
            }
            set
            {
                _segmentInfo = value;
            }
        }

        public AkCallbackType MusicSyncType
        {
            get
            {
                return _musicSyncType;
            }
            set
            {
                _musicSyncType = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=4)]
    public struct AkResourceMonitorDataSummary
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _totalCPU;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _pluginCPU;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _physicalVoices;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _virtualVoices;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _totalVoices;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal uint _nbActiveEvents;

        public float TotalCPU
        {
            get
            {
                return _totalCPU;
            }
            set
            {
                _totalCPU = value;
            }
        }

        public float PluginCPU
        {
            get
            {
                return _pluginCPU;
            }
            set
            {
                _pluginCPU = value;
            }
        }

        public uint PhysicalVoices
        {
            get
            {
                return _physicalVoices;
            }
            set
            {
                _physicalVoices = value;
            }
        }

        public uint VirtualVoices
        {
            get
            {
                return _virtualVoices;
            }
            set
            {
                _virtualVoices = value;
            }
        }

        public uint TotalVoices
        {
            get
            {
                return _totalVoices;
            }
            set
            {
                _totalVoices = value;
            }
        }

        public uint NbActiveEvents
        {
            get
            {
                return _nbActiveEvents;
            }
            set
            {
                _nbActiveEvents = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=8)]
    public struct AkDynamicSequenceSelectCallbackInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _audioNodeID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal int _msDelay;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _pCustomInfo;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal System.IntPtr _arExternalSources;

        public uint AudioNodeID
        {
            get
            {
                return _audioNodeID;
            }
            set
            {
                _audioNodeID = value;
            }
        }

        public int MsDelay
        {
            get
            {
                return _msDelay;
            }
            set
            {
                _msDelay = value;
            }
        }

        public System.IntPtr PCustomInfo
        {
            get
            {
                return _pCustomInfo;
            }
            set
            {
                _pCustomInfo = value;
            }
        }

        public System.IntPtr ArExternalSources
        {
            get
            {
                return _arExternalSources;
            }
            set
            {
                _arExternalSources = value;
            }
        }
    }

}