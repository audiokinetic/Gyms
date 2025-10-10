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
using System.Numerics;

namespace Wwise.AkCommandBuffer
{
    public static class AkVectorExtensions
    {
        public static Vector3 ToVector3(this AkVector AkVector)
        {
            return new Vector3(AkVector.X, AkVector.Y, AkVector.Z);
        }
        
        public static AkVector ToAkVector(this Vector3 AkVector)
        {
            return new AkVector
            {
                X = AkVector.X,
                Y = AkVector.Y,
                Z = AkVector.Z
            };
        }

        public static float Length(this AkVector akVector)
        {
            return (float)Math.Sqrt((akVector.X * akVector.X + akVector.Y * akVector.Y + akVector.Z * akVector.Z));
        }
        
        public static Vector<double> ToVector64(this AkVector64 AkVector)
        {
            return new Vector<double>(new double[]{AkVector.X, AkVector.Y, AkVector.Z});
        }
        
        public static AkVector64 ToAkVector64(this Vector<double> vector)
        {
            return new AkVector64
            {
                X = vector[0],
                Y = vector[1],
                Z = vector[2]
            };
        }
    }
}