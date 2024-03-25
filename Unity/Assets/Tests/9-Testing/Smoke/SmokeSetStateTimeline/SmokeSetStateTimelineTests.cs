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
using UnityEngine;
using UnityEngine.Playables;

namespace Tests
{
    public class SmokeSetStateTimelineTests : GymTests
    {
        const string SceneName = "SmokeSetStateTimeline";
        [UnityTest]
        public IEnumerator SmokeSetStateTimeline_Tests()
        {
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();

            uint expected = PostSilence() + 2;
            LoadBank(bank);
            PlayableDirector timeline = GameObject.Find("SetSwitchTimeline").GetComponent<PlayableDirector>();
            timeline.Play();
            yield return new WaitForSeconds(0.2f);
            Assert.AreEqual(PostSilence(), expected);

            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
