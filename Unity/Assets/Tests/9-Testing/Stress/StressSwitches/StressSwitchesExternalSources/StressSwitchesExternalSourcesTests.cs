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
using System.Collections;
using UnityEngine.TestTools;

namespace Tests
{
    public class StressSwitchesExternalSourcesTests : GymTests
    {
        const string SceneName = "StressSwitchesExternalSources";
        [UnityTest]
        public IEnumerator StressSwitchesExternalSources_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            AkSwitch akSwitch = gameObject.GetComponent<AkSwitch>();
            uint expected = PostSilence();

            // Load the Event and the Switch.
            bank.data.Load();
            akSwitch.data.SetValue(gameObject);

            // Set ExternalSource Media and post the event. 01
            AkExternalSourceInfoArray _arrayTest = new AkExternalSourceInfoArray(1);
            _arrayTest[0].iExternalSrcCookie = AkSoundEngine.GetIDFromString("External_Source_Switch1");
            _arrayTest[0].szFile = "01.wem";
            _arrayTest[0].idCodec = 2;
            ExpectedLogError("01 is expecting to play");
            Assert.AreEqual(++expected, AkSoundEngine.PostEvent(akEvent.data.Id, gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "01", 1, _arrayTest));
            yield return new UnityEngine.WaitForSeconds(0.5f);

            // Set another media to the switch.
            _arrayTest[0].szFile = "04.wem";
            bank.data.Load();
            ExpectedLogError("04 is expecting to play");
            Assert.AreEqual(++expected, AkSoundEngine.PostEvent(akEvent.data.Id, gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "04", 1, _arrayTest));
            yield return new UnityEngine.WaitForSeconds(0.5f);

            // Unload all and expect Media 01.wem to be unloaded.
            bank.data.Unload();
            yield return new UnityEngine.WaitForSeconds(0.1f);
            // No way to check if 01.wem is unloaded.

            yield return FinishTest(SceneName);
        }
    }
}
