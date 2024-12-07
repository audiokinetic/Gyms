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

using UnityEngine;

public class SpatialRadialEmitterParams_Interact : ButtonWithTextManager
{
    public enum Radius
    {
        OuterRadius,
        InnerRadius
    }

    [SerializeField] private AkRadialEmitter _radialEmitter;
    [SerializeField] private float _variation;
    [SerializeField] private Radius _radius;

    private Color _defaultDescriptionColor = new Color(0.2f, 1f, 0.65f, 1f);
    private float _maxOuterRadius = 200000000f;


    private void OnValidate()
	{
		UpdateDescriptionColor(_defaultDescriptionColor);
		UpdateValue(
			_variation >= 0 ? "+" + _variation : _variation.ToString(),
			_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
		);

		if (_radius == Radius.OuterRadius)
		{
			UpdateDescriptionText("Change\nOuter Radius");
		}
		else if (_radius == Radius.InnerRadius)
		{
			UpdateDescriptionText("Change\nInner Radius");
		}
	}

	public void OnInteract()
    {
        if (_radialEmitter == null)
        {
            Debug.LogWarning("SpatialRadialEmitterParams_Interact: RadialEmitter is null.");
            return;
        }

        float innerRadius = 0;
        float outerRadius = 0;
        if (_radius == Radius.OuterRadius)
        {
            outerRadius = _radialEmitter.outerRadius + _variation;
            _radialEmitter.outerRadius = Mathf.Clamp(outerRadius, _radialEmitter.innerRadius, _maxOuterRadius);
        }
        else
        {
            innerRadius = _radialEmitter.innerRadius + _variation;
            _radialEmitter.innerRadius = Mathf.Clamp(innerRadius, 0f, _radialEmitter.outerRadius);
        }

        SpatialRadialEmitterManager.instance.UpdateUI();
    }
}
