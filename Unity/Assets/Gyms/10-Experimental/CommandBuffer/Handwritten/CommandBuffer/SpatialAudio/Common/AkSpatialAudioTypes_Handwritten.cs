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
using System;
using System.Runtime.InteropServices;

namespace Wwise.AkCommandBuffer
{
    public class AkAcousticSurfaceSafeHandle: SafeHandle
    {
        private AkAcousticSurface _akAcousticSurfaceInternal;
        
        internal AkAcousticSurface AkAcousticSurfaceInternal => _akAcousticSurfaceInternal;

        private bool _ownsStrName = false;

        internal AkAcousticSurfaceSafeHandle(AkAcousticSurface internalSurface): base(new System.IntPtr(0), true)
        {
            _akAcousticSurfaceInternal = internalSurface;
        }
        
        public AkAcousticSurfaceSafeHandle(): base(new System.IntPtr(0), true)
        {
        }

        public static int InternalSize()
        {
            return Marshal.SizeOf<AkAcousticSurface>();
        }

        public uint TextureID
        {
            get => _akAcousticSurfaceInternal.TextureID;
            set => _akAcousticSurfaceInternal.TextureID = value;
        }

        public float TransmissionLoss
        {
            get => _akAcousticSurfaceInternal.TransmissionLoss;
            set => _akAcousticSurfaceInternal.TransmissionLoss = value;
        }

        public string StrName
        {
            get => Marshal.PtrToStringUTF8(_akAcousticSurfaceInternal._strName);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    FreeStrName();
                    return;
                }
                
                if (!_ownsStrName && _akAcousticSurfaceInternal._strName != IntPtr.Zero)
                {
                    throw new Exception($"Already using native string at {_akAcousticSurfaceInternal._strName}");
                }

                else if (_ownsStrName && _akAcousticSurfaceInternal._strName != IntPtr.Zero)
                {
                    FreeStrName();
                }

                _ownsStrName = true;
                _akAcousticSurfaceInternal._strName = Marshal.StringToCoTaskMemUTF8(value);
            }
        }

        private void FreeStrName()
        {
            if (_ownsStrName && _akAcousticSurfaceInternal._strName != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_akAcousticSurfaceInternal._strName);
                _akAcousticSurfaceInternal._strName = IntPtr.Zero;
            }
        }
        
        private void ReleaseUnmanagedResources()
        {
            FreeStrName();
        }
        
        protected override bool ReleaseHandle()
        {
            ReleaseUnmanagedResources();
            return true;
        }

        public override bool IsInvalid => false;
    }
    
    public class AkGeometryParamsSafeHandle: SafeHandle
    {
        private AkGeometryParams _akGeometryParamsInternal;

        public AkGeometryParamsSafeHandle() : base(new System.IntPtr(0), true)
        {
        }

        internal ref AkGeometryParams AkAcousticSurfaceInternal => ref _akGeometryParamsInternal;
        
        private bool _ownsTriangles = false;
        private bool _ownsVertices = false;
        private bool _ownsSurfaces = false;

        public static int InternalSize()
        {
            return Marshal.SizeOf<AkGeometryParams>();
        }
        
        public Span<AkTriangle> Triangles
        {
            get
            {
                unsafe
                {
                    return new Span<AkTriangle>((AkTriangle*)_akGeometryParamsInternal._triangles, NumTriangles);
                }
            }
            
            set
            {
                if (!_ownsTriangles && _akGeometryParamsInternal._triangles != IntPtr.Zero)
                {
                    throw new Exception($"Already using native memory at {_akGeometryParamsInternal._triangles}");
                }

                if (value == null || value.Length == 0)
                {
                    FreeTriangles();
                    _akGeometryParamsInternal.NumTriangles = 0;
                    return;
                }
                
                else if (_ownsTriangles && _akGeometryParamsInternal._triangles != IntPtr.Zero)
                {
                    FreeTriangles();
                }
                
                _ownsTriangles = true;
                // TODO: Possible optimization, there is a Marshal.ReAllocHGlobal
                _akGeometryParamsInternal._triangles = Marshal.AllocHGlobal(value.Length * Marshal.SizeOf<AkTriangle>() );
                
                unsafe
                {
                    WwiseMarshalHelper.SetFixedArray(value, (AkTriangle*)_akGeometryParamsInternal._triangles,  value.Length);
                }
                _akGeometryParamsInternal.NumTriangles = (ushort)value.Length;
            }
        }

        public ushort NumTriangles => _akGeometryParamsInternal.NumTriangles;

        public Span<AkVector> Vertices
        {
            get
            {
                unsafe
                {
                    return new Span<AkVector>((AkVector*)_akGeometryParamsInternal._vertices, NumVertices);
                }
            }
            
            set
            {
                if (!_ownsVertices && _akGeometryParamsInternal._vertices != IntPtr.Zero)
                {
                    throw new Exception($"Already using native memory at {_akGeometryParamsInternal._vertices}");
                }

                if (value == null || value.Length == 0)
                {
                    FreeVertices();
                    _akGeometryParamsInternal.NumVertices = 0;
                    return;
                }
                
                else if (_ownsVertices && _akGeometryParamsInternal._vertices != IntPtr.Zero)
                {
                    FreeVertices();
                }
                
                _ownsVertices = true;
                _akGeometryParamsInternal._vertices = Marshal.AllocHGlobal(value.Length * Marshal.SizeOf<AkVector>() );
                
                unsafe
                {
                    WwiseMarshalHelper.SetFixedArray(value, (AkVector*)_akGeometryParamsInternal._vertices, value.Length);
                }
                _akGeometryParamsInternal.NumVertices = (ushort)value.Length;
            }
        }

        public ushort NumVertices => _akGeometryParamsInternal.NumVertices;

        public AkAcousticSurfaceSafeHandle[] Surfaces
        {
            get
            {
                unsafe
                {
                    AkAcousticSurfaceSafeHandle[] result = new  AkAcousticSurfaceSafeHandle[_akGeometryParamsInternal.NumSurfaces];

                    var internalSurfaces = WwiseMarshalHelper.GetManagedArray(
                        (AkAcousticSurface*)_akGeometryParamsInternal._surfaces,
                        _akGeometryParamsInternal.NumSurfaces);
                    
                    for (var i = 0; i < _akGeometryParamsInternal.NumSurfaces; i++)
                    {
                        result[i] = new AkAcousticSurfaceSafeHandle(internalSurfaces[i]);
                    }

                    return result;
                }
            }
            
            set
            {
                if (!_ownsSurfaces && _akGeometryParamsInternal._surfaces != IntPtr.Zero)
                {
                    throw new Exception($"Already using native memory at {_akGeometryParamsInternal._surfaces}");
                }

                if (value == null || value.Length == 0)
                {
                    FreeSurfaces();
                    _akGeometryParamsInternal.NumSurfaces = 0;
                    return;
                }
                
                else if (_ownsSurfaces && _akGeometryParamsInternal._surfaces != IntPtr.Zero)
                {
                    FreeSurfaces();
                }
                
                _ownsSurfaces = true;
                _akGeometryParamsInternal._surfaces = Marshal.AllocHGlobal(value.Length * Marshal.SizeOf<AkAcousticSurface>() );
                
                unsafe
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        // Suppress null check. If we are here, AllocHGlobal has not thrown and _surfaces is not null
                        ((AkAcousticSurface*)_akGeometryParamsInternal._surfaces)![i] = value[i].AkAcousticSurfaceInternal;
                    }
                }
                
                _akGeometryParamsInternal.NumSurfaces = (ushort)value.Length;
            }
        }

        public ushort NumSurfaces => _akGeometryParamsInternal.NumSurfaces;

        public bool EnableDiffraction
        {
            get => _akGeometryParamsInternal._enableDiffraction != 0;
            set => _akGeometryParamsInternal._enableDiffraction = (byte)(value? 1 : 0);
        }

        public bool EnableDiffractionOnBoundaryEdges
        {
            get => _akGeometryParamsInternal._enableDiffractionOnBoundaryEdges != 0;
            set => _akGeometryParamsInternal._enableDiffractionOnBoundaryEdges = (byte)(value? 1 : 0);
        }

        private bool OwnsTriangles => _ownsTriangles;

        public void FreeTriangles()
        {
            if (OwnsTriangles && _akGeometryParamsInternal._triangles != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_akGeometryParamsInternal._triangles);
                _akGeometryParamsInternal._triangles = IntPtr.Zero;
                _akGeometryParamsInternal.NumTriangles = 0;
            }
        }
        
        public void FreeVertices()
        {
            if (_ownsVertices && _akGeometryParamsInternal._vertices != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_akGeometryParamsInternal._vertices);
                _akGeometryParamsInternal._vertices = IntPtr.Zero;
                _akGeometryParamsInternal.NumVertices = 0;
            }
        }
        
        public void FreeSurfaces()
        {
            if (_ownsSurfaces && _akGeometryParamsInternal._surfaces != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_akGeometryParamsInternal._surfaces);
                _akGeometryParamsInternal._surfaces = IntPtr.Zero;
                _akGeometryParamsInternal.NumSurfaces = 0;
            }
        }
        
        private void ReleaseUnmanagedResources()
        {
            FreeTriangles();
            FreeVertices();
            FreeSurfaces();
        }

        protected override bool ReleaseHandle()
        {
            ReleaseUnmanagedResources();
            return true;
        }

        public override bool IsInvalid => false;
    }
}