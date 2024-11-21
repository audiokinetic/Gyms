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
using UnityEngine.Playables;
using UnityEngine.TestTools;

namespace Tests
{
    public class SmokeTimelineInteractiveMusicSeekTests : TimelineGymTests
    {
        const string SceneName = "SmokeTimelineInteractiveMusicSeek";
        [UnityTest]
        public IEnumerator SmokeTimelineInteractiveMusicSeek_Tests()
        {
#if UNITY_EDITOR
            float waitTime = 0.1f;
#else
            float waitTime = 0.2f;
#endif
            yield return StartTest(SceneName);

            var timeline = GameObject.FindFirstObjectByType<PlayableDirector>();
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            yield return LoadAllTimeLineEvent(timeline);
#endif
            timeline.time = 0f;

            uint firstSilence = PostSilence();
            timeline.Play();
            yield return new WaitForSeconds(waitTime);
            timeline.time = 2f;
            yield return new WaitForSeconds(waitTime);
            timeline.time = 1f;
            //Goes past the duration of the timeline. Loops and start another event.
            yield return new WaitForSeconds(waitTime);
            timeline.time = 20f;
            
            yield return new WaitForSeconds(waitTime);
            timeline.time = -1f;

            uint secondSilence = PostSilence();
            Assert.Greater(secondSilence, firstSilence + 5);
            Assert.Less(secondSilence, firstSilence + 15);
            yield return FinishTest(SceneName);
        }
    }
}
