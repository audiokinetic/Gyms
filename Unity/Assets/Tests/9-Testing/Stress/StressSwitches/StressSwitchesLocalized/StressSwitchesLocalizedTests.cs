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
    public class StressSwitchesLocalizedTests : GymTests
    {
        const string SceneName = "StressSwitchesLocalized";
        [UnityTest]
        public IEnumerator StressSwitchesLocalized_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            AkSwitch akSwitch = gameObject.GetComponent<AkSwitch>();
            uint expected = PostSilence();

            // Loading Play_Localized_Switch, Localized_Switch, set it to Voice1 and post it.Expect Hello and en_US marker callback.
            bank.data.Load();
            akSwitch.data.SetValue(gameObject);
            ExpectedLogError("Hello is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckLocalizedFileEnglishPlaying, "Hello"));
            yield return new UnityEngine.WaitForSeconds(0.1f);


            // Changing localization to French and posting the event. Expect Hello and fr_FR marker callback.
            bank.data.Unload();
            AkSoundEngine.SetCurrentLanguage("fr_FR");
            yield return new UnityEngine.WaitForSeconds(0.1f);
            bank.data.Load();
            Assert.AreEqual("fr_FR", AkSoundEngine.GetCurrentLanguage());
            ExpectedLogError("Hello is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckLocalizedFileFrenchPlaying, "Hello"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Unloading Switch, make sure the Media is unloaded. ShouldNotPlay
            // Currently no way to only unload the Switch.
            Assert.IsTrue(false);


            yield return FinishTest(SceneName);
        }
        
        void CheckLocalizedFileEnglishPlaying(object in_cookie, AkCallbackType in_type, object in_info)
        {
            if (in_type == AkCallbackType.AK_Marker)
            {
                if (((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie)
                {
                    Assert.IsTrue(((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie, string.Format("Expected {0} to play, but it was {1}", (string)in_cookie, ((AkMarkerCallbackInfo)in_info).strLabel));
                    UnityEngine.Debug.LogError(string.Format("{0} is expecting to play (This is an expected error)", (string)in_cookie));
                }
                else
                {
                    Assert.IsTrue(((AkMarkerCallbackInfo)in_info).strLabel == "en_US", string.Format("Expected {0} to play, but it was {1}", "en_US", ((AkMarkerCallbackInfo)in_info).strLabel));
                }
            }
        }
        void CheckLocalizedFileFrenchPlaying(object in_cookie, AkCallbackType in_type, object in_info)
        {
            if (in_type == AkCallbackType.AK_Marker)
            {
                if (((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie)
                {
                    Assert.IsTrue(((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie, string.Format("Expected {0} to play, but it was {1}", (string)in_cookie, ((AkMarkerCallbackInfo)in_info).strLabel));
                    UnityEngine.Debug.LogError(string.Format("{0} is expecting to play (This is an expected error)", (string)in_cookie));
                }
                else
                {
                    Assert.IsTrue(((AkMarkerCallbackInfo)in_info).strLabel == "fr_FR", string.Format("Expected {0} to play, but it was {1}", "fr_FR", ((AkMarkerCallbackInfo)in_info).strLabel));
                }
            }
        }
    }
}
