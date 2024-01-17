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
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BasicFootstepsDynamicTests : GymTests
    {
        const string SceneName = "BasicFootstepsDynamic";
        [UnityTest]
        public IEnumerator BasicFootstepsDynamic_Tests()
        {
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            GameObject go = GameObject.Find("FPSController");
            BasicFootstepsDyamic footstepsDyamic = go.GetComponent<BasicFootstepsDyamic>();

            LoadBank(bank);
            var switches = gameObject.GetComponents<AkSwitch>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            
            uint expected = PostSilence();
            expected = akEvent.playingId + 1;
            LogOutput("Post on event ", true);
            Assert.AreEqual(expected, akEvent.playingId + 1);
            
            //Test the raycast 
            AK.Wwise.Switch switchPlay = footstepsDyamic.RaycastDown();
            LogOutput("The first Raycast must play Grass ", true);
            Assert.AreEqual(switches[0].data.Name, switchPlay.Name);

            //Test with a new location
            go.transform.position = new Vector3(0, go.transform.position.y, go.transform.position.z);
            switchPlay = footstepsDyamic.RaycastDown();
            LogOutput("The second Raycast must play Wood ", true);
            Assert.AreEqual(switches[2].data.Name, switchPlay.Name);

            //Test in the sky
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y * 10, go.transform.position.z);
            switchPlay = footstepsDyamic.RaycastDown();
            LogOutput("The third Raycast must play nothing ", true);
            Assert.AreEqual(null, switchPlay);
            
            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
