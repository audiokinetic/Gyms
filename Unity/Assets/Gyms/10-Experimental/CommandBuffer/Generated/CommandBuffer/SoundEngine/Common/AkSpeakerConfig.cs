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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=4, Pack=4)]
    public struct AkChannelConfig
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uNumChannels;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal uint _eConfigType;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal uint _uChannelMask;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal uint _uFullCfg;

        public uint UFullCfg
        {
            get
            {
                return _uFullCfg;
            }
            set
            {
                _uFullCfg = value;
            }
        }
    }


    public static class AkSpeakerConfigConstants
    {

        public const int AK_SPEAKER_FRONT_LEFT = 1;

        public const int AK_SPEAKER_FRONT_RIGHT = 2;

        public const int AK_SPEAKER_FRONT_CENTER = 4;

        public const int AK_SPEAKER_LOW_FREQUENCY = 8;

        public const int AK_SPEAKER_BACK_LEFT = 16;

        public const int AK_SPEAKER_BACK_RIGHT = 32;

        public const int AK_SPEAKER_BACK_CENTER = 256;

        public const int AK_SPEAKER_SIDE_LEFT = 512;

        public const int AK_SPEAKER_SIDE_RIGHT = 1024;

        public const int AK_SPEAKER_TOP = 2048;

        public const int AK_SPEAKER_HEIGHT_FRONT_LEFT = 4096;

        public const int AK_SPEAKER_HEIGHT_FRONT_CENTER = 8192;

        public const int AK_SPEAKER_HEIGHT_FRONT_RIGHT = 16384;

        public const int AK_SPEAKER_HEIGHT_BACK_LEFT = 32768;

        public const int AK_SPEAKER_HEIGHT_BACK_CENTER = 65536;

        public const int AK_SPEAKER_HEIGHT_BACK_RIGHT = 131072;

        public const int AK_SPEAKER_HEIGHT_SIDE_LEFT = 262144;

        public const int AK_SPEAKER_HEIGHT_SIDE_RIGHT = 524288;

        public const int AK_SPEAKER_SETUP_MONO = 4;

        public const int AK_SPEAKER_SETUP_0POINT1 = 8;

        public const int AK_SPEAKER_SETUP_1POINT1 = 12;

        public const int AK_SPEAKER_SETUP_STEREO = 3;

        public const int AK_SPEAKER_SETUP_2POINT1 = 11;

        public const int AK_SPEAKER_SETUP_3STEREO = 7;

        public const int AK_SPEAKER_SETUP_3POINT1 = 15;

        public const int AK_SPEAKER_SETUP_4 = 1539;

        public const int AK_SPEAKER_SETUP_4POINT1 = 1547;

        public const int AK_SPEAKER_SETUP_5 = 1543;

        public const int AK_SPEAKER_SETUP_5POINT1 = 1551;

        public const int AK_SPEAKER_SETUP_6 = 1587;

        public const int AK_SPEAKER_SETUP_6POINT1 = 1595;

        public const int AK_SPEAKER_SETUP_7 = 1591;

        public const int AK_SPEAKER_SETUP_7POINT1 = 1599;

        public const int AK_SPEAKER_SETUP_SURROUND = 259;

        public const int AK_SPEAKER_SETUP_HEIGHT_2 = 20480;

        public const int AK_SPEAKER_SETUP_HEIGHT_4 = 184320;

        public const int AK_SPEAKER_SETUP_HEIGHT_5 = 192512;

        public const int AK_SPEAKER_SETUP_HEIGHT_ALL = 258048;

        public const int AK_SPEAKER_SETUP_HEIGHT_4_TOP = 186368;

        public const int AK_SPEAKER_SETUP_HEIGHT_5_TOP = 194560;

        public const int AK_SPEAKER_SETUP_AURO_222 = 22019;

        public const int AK_SPEAKER_SETUP_AURO_8 = 185859;

        public const int AK_SPEAKER_SETUP_AURO_9 = 185863;

        public const int AK_SPEAKER_SETUP_AURO_9POINT1 = 185871;

        public const int AK_SPEAKER_SETUP_AURO_10 = 187911;

        public const int AK_SPEAKER_SETUP_AURO_10POINT1 = 187919;

        public const int AK_SPEAKER_SETUP_AURO_11 = 196103;

        public const int AK_SPEAKER_SETUP_AURO_11POINT1 = 196111;

        public const int AK_SPEAKER_SETUP_AURO_11_740 = 185911;

        public const int AK_SPEAKER_SETUP_AURO_11POINT1_740 = 185919;

        public const int AK_SPEAKER_SETUP_AURO_13_751 = 196151;

        public const int AK_SPEAKER_SETUP_AURO_13POINT1_751 = 196159;

        public const int AK_SPEAKER_SETUP_DOLBY_5_0_2 = 22023;

        public const int AK_SPEAKER_SETUP_DOLBY_5_1_2 = 22031;

        public const int AK_SPEAKER_SETUP_DOLBY_5_0_4 = 185863;

        public const int AK_SPEAKER_SETUP_DOLBY_5_1_4 = 185871;

        public const int AK_SPEAKER_SETUP_DOLBY_6_0_2 = 22067;

        public const int AK_SPEAKER_SETUP_DOLBY_6_1_2 = 22075;

        public const int AK_SPEAKER_SETUP_DOLBY_6_0_4 = 185907;

        public const int AK_SPEAKER_SETUP_DOLBY_6_1_4 = 185915;

        public const int AK_SPEAKER_SETUP_DOLBY_7_0_2 = 22071;

        public const int AK_SPEAKER_SETUP_DOLBY_7_1_2 = 22079;

        public const int AK_SPEAKER_SETUP_DOLBY_7_0_4 = 185911;

        public const int AK_SPEAKER_SETUP_DOLBY_7_1_4 = 185919;

        public const int AK_SPEAKER_SETUP_ALL_SPEAKERS = 261951;

        public const int AK_IDX_SETUP_FRONT_LEFT = 0;

        public const int AK_IDX_SETUP_FRONT_RIGHT = 1;

        public const int AK_IDX_SETUP_CENTER = 2;

        public const int AK_IDX_SETUP_NOCENTER_BACK_LEFT = 2;

        public const int AK_IDX_SETUP_NOCENTER_BACK_RIGHT = 3;

        public const int AK_IDX_SETUP_NOCENTER_SIDE_LEFT = 4;

        public const int AK_IDX_SETUP_NOCENTER_SIDE_RIGHT = 5;

        public const int AK_IDX_SETUP_WITHCENTER_BACK_LEFT = 3;

        public const int AK_IDX_SETUP_WITHCENTER_BACK_RIGHT = 4;

        public const int AK_IDX_SETUP_WITHCENTER_SIDE_LEFT = 5;

        public const int AK_IDX_SETUP_WITHCENTER_SIDE_RIGHT = 6;

        public const int AK_IDX_SETUP_WITHCENTER_HEIGHT_FRONT_LEFT = 7;

        public const int AK_IDX_SETUP_WITHCENTER_HEIGHT_FRONT_RIGHT = 8;

        public const int AK_IDX_SETUP_WITHCENTER_HEIGHT_BACK_LEFT = 9;

        public const int AK_IDX_SETUP_WITHCENTER_HEIGHT_BACK_RIGHT = 10;

        public const int AK_IDX_SETUP_0_LFE = 0;

        public const int AK_IDX_SETUP_1_CENTER = 0;

        public const int AK_IDX_SETUP_1_LFE = 1;

        public const int AK_IDX_SETUP_2_LEFT = 0;

        public const int AK_IDX_SETUP_2_RIGHT = 1;

        public const int AK_IDX_SETUP_2_LFE = 2;

        public const int AK_IDX_SETUP_3_LEFT = 0;

        public const int AK_IDX_SETUP_3_RIGHT = 1;

        public const int AK_IDX_SETUP_3_CENTER = 2;

        public const int AK_IDX_SETUP_3_LFE = 3;

        public const int AK_IDX_SETUP_4_FRONTLEFT = 0;

        public const int AK_IDX_SETUP_4_FRONTRIGHT = 1;

        public const int AK_IDX_SETUP_4_REARLEFT = 2;

        public const int AK_IDX_SETUP_4_REARRIGHT = 3;

        public const int AK_IDX_SETUP_4_LFE = 4;

        public const int AK_IDX_SETUP_5_FRONTLEFT = 0;

        public const int AK_IDX_SETUP_5_FRONTRIGHT = 1;

        public const int AK_IDX_SETUP_5_CENTER = 2;

        public const int AK_IDX_SETUP_5_REARLEFT = 3;

        public const int AK_IDX_SETUP_5_REARRIGHT = 4;

        public const int AK_IDX_SETUP_5_LFE = 5;

        public const int AK_IDX_SETUP_6_FRONTLEFT = 0;

        public const int AK_IDX_SETUP_6_FRONTRIGHT = 1;

        public const int AK_IDX_SETUP_6_REARLEFT = 2;

        public const int AK_IDX_SETUP_6_REARRIGHT = 3;

        public const int AK_IDX_SETUP_6_SIDELEFT = 4;

        public const int AK_IDX_SETUP_6_SIDERIGHT = 5;

        public const int AK_IDX_SETUP_6_LFE = 6;

        public const int AK_IDX_SETUP_7_FRONTLEFT = 0;

        public const int AK_IDX_SETUP_7_FRONTRIGHT = 1;

        public const int AK_IDX_SETUP_7_CENTER = 2;

        public const int AK_IDX_SETUP_7_REARLEFT = 3;

        public const int AK_IDX_SETUP_7_REARRIGHT = 4;

        public const int AK_IDX_SETUP_7_SIDELEFT = 5;

        public const int AK_IDX_SETUP_7_SIDERIGHT = 6;

        public const int AK_IDX_SETUP_7_LFE = 7;

        public const int AK_SPEAKER_SETUP_0_1 = 8;

        public const int AK_SPEAKER_SETUP_1_0_CENTER = 4;

        public const int AK_SPEAKER_SETUP_1_1_CENTER = 12;

        public const int AK_SPEAKER_SETUP_2_0 = 3;

        public const int AK_SPEAKER_SETUP_2_1 = 11;

        public const int AK_SPEAKER_SETUP_3_0 = 7;

        public const int AK_SPEAKER_SETUP_3_1 = 15;

        public const int AK_SPEAKER_SETUP_FRONT = 7;

        public const int AK_SPEAKER_SETUP_4_0 = 1539;

        public const int AK_SPEAKER_SETUP_4_1 = 1547;

        public const int AK_SPEAKER_SETUP_5_0 = 1543;

        public const int AK_SPEAKER_SETUP_5_1 = 1551;

        public const int AK_SPEAKER_SETUP_6_0 = 1587;

        public const int AK_SPEAKER_SETUP_6_1 = 1595;

        public const int AK_SPEAKER_SETUP_7_0 = 1591;

        public const int AK_SPEAKER_SETUP_7_1 = 1599;

        public const int AK_SPEAKER_SETUP_DEFAULT_PLANE = 1599;

        public const int AK_SUPPORTED_STANDARD_CHANNEL_MASK = 261951;

        public const int AK_STANDARD_MAX_NUM_CHANNELS = 8;

        public const int AK_MAX_AMBISONICS_ORDER = 5;

        public const float AK_DEFAULT_HEIGHT_ANGLE = 30f;
    }
}