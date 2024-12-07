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
using UnityEngine.UI;

public class ButtonWithTextManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Text component where to print this button's description.")]
    Text _descriptionText = default;
    [SerializeField]
    [Tooltip("The Text component where to print this button's value.")]
    Text _valueText = default;

    protected void UpdateDescriptionText(string in_text)
    {
        if (_descriptionText != null)
        {
            UpdateText(_descriptionText, in_text);
        }
    }
    protected void UpdateDescriptionColor(Color in_color)
    {
        if (_descriptionText != null)
        {
            UpdateColor(_descriptionText, in_color);
        }
    }

    protected void UpdateValue(string in_text, Color in_color)
	{
        UpdateValueText(in_text);
        UpdateValueColor(in_color);
    }

    protected void UpdateValueText(string in_text)
    {
        if (_valueText != null)
        {
            UpdateText(_valueText, in_text);
        }
    }

    protected void UpdateValueColor(Color in_color)
    {
        if (_valueText != null)
        {
            UpdateColor(_valueText, in_color);
        }
    }

    private void UpdateText(Text in_text, string in_string)
    {
        if (in_text.text != in_string)
        {
            in_text.text = in_string;
        }
    }

    private void UpdateColor(Text in_text, Color in_color)
    {
        if (in_text.color != in_color)
        {
            in_text.color = in_color;
        }
    }
}
