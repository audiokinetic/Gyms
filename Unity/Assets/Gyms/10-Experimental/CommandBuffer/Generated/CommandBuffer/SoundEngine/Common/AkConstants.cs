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

    public static class AkConstantsConstants
    {

        public const uint AK_INVALID_PLUGINID = 4294967295;

        public const uint AK_INVALID_SHARE_SET_ID = 4294967295;

        public const ulong AK_INVALID_GAME_OBJECT = 18446744073709551615;

        public const uint AK_INVALID_UNIQUE_ID = 0;

        public const uint AK_INVALID_RTPC_ID = 0;

        public const uint AK_INVALID_PLAYING_ID = 0;

        public const uint AK_DEFAULT_SWITCH_STATE = 0;

        public const int AK_INVALID_POOL_ID = -1;

        public const int AK_DEFAULT_POOL_ID = -1;

        public const uint AK_INVALID_AUX_ID = 0;

        public const uint AK_INVALID_FILE_ID = 4294967295;

        public const ulong AK_INVALID_CACHE_ID = 18446744073709551615;

        public const uint AK_INVALID_DEVICE_ID = 4294967295;

        public const uint AK_INVALID_BANK_ID = 0;

        public const uint AK_FALLBACK_ARGUMENTVALUE_ID = 0;

        public const uint AK_INVALID_CHANNELMASK = 0;

        public const uint AK_INVALID_OUTPUT_DEVICE_ID = 0;

        public const uint AK_INVALID_PIPELINE_ID = 0;

        public const ulong AK_INVALID_AUDIO_OBJECT_ID = 18446744073709551615;

        public const ulong AK_TRANSPORT_GAME_OBJECT = 18446744073709551614;

        public const ulong AK_DIRECT_GAME_OBJECT = 18446744073709551613;

        public const sbyte AK_DEFAULT_PRIORITY = 50;

        public const sbyte AK_MIN_PRIORITY = 0;

        public const sbyte AK_MAX_PRIORITY = 100;

        public const sbyte AK_DEFAULT_BANK_IO_PRIORITY = 50;

        public const float AK_DEFAULT_BANK_THROUGHPUT = 1048.576f;

        public const uint AK_SOUNDBANK_VERSION = 172;

        public const uint AkJobType_Generic = 0;

        public const uint AkJobType_AudioProcessing = 1;

        public const uint AkJobType_SpatialAudio = 2;

        public const uint AK_NUM_JOB_TYPES = 3;

        public const int AK_MAX_LANGUAGE_NAME_SIZE = 32;

        public const int AKCOMPANYID_PLUGINDEV_MIN = 64;

        public const int AKCOMPANYID_PLUGINDEV_MAX = 255;

        public const int AKCOMPANYID_AUDIOKINETIC = 0;

        public const int AKCOMPANYID_AUDIOKINETIC_EXTERNAL = 1;

        public const int AKCOMPANYID_MCDSP = 256;

        public const int AKCOMPANYID_WAVEARTS = 257;

        public const int AKCOMPANYID_PHONETICARTS = 258;

        public const int AKCOMPANYID_IZOTOPE = 259;

        public const int AKCOMPANYID_CRANKCASEAUDIO = 261;

        public const int AKCOMPANYID_IOSONO = 262;

        public const int AKCOMPANYID_AUROTECHNOLOGIES = 263;

        public const int AKCOMPANYID_DOLBY = 264;

        public const int AKCOMPANYID_TWOBIGEARS = 265;

        public const int AKCOMPANYID_OCULUS = 266;

        public const int AKCOMPANYID_BLUERIPPLESOUND = 267;

        public const int AKCOMPANYID_ENZIEN = 268;

        public const int AKCOMPANYID_KROTOS = 269;

        public const int AKCOMPANYID_NURULIZE = 270;

        public const int AKCOMPANYID_SUPERPOWERED = 271;

        public const int AKCOMPANYID_GOOGLE = 272;

        public const int AKCOMPANYID_VISISONICS = 277;

        public const int AKCODECID_BANK = 0;

        public const int AKCODECID_PCM = 1;

        public const int AKCODECID_ADPCM = 2;

        public const int AKCODECID_XMA = 3;

        public const int AKCODECID_VORBIS = 4;

        public const int AKCODECID_WIIADPCM = 5;

        public const int AKCODECID_PCM_WAV = 7;

        public const int AKCODECID_EXTERNAL_SOURCE = 8;

        public const int AKCODECID_XWMA = 9;

        public const int AKCODECID_FILE_PACKAGE = 11;

        public const int AKCODECID_ATRAC9 = 12;

        public const int AKCODECID_VAG = 13;

        public const int AKCODECID_PROFILERCAPTURE = 14;

        public const int AKCODECID_ANALYSISFILE = 15;

        public const int AKCODECID_MIDI = 16;

        public const int AKCODECID_OPUSNX = 17;

        public const int AKCODECID_CAF = 18;

        public const int AKCODECID_AKOPUS = 19;

        public const int AKCODECID_AKOPUS_WEM = 20;

        public const int AKCODECID_MEMORYMGR_DUMP = 21;

        public const int AKCODECID_SONY360 = 22;

        public const int AKCODECID_BANK_EVENT = 30;

        public const int AKCODECID_BANK_BUS = 31;

        public const int AKPLUGINID_RECORDER = 132;

        public const int AKPLUGINID_IMPACTER = 184;

        public const int AKPLUGINID_SYSTEM_OUTPUT_META = 900;

        public const int AKPLUGINID_AUDIO_OBJECT_ATTENUATION_META = 901;

        public const int AKPLUGINID_AUDIO_OBJECT_PRIORITY_META = 902;

        public const int AKEXTENSIONID_SPATIALAUDIO = 800;

        public const int AKEXTENSIONID_INTERACTIVEMUSIC = 801;

        public const int AKEXTENSIONID_MIDIDEVICEMGR = 802;

        public const int AK_WAVE_FORMAT_VAG = 65531;

        public const int AK_WAVE_FORMAT_AT9 = 65532;

        public const int AK_WAVE_FORMAT_VORBIS = 65535;

        public const int AK_WAVE_FORMAT_OPUSNX = 12345;

        public const int AK_WAVE_FORMAT_OPUS = 12352;

        public const int AK_WAVE_FORMAT_OPUS_WEM = 12353;

        public const int AK_WAVE_FORMAT_XMA2 = 358;

        public const uint AK_INVALID_SAMPLE_POS = 4294967295;
    }
}