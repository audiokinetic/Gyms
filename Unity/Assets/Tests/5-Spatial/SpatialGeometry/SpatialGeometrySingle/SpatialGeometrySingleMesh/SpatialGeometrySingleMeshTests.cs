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

using AK.Wwise;
using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using GeometryParameters = SpatialGeometrySingleManager.GeometryParameters;

namespace Tests
{
	public class SpatialGeometrySingleMeshTests : AcousticsGymTests
	{
		const string SceneName = "SpatialGeometrySingleMesh";

		const float MinTransmissionLoss = 0f;
		const float MaxTransmissionLoss = 1f;
		const float TransmissionLossVariation = 0.1f;

		private AkSurfaceReflector _akSurfaceReflector;
		private AkEvent _akEvent;
		private SpatialGeometrySingleManager _manager;

		bool _initialEnableDiffraction;
		bool _initialEnableDiffractionBoundaryEdges;
		bool _initialEnableGeometry;
		float[] _initialTransmissionLoss;
		int[] _initialAcousticTextureIndex;
		AcousticTexture[] _initialAcousticTexture;

		[UnityTest]
		public IEnumerator SpatialGeometrySingleMesh_Tests()
		{
			yield return StartTest(SceneName);

			InitializeParams();

			yield return PostEventTest(_akEvent);
			ToggleParamTest(GeometryParameters.Diffraction);
			ToggleParamTest(GeometryParameters.BoundaryEdges);
			ToggleParamTest(GeometryParameters.GeometryEnabled);
			AcousticTextureCyclingTest();
			TransmissionLossTest();
			RandomizeAndResetTest();

			AkUnitySoundEngine.StopAll();
			yield return FinishTest(SceneName);
		}

		private void InitializeParams()
		{
			_manager = SpatialGeometrySingleManager.Instance;
			_akEvent = GameObject.Find("SpatialEmitter").GetComponent<AkEvent>();
			_akSurfaceReflector = GameObject.Find("Geometry").GetComponent<AkSurfaceReflector>();

			_initialEnableDiffraction = _akSurfaceReflector.EnableDiffraction;
			_initialEnableDiffractionBoundaryEdges = _akSurfaceReflector.EnableDiffractionOnBoundaryEdges;
			_initialEnableGeometry = _akSurfaceReflector.enabled;
			_initialTransmissionLoss = new float[2]
			{
				_akSurfaceReflector.TransmissionLossValues[0],
				_akSurfaceReflector.TransmissionLossValues[1]
			};
			_initialAcousticTexture = new AcousticTexture[2]
			{
				_akSurfaceReflector.AcousticTextures[0],
				_akSurfaceReflector.AcousticTextures[1]
			};

			// Find indexes of initial acoustic textures
			_initialAcousticTextureIndex = new int[2];
			for (int i = 0; i < _manager.AcousticTextures.Count; ++i)
			{
				if (_manager.AcousticTextures[i] == _akSurfaceReflector.AcousticTextures[0])
					_initialAcousticTextureIndex[0] = i;

				if (_manager.AcousticTextures[i] == _akSurfaceReflector.AcousticTextures[1])
					_initialAcousticTextureIndex[1] = i;
			}
		}

		private void ToggleParamTest(GeometryParameters parameter)
		{
			switch (parameter)
			{
				case GeometryParameters.Diffraction:
					ToggleParamTest(
						GeometryParameters.Diffraction,
						() => _akSurfaceReflector.EnableDiffraction,
						_initialEnableDiffraction);
					break;

				case GeometryParameters.BoundaryEdges:
					ToggleParamTest(
						GeometryParameters.BoundaryEdges,
						() => _akSurfaceReflector.EnableDiffractionOnBoundaryEdges,
						_initialEnableDiffractionBoundaryEdges);
					break;

				case GeometryParameters.GeometryEnabled:
					ToggleParamTest(
						GeometryParameters.GeometryEnabled,
						() => _akSurfaceReflector.enabled,
						_initialEnableGeometry);
					break;
			}
		}

		private void ToggleParamTest(GeometryParameters parameter, Func<bool> getModifiedParameter, bool initialParameterValue)
		{
			_manager.ToggleParam(parameter);
			Assert.AreEqual(!initialParameterValue, getModifiedParameter());

			_manager.ToggleParam(parameter);
			Assert.AreEqual(initialParameterValue, getModifiedParameter());

			LogOutput($"Toggle {parameter.ToString()}: ", true);
		}

		private void AcousticTextureCyclingTest()
		{
			AcousticTextureCyclingTest(0);
			AcousticTextureCyclingTest(1);
		}

		private void AcousticTextureCyclingTest(int meshIdx)
		{
			int nbAcousticTextures = _manager.AcousticTextures.Count;
			for (int i = 1; i <= nbAcousticTextures; ++i)
			{
				int expectedIndex = (_initialAcousticTextureIndex[meshIdx] + i) % nbAcousticTextures;
				_manager.ChangeAcousticTexture(meshIdx);
				Assert.AreEqual(_manager.AcousticTextures[expectedIndex], _akSurfaceReflector.AcousticTextures[meshIdx]);
			}

			Assert.AreEqual(_initialAcousticTexture[meshIdx], _akSurfaceReflector.AcousticTextures[meshIdx]);
			LogOutput($"Cycle acoustic texture [mesh {meshIdx}]: ", true);
		}

		private void TransmissionLossTest()
		{
			TransmissionLossTest(0, 1);
			TransmissionLossTest(1, 0);
		}

		private void TransmissionLossTest(int meshIdxToModify, int unchangedMeshIdx)
		{
			ref float transmissionLossUnchanged = ref _akSurfaceReflector.TransmissionLossValues[unchangedMeshIdx];
			float initialTransmissionLossUnchanged = transmissionLossUnchanged;

			Debug.Log($"Transmission Loss variation [mesh {meshIdxToModify}]:");
			TestAllFloatVariations(
				() => _akSurfaceReflector.TransmissionLossValues[meshIdxToModify],
				+TransmissionLossVariation, -TransmissionLossVariation,
				() => _manager.IncreaseTransmissionLoss(meshIdxToModify, +TransmissionLossVariation),
				() => _manager.IncreaseTransmissionLoss(meshIdxToModify, -TransmissionLossVariation),
				MinTransmissionLoss, MaxTransmissionLoss);

			Assert.AreEqual(initialTransmissionLossUnchanged, transmissionLossUnchanged);
		}

		private void RandomizeAndResetTest()
		{
			_manager.RandomizeParams();
			_manager.ResetParams();

			Assert.AreEqual(_initialEnableDiffraction, _akSurfaceReflector.EnableDiffraction);
			Assert.AreEqual(_initialEnableDiffractionBoundaryEdges, _akSurfaceReflector.EnableDiffractionOnBoundaryEdges);
			Assert.AreEqual(_initialEnableGeometry, _akSurfaceReflector.enabled);
			Assert.AreEqual(_initialTransmissionLoss[0], _akSurfaceReflector.TransmissionLossValues[0], Tolerance);
			Assert.AreEqual(_initialTransmissionLoss[1], _akSurfaceReflector.TransmissionLossValues[1], Tolerance);
			Assert.AreEqual(_initialAcousticTexture[0], _akSurfaceReflector.AcousticTextures[0]);
			Assert.AreEqual(_initialAcousticTexture[1], _akSurfaceReflector.AcousticTextures[1]);
			LogOutput("Randomize and reset: ", true);
		}
	}
}
