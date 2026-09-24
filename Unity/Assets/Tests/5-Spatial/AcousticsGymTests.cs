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

using NUnit.Framework;
using System;
using UnityEngine;

public class AcousticsGymTests : GymTests
{
	protected const float Tolerance = 0.001f;

	protected void TestFloatVariation(Func<float> getValue, float variation, Action variationAction, int nbIterations, float min, float max, float tolerance = Tolerance)
	{
		float initialValue = getValue();
		for (int i = 1; i <= nbIterations; ++i)
		{
			variationAction.Invoke();
			float expected = Mathf.Clamp(initialValue + variation * i, min, max);
			Assert.AreEqual(expected, getValue(), tolerance);
		}
	}

	protected void TestFloatVariation(Func<float> getValue, float newValue, float variation, Action variationAction, float min, float max, float tolerance = Tolerance)
	{
		float initialValue = getValue();
		float nbIterations = Mathf.CeilToInt(Mathf.Abs(getValue() - newValue) / Mathf.Abs(variation));
		for (int i = 1; i <= nbIterations; ++i)
		{
			variationAction.Invoke();
			float expected = Mathf.Clamp(initialValue + variation * i, min, max);
			Assert.AreEqual(expected, getValue(), tolerance);
		}
	}

	protected void TestClampMax(Func<float> getValue, float variationInc, Action incremenAction, float max, float tolerance = Tolerance)
	{
		// Test increment to max
		TestFloatVariation(getValue, max, variationInc, incremenAction, float.MinValue, max, tolerance);

		// Test clamp to max
		incremenAction.Invoke();
		Assert.AreEqual(max, getValue());
	}

	protected void TestClampMin(Func<float> getValue, float variationDec, Action decrementAction, float min, float tolerance = Tolerance)
	{
		// Test decrement to max
		TestFloatVariation(getValue, min, variationDec, decrementAction, min, float.MaxValue, tolerance);

		// Test clamp to max
		decrementAction.Invoke();
		Assert.AreEqual(min, getValue());
	}

	protected void TestAllFloatVariations(Func<float> getValue, float variationInc, float variationDec, Action incrementAction, Action decrementAction, float min, float max, float tolerance = Tolerance)
	{
		int nbIterations = 2;

		// Test increment
		float lastValueBeforeInc = getValue();
		TestFloatVariation(getValue,
			variationInc, incrementAction.Invoke, nbIterations,
			min, max, tolerance);
		Assert.That(getValue(), Is.GreaterThan(lastValueBeforeInc).Or.EqualTo(lastValueBeforeInc).Within(tolerance));
		Debug.Log($"\tIncrement {nbIterations} times: true");

		// Test decrement
		float lastValueBeforeDec = getValue();
		TestFloatVariation(getValue,
			variationDec, decrementAction.Invoke, nbIterations,
			min, max, tolerance);
		Assert.That(getValue(), Is.LessThan(lastValueBeforeInc).Or.EqualTo(lastValueBeforeInc).Within(tolerance));
		Debug.Log($"\tDecrement {nbIterations} times: true");


		if (max != float.MaxValue)
		{
			// Test clamp max
			TestClampMax(getValue, variationInc, incrementAction, max, tolerance);
			Debug.Log("\tClamp to max: true");
		}

		if (min != float.MinValue)
		{
			// Test clamp min
			TestClampMin(getValue, variationDec, decrementAction, min, tolerance);
			Debug.Log("\tClamp to min: true");
		}
	}
}
