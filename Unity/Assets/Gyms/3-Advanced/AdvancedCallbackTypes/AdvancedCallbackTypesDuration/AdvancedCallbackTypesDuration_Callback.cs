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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCallbackTypesDuration_Callback : MonoBehaviour
{
	[SerializeField]
	TextMesh m_DurationText;

	public void DurationCallback(AkEventCallbackMsg callbackInfo)
	{
		switch (callbackInfo.type)
		{
		case AkCallbackType.AK_Duration:
			var DurationCallbackInfo = callbackInfo.info as AkDurationCallbackInfo;
			m_DurationText.text = "This Event duration is " + DurationCallbackInfo.fDuration.ToString() + " milliseconds";
			break;
		}
	}

}
