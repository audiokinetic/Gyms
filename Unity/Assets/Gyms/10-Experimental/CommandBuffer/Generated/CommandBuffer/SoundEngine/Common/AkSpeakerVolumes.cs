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

    // TODO Use LibraryImport instead of DllImport as soon as Unity gets to .NET 7
    internal static partial class AkSpeakerVolumes
    {

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Copy", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Copy([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_CopyAndApplyGain", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_CopyAndApplyGain([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels, [System.Runtime.InteropServices.In]float in_fGain);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Zero", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Zero([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Add", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Add(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_L1Norm", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern float AK_SpeakerVolumes_Vector_L1Norm([System.Runtime.InteropServices.In]System.IntPtr in_pVolumes, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_MulScalar", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_MulScalar(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]float in_fVol, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Mul", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Mul(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Max", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Max(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_Min", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Vector_Min(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_GetNumElements", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SpeakerVolumes_Vector_GetNumElements([System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Vector_GetRequiredSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SpeakerVolumes_Vector_GetRequiredSize([System.Runtime.InteropServices.In]uint in_uNumChannels);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_GetRequiredSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SpeakerVolumes_Matrix_GetRequiredSize([System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_GetNumElements", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SpeakerVolumes_Matrix_GetNumElements([System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_GetChannel", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_SpeakerVolumes_Matrix_GetChannel([System.Runtime.InteropServices.In]System.IntPtr in_pVolumeMx, [System.Runtime.InteropServices.In]uint in_uIdxChannelIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_GetChannel_Const", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_SpeakerVolumes_Matrix_GetChannel_Const([System.Runtime.InteropServices.In]System.IntPtr in_pVolumeMx, [System.Runtime.InteropServices.In]uint in_uIdxChannelIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_Copy", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_Copy([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_CopyAndApplyGain", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_CopyAndApplyGain([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut, [System.Runtime.InteropServices.In]float in_fGain);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_Zero", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_Zero([System.Runtime.InteropServices.Out]System.IntPtr out_pVolumesDst, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_Mul", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_Mul(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]float in_fVol, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_Add", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_Add(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_MAdd", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_MAdd(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut, [System.Runtime.InteropServices.In]float in_fGain);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_AbsMax", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_AbsMax(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SpeakerVolumes_Matrix_Max", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_SpeakerVolumes_Matrix_Max(System.IntPtr io_pVolumesDst, [System.Runtime.InteropServices.In]System.IntPtr in_pVolumesSrc, [System.Runtime.InteropServices.In]uint in_uNumChannelsIn, [System.Runtime.InteropServices.In]uint in_uNumChannelsOut);

    }
}