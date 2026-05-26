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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SmokeEventWithNoMediaTests : GymTests
    {
        const string SceneName = "SmokeEventWithNoMedia";
        [UnityTest]
        public IEnumerator SmokeEventWithNoMedia_Tests()
        {
            yield return StartTest(SceneName);
            CallbackIncrement.CallbackCount = 0;

            uint expected = PostSilence();
            var EventButton = GameObject.Find("Button").GetComponentInChildren<AkEvent>();
            var StopButton = GameObject.Find("Button (1)").GetComponentInChildren<AkEvent>();
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
#if UNITY_WEBGL
            yield return EventButton.data.WwiseObjectReference.CompleteLoadBank();
            yield return StopButton.data.WwiseObjectReference.CompleteLoadBank();
#else
            yield return new WaitUntil(() => EventButton.data.WwiseObjectReference.CompleteLoadBank().IsCompleted);
            yield return new WaitUntil(() => StopButton.data.WwiseObjectReference.CompleteLoadBank().IsCompleted);
#endif
          
#endif
            EventButton.HandleEvent(EventButton.gameObject);
            yield return new WaitForSeconds(0.5f);
            StopButton.HandleEvent(EventButton.gameObject);
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(1, CallbackIncrement.CallbackCount);
            Assert.AreEqual(expected + 3, PostSilence());

            yield return FinishTest(SceneName);
        }
    }
}
