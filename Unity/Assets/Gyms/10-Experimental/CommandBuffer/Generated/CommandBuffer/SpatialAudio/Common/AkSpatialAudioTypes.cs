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

    public enum AkTransmissionOperation : int
    {
        AkTransmissionOperation_Add = 0,
        AkTransmissionOperation_Multiply = 1,
        AkTransmissionOperation_Max = 2,
        AkTransmissionOperation_Default = 2,
    }

    public enum AkRoomDistanceBehavior : int
    {
        AkRoomDistanceBehavior_Subtract = 0,
        AkRoomDistanceBehavior_Exclude = 1,
        AkRoomDistanceBehavior_Default = 0,
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=8)]
    internal struct AkImageSourceName
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNumChar;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _pName;

        public uint UNumChar
        {
            get
            {
                return _uNumChar;
            }
            set
            {
                _uNumChar = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=80, Pack=4)]
    public struct AkSpatialAudioInitSettings
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uMaxSoundPropagationDepth;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _fMovementThreshold;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal uint _uNumberOfPrimaryRays;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal uint _uMaxReflectionOrder;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal uint _uMaxDiffractionOrder;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal uint _uMaxDiffractionPaths;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal uint _uMaxGlobalReflectionPaths;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal uint _uMaxEmitterRoomAuxSends;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal uint _uDiffractionOnReflectionsOrder;
        [System.Runtime.InteropServices.FieldOffset(36)]
        internal float _fMaxDiffractionAngleDegrees;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal float _fMaxPathLength;
        [System.Runtime.InteropServices.FieldOffset(44)]
        internal float _fCPULimitPercentage;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal uint _uLoadBalancingSpread;
        [System.Runtime.InteropServices.FieldOffset(52)]
        internal float _fSmoothingConstantMs;
        [System.Runtime.InteropServices.FieldOffset(56)]
        internal float _fAdjacentRoomBleed;
        [System.Runtime.InteropServices.FieldOffset(60)]
        internal byte _bEnableGeometricDiffractionAndTransmission;
        [System.Runtime.InteropServices.FieldOffset(61)]
        internal byte _bCalcEmitterVirtualPosition;
        [System.Runtime.InteropServices.FieldOffset(64)]
        internal AkTransmissionOperation _eTransmissionOperation;
        [System.Runtime.InteropServices.FieldOffset(68)]
        internal uint _uClusteringMinPoints;
        [System.Runtime.InteropServices.FieldOffset(72)]
        internal float _fClusteringMaxDistance;
        [System.Runtime.InteropServices.FieldOffset(76)]
        internal float _fClusteringDeadZoneDistance;

        public uint UMaxSoundPropagationDepth
        {
            get
            {
                return _uMaxSoundPropagationDepth;
            }
            set
            {
                _uMaxSoundPropagationDepth = value;
            }
        }

        public float FMovementThreshold
        {
            get
            {
                return _fMovementThreshold;
            }
            set
            {
                _fMovementThreshold = value;
            }
        }

        public uint UNumberOfPrimaryRays
        {
            get
            {
                return _uNumberOfPrimaryRays;
            }
            set
            {
                _uNumberOfPrimaryRays = value;
            }
        }

        public uint UMaxReflectionOrder
        {
            get
            {
                return _uMaxReflectionOrder;
            }
            set
            {
                _uMaxReflectionOrder = value;
            }
        }

        public uint UMaxDiffractionOrder
        {
            get
            {
                return _uMaxDiffractionOrder;
            }
            set
            {
                _uMaxDiffractionOrder = value;
            }
        }

        public uint UMaxDiffractionPaths
        {
            get
            {
                return _uMaxDiffractionPaths;
            }
            set
            {
                _uMaxDiffractionPaths = value;
            }
        }

        public uint UMaxGlobalReflectionPaths
        {
            get
            {
                return _uMaxGlobalReflectionPaths;
            }
            set
            {
                _uMaxGlobalReflectionPaths = value;
            }
        }

        public uint UMaxEmitterRoomAuxSends
        {
            get
            {
                return _uMaxEmitterRoomAuxSends;
            }
            set
            {
                _uMaxEmitterRoomAuxSends = value;
            }
        }

        public uint UDiffractionOnReflectionsOrder
        {
            get
            {
                return _uDiffractionOnReflectionsOrder;
            }
            set
            {
                _uDiffractionOnReflectionsOrder = value;
            }
        }

        public float FMaxDiffractionAngleDegrees
        {
            get
            {
                return _fMaxDiffractionAngleDegrees;
            }
            set
            {
                _fMaxDiffractionAngleDegrees = value;
            }
        }

        public float FMaxPathLength
        {
            get
            {
                return _fMaxPathLength;
            }
            set
            {
                _fMaxPathLength = value;
            }
        }

        public float FCPULimitPercentage
        {
            get
            {
                return _fCPULimitPercentage;
            }
            set
            {
                _fCPULimitPercentage = value;
            }
        }

        public uint ULoadBalancingSpread
        {
            get
            {
                return _uLoadBalancingSpread;
            }
            set
            {
                _uLoadBalancingSpread = value;
            }
        }

        public float FSmoothingConstantMs
        {
            get
            {
                return _fSmoothingConstantMs;
            }
            set
            {
                _fSmoothingConstantMs = value;
            }
        }

        public float FAdjacentRoomBleed
        {
            get
            {
                return _fAdjacentRoomBleed;
            }
            set
            {
                _fAdjacentRoomBleed = value;
            }
        }

        public bool BEnableGeometricDiffractionAndTransmission
        {
            get
            {
                return _bEnableGeometricDiffractionAndTransmission != 0;
            }
            set
            {
                _bEnableGeometricDiffractionAndTransmission = (byte)(value? 1 : 0);
            }
        }

        public bool BCalcEmitterVirtualPosition
        {
            get
            {
                return _bCalcEmitterVirtualPosition != 0;
            }
            set
            {
                _bCalcEmitterVirtualPosition = (byte)(value? 1 : 0);
            }
        }

        public AkTransmissionOperation ETransmissionOperation
        {
            get
            {
                return _eTransmissionOperation;
            }
            set
            {
                _eTransmissionOperation = value;
            }
        }

        public uint UClusteringMinPoints
        {
            get
            {
                return _uClusteringMinPoints;
            }
            set
            {
                _uClusteringMinPoints = value;
            }
        }

        public float FClusteringMaxDistance
        {
            get
            {
                return _fClusteringMaxDistance;
            }
            set
            {
                _fClusteringMaxDistance = value;
            }
        }

        public float FClusteringDeadZoneDistance
        {
            get
            {
                return _fClusteringDeadZoneDistance;
            }
            set
            {
                _fClusteringDeadZoneDistance = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=44, Pack=4)]
    public struct AkImageSourceParams
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkVector64 _sourcePosition;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal float _fDistanceScalingFactor;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal float _fLevel;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal float _fDiffraction;
        [System.Runtime.InteropServices.FieldOffset(36)]
        internal float _fOcclusion;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal byte _uDiffractionEmitterSide;
        [System.Runtime.InteropServices.FieldOffset(41)]
        internal byte _uDiffractionListenerSide;

        public AkVector64 SourcePosition
        {
            get
            {
                return _sourcePosition;
            }
            set
            {
                _sourcePosition = value;
            }
        }

        public float FDistanceScalingFactor
        {
            get
            {
                return _fDistanceScalingFactor;
            }
            set
            {
                _fDistanceScalingFactor = value;
            }
        }

        public float FLevel
        {
            get
            {
                return _fLevel;
            }
            set
            {
                _fLevel = value;
            }
        }

        public float FDiffraction
        {
            get
            {
                return _fDiffraction;
            }
            set
            {
                _fDiffraction = value;
            }
        }

        public float FOcclusion
        {
            get
            {
                return _fOcclusion;
            }
            set
            {
                _fOcclusion = value;
            }
        }

        public byte UDiffractionEmitterSide
        {
            get
            {
                return _uDiffractionEmitterSide;
            }
            set
            {
                _uDiffractionEmitterSide = value;
            }
        }

        public byte UDiffractionListenerSide
        {
            get
            {
                return _uDiffractionListenerSide;
            }
            set
            {
                _uDiffractionListenerSide = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkImageSourceTexture
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNumTexture;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal unsafe fixed uint _arTextureID[4];

        public uint UNumTexture
        {
            get
            {
                return _uNumTexture;
            }
            set
            {
                _uNumTexture = value;
            }
        }

        public System.Span<uint> ArTextureID
        {
            get
            {
                unsafe
                {
                    fixed (uint* _arTextureIDPtr = _arTextureID)
                    {
                        return new System.Span<uint>(_arTextureIDPtr, 4);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* _arTextureIDPtr = _arTextureID)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _arTextureIDPtr, 4);
                    }
                }
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=64, Pack=4)]
    public struct AkImageSourceSettings
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkImageSourceParams _params;
        [System.Runtime.InteropServices.FieldOffset(44)]
        internal AkImageSourceTexture _texture;

        public AkImageSourceParams Params
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

        public AkImageSourceTexture Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkExtent
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _halfWidth;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _halfHeight;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _halfDepth;

        public float HalfWidth
        {
            get
            {
                return _halfWidth;
            }
            set
            {
                _halfWidth = value;
            }
        }

        public float HalfHeight
        {
            get
            {
                return _halfHeight;
            }
            set
            {
                _halfHeight = value;
            }
        }

        public float HalfDepth
        {
            get
            {
                return _halfDepth;
            }
            set
            {
                _halfDepth = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=2)]
    public struct AkTriangle
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ushort _point0;
        [System.Runtime.InteropServices.FieldOffset(2)]
        internal ushort _point1;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal ushort _point2;
        [System.Runtime.InteropServices.FieldOffset(6)]
        internal ushort _surface;

        public ushort Point0
        {
            get
            {
                return _point0;
            }
            set
            {
                _point0 = value;
            }
        }

        public ushort Point1
        {
            get
            {
                return _point1;
            }
            set
            {
                _point1 = value;
            }
        }

        public ushort Point2
        {
            get
            {
                return _point2;
            }
            set
            {
                _point2 = value;
            }
        }

        public ushort Surface
        {
            get
            {
                return _surface;
            }
            set
            {
                _surface = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=16, Pack=8)]
    internal struct AkAcousticSurface
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _textureID;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _transmissionLoss;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal System.IntPtr _strName;

        public uint TextureID
        {
            get
            {
                return _textureID;
            }
            set
            {
                _textureID = value;
            }
        }

        public float TransmissionLoss
        {
            get
            {
                return _transmissionLoss;
            }
            set
            {
                _transmissionLoss = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=296, Pack=8)]
    public struct AkReflectionPathInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkVector64 _imageSource;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal unsafe fixed byte _pathPoint[8 * 24];
        [System.Runtime.InteropServices.FieldOffset(216)]
        internal unsafe fixed uint _textureIDs[8];
        [System.Runtime.InteropServices.FieldOffset(248)]
        internal uint _numPathPoints;
        [System.Runtime.InteropServices.FieldOffset(252)]
        internal uint _numReflections;
        [System.Runtime.InteropServices.FieldOffset(256)]
        internal unsafe fixed float _diffraction[8];
        [System.Runtime.InteropServices.FieldOffset(288)]
        internal float _level;
        [System.Runtime.InteropServices.FieldOffset(292)]
        internal byte _isOccluded;

        public AkVector64 ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
            }
        }

        public System.Span<AkVector64> PathPoint
        {
            get
            {
                unsafe
                {
                    fixed (byte* _pathPointPtr = _pathPoint)
                    {
                        return new System.Span<AkVector64>(_pathPointPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (byte* _pathPointPtr = _pathPoint)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, (AkVector64*)_pathPointPtr, 8);
                    }
                }
            }
        }

        public System.Span<uint> TextureIDs
        {
            get
            {
                unsafe
                {
                    fixed (uint* _textureIDsPtr = _textureIDs)
                    {
                        return new System.Span<uint>(_textureIDsPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (uint* _textureIDsPtr = _textureIDs)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _textureIDsPtr, 8);
                    }
                }
            }
        }

        public uint NumPathPoints
        {
            get
            {
                return _numPathPoints;
            }
            set
            {
                _numPathPoints = value;
            }
        }

        public uint NumReflections
        {
            get
            {
                return _numReflections;
            }
            set
            {
                _numReflections = value;
            }
        }

        public System.Span<float> Diffraction
        {
            get
            {
                unsafe
                {
                    fixed (float* _diffractionPtr = _diffraction)
                    {
                        return new System.Span<float>(_diffractionPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (float* _diffractionPtr = _diffraction)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _diffractionPtr, 8);
                    }
                }
            }
        }

        public float Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public bool IsOccluded
        {
            get
            {
                return _isOccluded != 0;
            }
            set
            {
                _isOccluded = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=464, Pack=8)]
    public struct AkDiffractionPathInfo
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal unsafe fixed byte _nodes[8 * 24];
        [System.Runtime.InteropServices.FieldOffset(192)]
        internal AkVector64 _emitterPos;
        [System.Runtime.InteropServices.FieldOffset(216)]
        internal unsafe fixed float _angles[8];
        [System.Runtime.InteropServices.FieldOffset(248)]
        internal unsafe fixed ulong _portals[8];
        [System.Runtime.InteropServices.FieldOffset(312)]
        internal unsafe fixed ulong _rooms[9];
        [System.Runtime.InteropServices.FieldOffset(384)]
        internal AkWorldTransform _virtualPos;
        [System.Runtime.InteropServices.FieldOffset(432)]
        internal uint _nodeCount;
        [System.Runtime.InteropServices.FieldOffset(436)]
        internal float _diffraction;
        [System.Runtime.InteropServices.FieldOffset(440)]
        internal float _transmissionLoss;
        [System.Runtime.InteropServices.FieldOffset(444)]
        internal float _totLength;
        [System.Runtime.InteropServices.FieldOffset(448)]
        internal float _obstructionValue;
        [System.Runtime.InteropServices.FieldOffset(452)]
        internal float _occlusionValue;
        [System.Runtime.InteropServices.FieldOffset(456)]
        internal float _gain;

        public System.Span<AkVector64> Nodes
        {
            get
            {
                unsafe
                {
                    fixed (byte* _nodesPtr = _nodes)
                    {
                        return new System.Span<AkVector64>(_nodesPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (byte* _nodesPtr = _nodes)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, (AkVector64*)_nodesPtr, 8);
                    }
                }
            }
        }

        public AkVector64 EmitterPos
        {
            get
            {
                return _emitterPos;
            }
            set
            {
                _emitterPos = value;
            }
        }

        public System.Span<float> Angles
        {
            get
            {
                unsafe
                {
                    fixed (float* _anglesPtr = _angles)
                    {
                        return new System.Span<float>(_anglesPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (float* _anglesPtr = _angles)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _anglesPtr, 8);
                    }
                }
            }
        }

        public System.Span<ulong> Portals
        {
            get
            {
                unsafe
                {
                    fixed (ulong* _portalsPtr = _portals)
                    {
                        return new System.Span<ulong>(_portalsPtr, 8);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (ulong* _portalsPtr = _portals)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _portalsPtr, 8);
                    }
                }
            }
        }

        public System.Span<ulong> Rooms
        {
            get
            {
                unsafe
                {
                    fixed (ulong* _roomsPtr = _rooms)
                    {
                        return new System.Span<ulong>(_roomsPtr, 9);
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (ulong* _roomsPtr = _rooms)
                    {
                        WwiseMarshalHelper.SetFixedArray(value, _roomsPtr, 9);
                    }
                }
            }
        }

        public AkWorldTransform VirtualPos
        {
            get
            {
                return _virtualPos;
            }
            set
            {
                _virtualPos = value;
            }
        }

        public uint NodeCount
        {
            get
            {
                return _nodeCount;
            }
            set
            {
                _nodeCount = value;
            }
        }

        public float Diffraction
        {
            get
            {
                return _diffraction;
            }
            set
            {
                _diffraction = value;
            }
        }

        public float TransmissionLoss
        {
            get
            {
                return _transmissionLoss;
            }
            set
            {
                _transmissionLoss = value;
            }
        }

        public float TotLength
        {
            get
            {
                return _totLength;
            }
            set
            {
                _totLength = value;
            }
        }

        public float ObstructionValue
        {
            get
            {
                return _obstructionValue;
            }
            set
            {
                _obstructionValue = value;
            }
        }

        public float OcclusionValue
        {
            get
            {
                return _occlusionValue;
            }
            set
            {
                _occlusionValue = value;
            }
        }

        public float Gain
        {
            get
            {
                return _gain;
            }
            set
            {
                _gain = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=84, Pack=4)]
    public struct AkPortalParams
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkWorldTransform _transform;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal AkExtent _extent;
        [System.Runtime.InteropServices.FieldOffset(60)]
        internal byte _bEnabled;
        [System.Runtime.InteropServices.FieldOffset(64)]
        internal ulong _frontRoom;
        [System.Runtime.InteropServices.FieldOffset(72)]
        internal ulong _backRoom;
        [System.Runtime.InteropServices.FieldOffset(80)]
        internal float _adjacentRoomBleed;

        public AkWorldTransform Transform
        {
            get
            {
                return _transform;
            }
            set
            {
                _transform = value;
            }
        }

        public AkExtent Extent
        {
            get
            {
                return _extent;
            }
            set
            {
                _extent = value;
            }
        }

        public bool BEnabled
        {
            get
            {
                return _bEnabled != 0;
            }
            set
            {
                _bEnabled = (byte)(value? 1 : 0);
            }
        }

        public ulong FrontRoom
        {
            get
            {
                return _frontRoom;
            }
            set
            {
                _frontRoom = value;
            }
        }

        public ulong BackRoom
        {
            get
            {
                return _backRoom;
            }
            set
            {
                _backRoom = value;
            }
        }

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


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=60, Pack=4)]
    public struct AkRoomParams
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkVector _front;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkVector _up;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal uint _reverbAuxBus;
        [System.Runtime.InteropServices.FieldOffset(28)]
        internal float _reverbLevel;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal float _transmissionLoss;
        [System.Runtime.InteropServices.FieldOffset(36)]
        internal float _roomGameObjAuxSendLevelToSelf;
        [System.Runtime.InteropServices.FieldOffset(40)]
        internal byte _roomGameObjKeepRegistered;
        [System.Runtime.InteropServices.FieldOffset(44)]
        internal ulong _geometryInstanceID;
        [System.Runtime.InteropServices.FieldOffset(52)]
        internal float _roomPriority;
        [System.Runtime.InteropServices.FieldOffset(56)]
        internal AkRoomDistanceBehavior _distanceBehavior;

        public AkVector Front
        {
            get
            {
                return _front;
            }
            set
            {
                _front = value;
            }
        }

        public AkVector Up
        {
            get
            {
                return _up;
            }
            set
            {
                _up = value;
            }
        }

        public uint ReverbAuxBus
        {
            get
            {
                return _reverbAuxBus;
            }
            set
            {
                _reverbAuxBus = value;
            }
        }

        public float ReverbLevel
        {
            get
            {
                return _reverbLevel;
            }
            set
            {
                _reverbLevel = value;
            }
        }

        public float TransmissionLoss
        {
            get
            {
                return _transmissionLoss;
            }
            set
            {
                _transmissionLoss = value;
            }
        }

        public float RoomGameObjAuxSendLevelToSelf
        {
            get
            {
                return _roomGameObjAuxSendLevelToSelf;
            }
            set
            {
                _roomGameObjAuxSendLevelToSelf = value;
            }
        }

        public bool RoomGameObjKeepRegistered
        {
            get
            {
                return _roomGameObjKeepRegistered != 0;
            }
            set
            {
                _roomGameObjKeepRegistered = (byte)(value? 1 : 0);
            }
        }

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

        public float RoomPriority
        {
            get
            {
                return _roomPriority;
            }
            set
            {
                _roomPriority = value;
            }
        }

        public AkRoomDistanceBehavior DistanceBehavior
        {
            get
            {
                return _distanceBehavior;
            }
            set
            {
                _distanceBehavior = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=36, Pack=4)]
    internal struct AkGeometryParams
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal System.IntPtr _triangles;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal ushort _numTriangles;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal System.IntPtr _vertices;
        [System.Runtime.InteropServices.FieldOffset(20)]
        internal ushort _numVertices;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal System.IntPtr _surfaces;
        [System.Runtime.InteropServices.FieldOffset(32)]
        internal ushort _numSurfaces;
        [System.Runtime.InteropServices.FieldOffset(34)]
        internal byte _enableDiffraction;
        [System.Runtime.InteropServices.FieldOffset(35)]
        internal byte _enableDiffractionOnBoundaryEdges;

        public ushort NumTriangles
        {
            get
            {
                return _numTriangles;
            }
            set
            {
                _numTriangles = value;
            }
        }

        public ushort NumVertices
        {
            get
            {
                return _numVertices;
            }
            set
            {
                _numVertices = value;
            }
        }

        public ushort NumSurfaces
        {
            get
            {
                return _numSurfaces;
            }
            set
            {
                _numSurfaces = value;
            }
        }

        public bool EnableDiffraction
        {
            get
            {
                return _enableDiffraction != 0;
            }
            set
            {
                _enableDiffraction = (byte)(value? 1 : 0);
            }
        }

        public bool EnableDiffractionOnBoundaryEdges
        {
            get
            {
                return _enableDiffractionOnBoundaryEdges != 0;
            }
            set
            {
                _enableDiffractionOnBoundaryEdges = (byte)(value? 1 : 0);
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=72, Pack=4)]
    public struct AkGeometryInstanceParams
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkWorldTransform _positionAndOrientation;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal AkVector _scale;
        [System.Runtime.InteropServices.FieldOffset(60)]
        internal ulong _geometrySetID;
        [System.Runtime.InteropServices.FieldOffset(68)]
        internal byte _useForReflectionAndDiffraction;
        [System.Runtime.InteropServices.FieldOffset(69)]
        internal byte _bypassPortalSubtraction;
        [System.Runtime.InteropServices.FieldOffset(70)]
        internal byte _isSolid;

        public AkWorldTransform PositionAndOrientation
        {
            get
            {
                return _positionAndOrientation;
            }
            set
            {
                _positionAndOrientation = value;
            }
        }

        public AkVector Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }

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

        public bool UseForReflectionAndDiffraction
        {
            get
            {
                return _useForReflectionAndDiffraction != 0;
            }
            set
            {
                _useForReflectionAndDiffraction = (byte)(value? 1 : 0);
            }
        }

        public bool BypassPortalSubtraction
        {
            get
            {
                return _bypassPortalSubtraction != 0;
            }
            set
            {
                _bypassPortalSubtraction = (byte)(value? 1 : 0);
            }
        }

        public bool IsSolid
        {
            get
            {
                return _isSolid != 0;
            }
            set
            {
                _isSolid = (byte)(value? 1 : 0);
            }
        }
    }


    public static class AkSpatialAudioTypesConstants
    {

        public const int AK_MAX_NUM_TEXTURE = 4;

        public const int AK_MAX_REFLECT_ORDER = 4;

        public const int AK_MAX_REFLECTION_PATH_LENGTH = 8;

        public const int AK_STOCHASTIC_RESERVE_LENGTH = 8;

        public const int AK_MAX_SOUND_PROPAGATION_DEPTH = 8;

        public const int AK_MAX_SOUND_PROPAGATION_WIDTH = 32;

        public const float AK_SA_EPSILON = 0.001f;

        public const float AK_SA_DIFFRACTION_EPSILON = 0.01f;

        public const float AK_SA_DIFFRACTION_DOT_EPSILON = 5e-05f;

        public const float AK_SA_PLANE_THICKNESS = 0.01f;

        public const float AK_SA_MIN_ENVIRONMENT_ABSORPTION = 0.01f;

        public const float AK_SA_MIN_ENVIRONMENT_SURFACE_AREA = 1f;

        public const double AK_DEFAULT_GEOMETRY_POSITION_X = 0;

        public const double AK_DEFAULT_GEOMETRY_POSITION_Y = 0;

        public const double AK_DEFAULT_GEOMETRY_POSITION_Z = 0;

        public const double AK_DEFAULT_GEOMETRY_FRONT_X = 0;

        public const double AK_DEFAULT_GEOMETRY_FRONT_Y = 0;

        public const double AK_DEFAULT_GEOMETRY_FRONT_Z = 1;

        public const double AK_DEFAULT_GEOMETRY_TOP_X = 0;

        public const double AK_DEFAULT_GEOMETRY_TOP_Y = 1;

        public const double AK_DEFAULT_GEOMETRY_TOP_Z = 0;
    }
}