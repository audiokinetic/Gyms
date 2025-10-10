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
    internal static partial class AkCommandBuffer
    {

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_CmdSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_CmdSize([System.Runtime.InteropServices.In]AkCommand in_cmd_id);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_MinSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_MinSize();

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Create", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_Create([System.Runtime.InteropServices.In]nuint in_size);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Destroy", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_CommandBuffer_Destroy([System.Runtime.InteropServices.In]System.IntPtr in_buffer);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Init", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_Init([System.Runtime.InteropServices.Out]System.IntPtr out_buffer, [System.Runtime.InteropServices.In]nuint in_size);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Add", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_Add([System.Runtime.InteropServices.In]System.IntPtr in_buffer, [System.Runtime.InteropServices.In]AkCommand in_cmd_id);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_StringSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_StringSize([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)]string str);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_AddString", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_AddString([System.Runtime.InteropServices.In]System.IntPtr in_buffer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)]string str);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_ArraySize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_ArraySize(nuint item_size, ushort num_items);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_AddArray", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_AddArray([System.Runtime.InteropServices.In]System.IntPtr in_buffer, nuint item_size, ushort num_items, System.IntPtr items);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_ExternalSourcesSize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_ExternalSourcesSize([System.Runtime.InteropServices.In]uint in_uNumSources, [System.Runtime.InteropServices.In]ref AkExternalSourceInfo in_sources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_AddExternalSources", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_AddExternalSources([System.Runtime.InteropServices.In]System.IntPtr in_buffer, [System.Runtime.InteropServices.In]uint in_uNumSources, [System.Runtime.InteropServices.In]ref AkExternalSourceInfo in_pSources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_GeometrySize", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern nuint AK_CommandBuffer_GeometrySize([System.Runtime.InteropServices.In]ref AkGeometryParams in_geometryParams);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_AddGeometry", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_CommandBuffer_AddGeometry([System.Runtime.InteropServices.In]System.IntPtr in_buffer, [System.Runtime.InteropServices.In]ref AkGeometryParams in_geometryParams);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Remove", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_CommandBuffer_Remove([System.Runtime.InteropServices.In]System.IntPtr in_buffer);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Submit", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_CommandBuffer_Submit([System.Runtime.InteropServices.In]System.IntPtr in_buffer);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_SubmitNonBlocking", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_CommandBuffer_SubmitNonBlocking([System.Runtime.InteropServices.In]System.IntPtr in_buffer);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Begin", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_CommandBuffer_Begin([System.Runtime.InteropServices.In]System.IntPtr in_buffer, [System.Runtime.InteropServices.Out]out AkCommandBufferIterator out_iterator);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_CommandBuffer_Next", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern int AK_CommandBuffer_Next([System.Runtime.InteropServices.In][System.Runtime.InteropServices.Out]ref AkCommandBufferIterator inout_iterator);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SoundEngine_GeneratePlayingID", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SoundEngine_GeneratePlayingID();

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SoundEngine_GetIDFromString", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SoundEngine_GetIDFromString([System.Runtime.InteropServices.In][System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)]string in_pszString);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SoundEngine_GetIDFromStringW", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_SoundEngine_GetIDFromStringW([System.Runtime.InteropServices.In][System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string in_pszString);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_SoundEngine_GetOutputID", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern ulong AK_SoundEngine_GetOutputID([System.Runtime.InteropServices.In]uint in_idShareset, [System.Runtime.InteropServices.In]uint in_idDevice);

    }
}