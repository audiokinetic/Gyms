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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=24, Pack=8)]
    public struct AkVector64
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal double _x;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal double _y;
        [System.Runtime.InteropServices.FieldOffset(16)]
        internal double _z;

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkVector
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _x;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _y;
        [System.Runtime.InteropServices.FieldOffset(8)]
        internal float _z;

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=48, Pack=8)]
    public struct AkWorldTransform
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkVector _orientationFront;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkVector _orientationTop;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal AkVector64 _position;

        public AkVector OrientationFront
        {
            get
            {
                return _orientationFront;
            }
            set
            {
                _orientationFront = value;
            }
        }

        public AkVector OrientationTop
        {
            get
            {
                return _orientationTop;
            }
            set
            {
                _orientationTop = value;
            }
        }

        public AkVector64 Position
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
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=36, Pack=4)]
    public struct AkTransform
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkVector _orientationFront;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal AkVector _orientationTop;
        [System.Runtime.InteropServices.FieldOffset(24)]
        internal AkVector _position;

        public AkVector OrientationFront
        {
            get
            {
                return _orientationFront;
            }
            set
            {
                _orientationFront = value;
            }
        }

        public AkVector OrientationTop
        {
            get
            {
                return _orientationTop;
            }
            set
            {
                _orientationTop = value;
            }
        }

        public AkVector Position
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
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=56, Pack=8)]
    public struct AkChannelEmitter
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkWorldTransform _position;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal uint _uInputChannels;

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

        public uint UInputChannels
        {
            get
            {
                return _uInputChannels;
            }
            set
            {
                _uInputChannels = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkPolarCoord
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal float _r;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal float _theta;

        public float R
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
            }
        }

        public float Theta
        {
            get
            {
                return _theta;
            }
            set
            {
                _theta = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=56, Pack=8)]
    public struct AkListener
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkWorldTransform _position;
        [System.Runtime.InteropServices.FieldOffset(48)]
        internal float _fScalingFactor;
        [System.Runtime.InteropServices.FieldOffset(52)]
        internal byte _bSpatialized;

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

        public float FScalingFactor
        {
            get
            {
                return _fScalingFactor;
            }
            set
            {
                _fScalingFactor = value;
            }
        }

        public bool BSpatialized
        {
            get
            {
                return _bSpatialized != 0;
            }
            set
            {
                _bSpatialized = (byte)(value? 1 : 0);
            }
        }
    }

}