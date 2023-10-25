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
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AdvancedExternalSourcesTests : GymTests
    {
        const string SceneName = "AdvancedExternalSources";
        [UnityTest]
        public IEnumerator AdvancedExternalSources_Tests()
        {
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();

            LoadBank(bank);

            uint expected = AkSoundEngine.PostEvent("Silence", gameObject);

            AkExternalSourceInfoArray _arrayTest = new AkExternalSourceInfoArray(3);
            _arrayTest[0].iExternalSrcCookie = AkSoundEngine.GetIDFromString("One");
            _arrayTest[0].szFile = "01.wem";
            _arrayTest[0].idCodec = 2;

            _arrayTest[1].iExternalSrcCookie = AkSoundEngine.GetIDFromString("Two");
            _arrayTest[1].szFile = "02.wem";
            _arrayTest[1].idCodec = 2;

            _arrayTest[2].iExternalSrcCookie = AkSoundEngine.GetIDFromString("Three");
            _arrayTest[2].szFile = "03.wem";
            _arrayTest[2].idCodec = 2;

            //Post external source test
            uint id = AkSoundEngine.PostEvent("Post_ExternalAudio_Event", gameObject, 0, null, 0, 3, _arrayTest);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(++expected, id);
            LogOutput("Post an external source event: ", true);

            //Change external source test
            _arrayTest[0].szFile = "04.wem";
            _arrayTest[1].szFile = "05.wem";
            _arrayTest[2].szFile = "06.wem";

            id = AkSoundEngine.PostEvent("Post_ExternalAudio_Event", gameObject, 0, null, 0, 3, _arrayTest);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(++expected, id);
            LogOutput("Change the external source event: ", true);

            yield return FinishTest(SceneName);
        }
    }
}
