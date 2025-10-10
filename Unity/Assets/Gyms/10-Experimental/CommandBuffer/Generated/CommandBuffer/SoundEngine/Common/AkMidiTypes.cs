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

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=2, Pack=1)]
    public struct AkMIDIGen
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byParam1;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byParam2;

        public byte ByParam1
        {
            get
            {
                return _byParam1;
            }
            set
            {
                _byParam1 = value;
            }
        }

        public byte ByParam2
        {
            get
            {
                return _byParam2;
            }
            set
            {
                _byParam2 = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=2, Pack=1)]
    public struct AkMIDINote
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byNote;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byVelocity;

        public byte ByNote
        {
            get
            {
                return _byNote;
            }
            set
            {
                _byNote = value;
            }
        }

        public byte ByVelocity
        {
            get
            {
                return _byVelocity;
            }
            set
            {
                _byVelocity = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=2, Pack=1)]
    public struct AkMIDICC
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byCc;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byValue;

        public byte ByCc
        {
            get
            {
                return _byCc;
            }
            set
            {
                _byCc = value;
            }
        }

        public byte ByValue
        {
            get
            {
                return _byValue;
            }
            set
            {
                _byValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=2, Pack=1)]
    public struct AkMIDIPitchbend
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byValueLsb;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byValueMsb;

        public byte ByValueLsb
        {
            get
            {
                return _byValueLsb;
            }
            set
            {
                _byValueLsb = value;
            }
        }

        public byte ByValueMsb
        {
            get
            {
                return _byValueMsb;
            }
            set
            {
                _byValueMsb = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=2, Pack=1)]
    public struct AkMIDINoteAftertouch
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byNote;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byValue;

        public byte ByNote
        {
            get
            {
                return _byNote;
            }
            set
            {
                _byNote = value;
            }
        }

        public byte ByValue
        {
            get
            {
                return _byValue;
            }
            set
            {
                _byValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=1, Pack=1)]
    public struct AkMIDIChannelAftertouch
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byValue;

        public byte ByValue
        {
            get
            {
                return _byValue;
            }
            set
            {
                _byValue = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=1, Pack=1)]
    public struct AkMIDIProgramChange
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byProgramNum;

        public byte ByProgramNum
        {
            get
            {
                return _byProgramNum;
            }
            set
            {
                _byProgramNum = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=8, Pack=4)]
    public struct AkMIDIWwiseCmd
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal ushort _uCmd;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal uint _uArg;

        public ushort UCmd
        {
            get
            {
                return _uCmd;
            }
            set
            {
                _uCmd = value;
            }
        }

        public uint UArg
        {
            get
            {
                return _uArg;
            }
            set
            {
                _uArg = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=12, Pack=4)]
    public struct AkMIDIEvent
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal byte _byType;
        [System.Runtime.InteropServices.FieldOffset(1)]
        internal byte _byChan;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDIGen _gen;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDICC _cc;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDINote _noteOnOff;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDIPitchbend _pitchBend;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDINoteAftertouch _noteAftertouch;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDIChannelAftertouch _chanAftertouch;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDIProgramChange _programChange;
        [System.Runtime.InteropServices.FieldOffset(4)]
        internal AkMIDIWwiseCmd _wwiseCmd;

        public byte ByType
        {
            get
            {
                return _byType;
            }
            set
            {
                _byType = value;
            }
        }

        public byte ByChan
        {
            get
            {
                return _byChan;
            }
            set
            {
                _byChan = value;
            }
        }

        public AkMIDIGen Gen
        {
            get
            {
                return _gen;
            }
            set
            {
                _gen = value;
            }
        }

        public AkMIDICC Cc
        {
            get
            {
                return _cc;
            }
            set
            {
                _cc = value;
            }
        }

        public AkMIDINote NoteOnOff
        {
            get
            {
                return _noteOnOff;
            }
            set
            {
                _noteOnOff = value;
            }
        }

        public AkMIDIPitchbend PitchBend
        {
            get
            {
                return _pitchBend;
            }
            set
            {
                _pitchBend = value;
            }
        }

        public AkMIDINoteAftertouch NoteAftertouch
        {
            get
            {
                return _noteAftertouch;
            }
            set
            {
                _noteAftertouch = value;
            }
        }

        public AkMIDIChannelAftertouch ChanAftertouch
        {
            get
            {
                return _chanAftertouch;
            }
            set
            {
                _chanAftertouch = value;
            }
        }

        public AkMIDIProgramChange ProgramChange
        {
            get
            {
                return _programChange;
            }
            set
            {
                _programChange = value;
            }
        }

        public AkMIDIWwiseCmd WwiseCmd
        {
            get
            {
                return _wwiseCmd;
            }
            set
            {
                _wwiseCmd = value;
            }
        }
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size=20, Pack=4)]
    public struct AkMIDIPost
    {

        [System.Runtime.InteropServices.FieldOffset(0)]
        internal AkMIDIEvent _midiEvent;
        [System.Runtime.InteropServices.FieldOffset(12)]
        internal ulong _uOffset;

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

        public ulong UOffset
        {
            get
            {
                return _uOffset;
            }
            set
            {
                _uOffset = value;
            }
        }
    }


    public static class AkMidiTypesConstants
    {

        public const byte AK_INVALID_MIDI_CHANNEL = 255;

        public const byte AK_INVALID_MIDI_NOTE = 255;

        public const int AK_MIDI_EVENT_TYPE_INVALID = 0;

        public const int AK_MIDI_EVENT_TYPE_NOTE_OFF = 128;

        public const int AK_MIDI_EVENT_TYPE_NOTE_ON = 144;

        public const int AK_MIDI_EVENT_TYPE_NOTE_AFTERTOUCH = 160;

        public const int AK_MIDI_EVENT_TYPE_CONTROLLER = 176;

        public const int AK_MIDI_EVENT_TYPE_PROGRAM_CHANGE = 192;

        public const int AK_MIDI_EVENT_TYPE_CHANNEL_AFTERTOUCH = 208;

        public const int AK_MIDI_EVENT_TYPE_PITCH_BEND = 224;

        public const int AK_MIDI_EVENT_TYPE_SYSEX = 240;

        public const int AK_MIDI_EVENT_TYPE_ESCAPE = 247;

        public const int AK_MIDI_EVENT_TYPE_WWISE_CMD = 254;

        public const int AK_MIDI_EVENT_TYPE_META = 255;

        public const int AK_MIDI_CC_BANK_SELECT_COARSE = 0;

        public const int AK_MIDI_CC_MOD_WHEEL_COARSE = 1;

        public const int AK_MIDI_CC_BREATH_CTRL_COARSE = 2;

        public const int AK_MIDI_CC_CTRL_3_COARSE = 3;

        public const int AK_MIDI_CC_FOOT_PEDAL_COARSE = 4;

        public const int AK_MIDI_CC_PORTAMENTO_COARSE = 5;

        public const int AK_MIDI_CC_DATA_ENTRY_COARSE = 6;

        public const int AK_MIDI_CC_VOLUME_COARSE = 7;

        public const int AK_MIDI_CC_BALANCE_COARSE = 8;

        public const int AK_MIDI_CC_CTRL_9_COARSE = 9;

        public const int AK_MIDI_CC_PAN_POSITION_COARSE = 10;

        public const int AK_MIDI_CC_EXPRESSION_COARSE = 11;

        public const int AK_MIDI_CC_EFFECT_CTRL_1_COARSE = 12;

        public const int AK_MIDI_CC_EFFECT_CTRL_2_COARSE = 13;

        public const int AK_MIDI_CC_CTRL_14_COARSE = 14;

        public const int AK_MIDI_CC_CTRL_15_COARSE = 15;

        public const int AK_MIDI_CC_GEN_SLIDER_1 = 16;

        public const int AK_MIDI_CC_GEN_SLIDER_2 = 17;

        public const int AK_MIDI_CC_GEN_SLIDER_3 = 18;

        public const int AK_MIDI_CC_GEN_SLIDER_4 = 19;

        public const int AK_MIDI_CC_CTRL_20_COARSE = 20;

        public const int AK_MIDI_CC_CTRL_21_COARSE = 21;

        public const int AK_MIDI_CC_CTRL_22_COARSE = 22;

        public const int AK_MIDI_CC_CTRL_23_COARSE = 23;

        public const int AK_MIDI_CC_CTRL_24_COARSE = 24;

        public const int AK_MIDI_CC_CTRL_25_COARSE = 25;

        public const int AK_MIDI_CC_CTRL_26_COARSE = 26;

        public const int AK_MIDI_CC_CTRL_27_COARSE = 27;

        public const int AK_MIDI_CC_CTRL_28_COARSE = 28;

        public const int AK_MIDI_CC_CTRL_29_COARSE = 29;

        public const int AK_MIDI_CC_CTRL_30_COARSE = 30;

        public const int AK_MIDI_CC_CTRL_31_COARSE = 31;

        public const int AK_MIDI_CC_BANK_SELECT_FINE = 32;

        public const int AK_MIDI_CC_MOD_WHEEL_FINE = 33;

        public const int AK_MIDI_CC_BREATH_CTRL_FINE = 34;

        public const int AK_MIDI_CC_CTRL_3_FINE = 35;

        public const int AK_MIDI_CC_FOOT_PEDAL_FINE = 36;

        public const int AK_MIDI_CC_PORTAMENTO_FINE = 37;

        public const int AK_MIDI_CC_DATA_ENTRY_FINE = 38;

        public const int AK_MIDI_CC_VOLUME_FINE = 39;

        public const int AK_MIDI_CC_BALANCE_FINE = 40;

        public const int AK_MIDI_CC_CTRL_9_FINE = 41;

        public const int AK_MIDI_CC_PAN_POSITION_FINE = 42;

        public const int AK_MIDI_CC_EXPRESSION_FINE = 43;

        public const int AK_MIDI_CC_EFFECT_CTRL_1_FINE = 44;

        public const int AK_MIDI_CC_EFFECT_CTRL_2_FINE = 45;

        public const int AK_MIDI_CC_CTRL_14_FINE = 46;

        public const int AK_MIDI_CC_CTRL_15_FINE = 47;

        public const int AK_MIDI_CC_CTRL_20_FINE = 52;

        public const int AK_MIDI_CC_CTRL_21_FINE = 53;

        public const int AK_MIDI_CC_CTRL_22_FINE = 54;

        public const int AK_MIDI_CC_CTRL_23_FINE = 55;

        public const int AK_MIDI_CC_CTRL_24_FINE = 56;

        public const int AK_MIDI_CC_CTRL_25_FINE = 57;

        public const int AK_MIDI_CC_CTRL_26_FINE = 58;

        public const int AK_MIDI_CC_CTRL_27_FINE = 59;

        public const int AK_MIDI_CC_CTRL_28_FINE = 60;

        public const int AK_MIDI_CC_CTRL_29_FINE = 61;

        public const int AK_MIDI_CC_CTRL_30_FINE = 62;

        public const int AK_MIDI_CC_CTRL_31_FINE = 63;

        public const int AK_MIDI_CC_HOLD_PEDAL = 64;

        public const int AK_MIDI_CC_PORTAMENTO_ON_OFF = 65;

        public const int AK_MIDI_CC_SUSTENUTO_PEDAL = 66;

        public const int AK_MIDI_CC_SOFT_PEDAL = 67;

        public const int AK_MIDI_CC_LEGATO_PEDAL = 68;

        public const int AK_MIDI_CC_HOLD_PEDAL_2 = 69;

        public const int AK_MIDI_CC_SOUND_VARIATION = 70;

        public const int AK_MIDI_CC_SOUND_TIMBRE = 71;

        public const int AK_MIDI_CC_SOUND_RELEASE_TIME = 72;

        public const int AK_MIDI_CC_SOUND_ATTACK_TIME = 73;

        public const int AK_MIDI_CC_SOUND_BRIGHTNESS = 74;

        public const int AK_MIDI_CC_SOUND_CTRL_6 = 75;

        public const int AK_MIDI_CC_SOUND_CTRL_7 = 76;

        public const int AK_MIDI_CC_SOUND_CTRL_8 = 77;

        public const int AK_MIDI_CC_SOUND_CTRL_9 = 78;

        public const int AK_MIDI_CC_SOUND_CTRL_10 = 79;

        public const int AK_MIDI_CC_GENERAL_BUTTON_1 = 80;

        public const int AK_MIDI_CC_GENERAL_BUTTON_2 = 81;

        public const int AK_MIDI_CC_GENERAL_BUTTON_3 = 82;

        public const int AK_MIDI_CC_GENERAL_BUTTON_4 = 83;

        public const int AK_MIDI_CC_REVERB_LEVEL = 91;

        public const int AK_MIDI_CC_TREMOLO_LEVEL = 92;

        public const int AK_MIDI_CC_CHORUS_LEVEL = 93;

        public const int AK_MIDI_CC_CELESTE_LEVEL = 94;

        public const int AK_MIDI_CC_PHASER_LEVEL = 95;

        public const int AK_MIDI_CC_DATA_BUTTON_P1 = 96;

        public const int AK_MIDI_CC_DATA_BUTTON_M1 = 97;

        public const int AK_MIDI_CC_NON_REGISTER_COARSE = 98;

        public const int AK_MIDI_CC_NON_REGISTER_FINE = 99;

        public const int AK_MIDI_CC_ALL_SOUND_OFF = 120;

        public const int AK_MIDI_CC_ALL_CONTROLLERS_OFF = 121;

        public const int AK_MIDI_CC_LOCAL_KEYBOARD = 122;

        public const int AK_MIDI_CC_ALL_NOTES_OFF = 123;

        public const int AK_MIDI_CC_OMNI_MODE_OFF = 124;

        public const int AK_MIDI_CC_OMNI_MODE_ON = 125;

        public const int AK_MIDI_CC_OMNI_MONOPHONIC_ON = 126;

        public const int AK_MIDI_CC_OMNI_POLYPHONIC_ON = 127;
    }
}