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
    internal static partial class AkExternalSourceArray
    {

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_Create", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_ExternalSourceArray_Create(uint capacity);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_CreateFromData", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_ExternalSourceArray_CreateFromData([System.Runtime.InteropServices.In]uint in_uNumSrcs, [System.Runtime.InteropServices.In]ref AkExternalSourceInfo in_pSources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_AddInMemorySource", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern int AK_ExternalSourceArray_AddInMemorySource([System.Runtime.InteropServices.In]System.IntPtr in_arSources, [System.Runtime.InteropServices.In]uint in_codec, [System.Runtime.InteropServices.In]uint in_cookie, [System.Runtime.InteropServices.In]System.IntPtr in_pInMemory, [System.Runtime.InteropServices.In]uint in_uiMemorySize);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_AddFileNameSource", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern int AK_ExternalSourceArray_AddFileNameSource([System.Runtime.InteropServices.In]System.IntPtr in_arSources, [System.Runtime.InteropServices.In]uint in_codec, [System.Runtime.InteropServices.In]uint in_cookie, [System.Runtime.InteropServices.In][System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPUTF8Str)]string in_filename);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_AddFileIDSource", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern int AK_ExternalSourceArray_AddFileIDSource([System.Runtime.InteropServices.In]System.IntPtr in_arSources, [System.Runtime.InteropServices.In]uint in_codec, [System.Runtime.InteropServices.In]uint in_cookie, [System.Runtime.InteropServices.In]uint in_fileID);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_Length", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_ExternalSourceArray_Length([System.Runtime.InteropServices.In]System.IntPtr in_arSources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_Capacity", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern uint AK_ExternalSourceArray_Capacity([System.Runtime.InteropServices.In]System.IntPtr in_arSources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_Data", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern System.IntPtr AK_ExternalSourceArray_Data([System.Runtime.InteropServices.In]System.IntPtr in_arSources);

        [System.Runtime.InteropServices.DllImport("AkUnitySoundEngine", EntryPoint="AK_ExternalSourceArray_Destroy", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        internal static extern void AK_ExternalSourceArray_Destroy([System.Runtime.InteropServices.In]System.IntPtr in_arSources);

    }
}