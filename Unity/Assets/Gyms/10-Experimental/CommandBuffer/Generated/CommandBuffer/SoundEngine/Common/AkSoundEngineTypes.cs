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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkAudioSettings
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNumSamplesPerFrame;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _uNumSamplesPerSecond;

        public uint UNumSamplesPerFrame
        {
            get
            {
                return _uNumSamplesPerFrame;
            }
            set
            {
                _uNumSamplesPerFrame = value;
            }
        }

        public uint UNumSamplesPerSecond
        {
            get
            {
                return _uNumSamplesPerSecond;
            }
            set
            {
                _uNumSamplesPerSecond = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkMotionDeviceData
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkMotionDeviceType _deviceType;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMotionInputProfile _inputProfile;

        public AkMotionDeviceType DeviceType
        {
            get
            {
                return _deviceType;
            }
            set
            {
                _deviceType = value;
            }
        }

        public AkMotionInputProfile InputProfile
        {
            get
            {
                return _inputProfile;
            }
            set
            {
                _inputProfile = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct Ak3DAudioSinkCapabilities
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkChannelConfig _channelConfig;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _uMaxSystemAudioObjects;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _uAvailableSystemAudioObjects;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal byte _bPassthrough;
        [System.Runtime.InteropServices.FieldOffset(13)]
        internal byte _bMultiChannelObjects;

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

        public uint UMaxSystemAudioObjects
        {
            get
            {
                return _uMaxSystemAudioObjects;
            }
            set
            {
                _uMaxSystemAudioObjects = value;
            }
        }

        public uint UAvailableSystemAudioObjects
        {
            get
            {
                return _uAvailableSystemAudioObjects;
            }
            set
            {
                _uAvailableSystemAudioObjects = value;
            }
        }

        public bool BPassthrough
        {
            get
            {
                return _bPassthrough != 0;
            }
            set
            {
                _bPassthrough = (byte)(value? 1 : 0);
            }
        }

        public bool BMultiChannelObjects
        {
            get
            {
                return _bMultiChannelObjects != 0;
            }
            set
            {
                _bMultiChannelObjects = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=40, Pack=8)]
    public struct AkOutputDeviceInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _pluginID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _audioDeviceShareset;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _idDevice;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkChannelConfig _channelConfig;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal Ak3DAudioSinkCapabilities _capabilities;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal System.IntPtr _customData;

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

        public uint AudioDeviceShareset
        {
            get
            {
                return _audioDeviceShareset;
            }
            set
            {
                _audioDeviceShareset = value;
            }
        }

        public uint IdDevice
        {
            get
            {
                return _idDevice;
            }
            set
            {
                _idDevice = value;
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

        public Ak3DAudioSinkCapabilities Capabilities
        {
            get
            {
                return _capabilities;
            }
            set
            {
                _capabilities = value;
            }
        }

        public System.IntPtr CustomData
        {
            get
            {
                return _customData;
            }
            set
            {
                _customData = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkObstructionOcclusionValues
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _occlusion;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _obstruction;

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


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=8)]
    public struct AkAuxSendValue
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ulong _listenerID;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _auxBusID;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal float _fControlValue;

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

        public float FControlValue
        {
            get
            {
                return _fControlValue;
            }
            set
            {
                _fControlValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=32, Pack=8)]
    internal struct AkExternalSourceInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _iExternalSrcCookie;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _idCodec;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _szFile;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal System.IntPtr _pInMemory;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal uint _uiMemorySize;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal uint _idFile;

        public uint IExternalSrcCookie
        {
            get
            {
                return _iExternalSrcCookie;
            }
            set
            {
                _iExternalSrcCookie = value;
            }
        }

        public uint IdCodec
        {
            get
            {
                return _idCodec;
            }
            set
            {
                _idCodec = value;
            }
        }

        public System.IntPtr PInMemory
        {
            get
            {
                return _pInMemory;
            }
            set
            {
                _pInMemory = value;
            }
        }

        public uint UiMemorySize
        {
            get
            {
                return _uiMemorySize;
            }
            set
            {
                _uiMemorySize = value;
            }
        }

        public uint IdFile
        {
            get
            {
                return _idFile;
            }
            set
            {
                _idFile = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkRamp
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _fPrev;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _fNext;

        public float FPrev
        {
            get
            {
                return _fPrev;
            }
            set
            {
                _fPrev = value;
            }
        }

        public float FNext
        {
            get
            {
                return _fNext;
            }
            set
            {
                _fNext = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=4)]
    public struct AkOutputSettings
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _audioDeviceShareset;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _idDevice;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal AkPanningRule _ePanningRule;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkChannelConfig _channelConfig;

        public uint AudioDeviceShareset
        {
            get
            {
                return _audioDeviceShareset;
            }
            set
            {
                _audioDeviceShareset = value;
            }
        }

        public uint IdDevice
        {
            get
            {
                return _idDevice;
            }
            set
            {
                _idDevice = value;
            }
        }

        public AkPanningRule EPanningRule
        {
            get
            {
                return _ePanningRule;
            }
            set
            {
                _ePanningRule = value;
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


    public static class AkSoundEngineTypesConstants
    {

        public const bool AK_ASYNC_OPEN_DEFAULT = false;

        public const int AK_COMM_DEFAULT_DISCOVERY_PORT = 24024;
    }
}