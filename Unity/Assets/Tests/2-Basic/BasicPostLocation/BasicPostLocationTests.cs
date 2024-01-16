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
using System;
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
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BasicPostLocationTests : GymTests
    {
        const string SceneName = "BasicPostLocation";
        [UnityTest]
        public IEnumerator BasicPostLocation_Tests()
        {
            yield return StartTest(SceneName);
            BasicPostLocation locationPost = GameObject.Find("Cylinder").GetComponent<BasicPostLocation>();
            Vector3 pos = locationPost.location;

            uint expected = PostSilence();

            yield return locationPost.PostTimer();
            BasicPostLocation_Event soundLocation = GameObject.Find("AkPostLocation").GetComponent<BasicPostLocation_Event>();
            AK.Wwise.Event locationEvent = soundLocation.akEvent.data;
            expected = locationEvent.PlayingId + 1;

            expected = PostSilence();
            LogOutput("Post on event ", true);
            Assert.AreEqual(expected, locationEvent.PlayingId + 1);
            
            //verify the position of the new GameObject
            yield return locationPost.PostTimer();
            Vector3 posObj = soundLocation.gameObject.transform.position;
            LogOutput("Expected the same postion ", true);
            Assert.AreEqual(pos, posObj);

            //verify is the gameObject exist after 2s
            yield return new WaitForSeconds(2);
            GameObject obj = GameObject.Find("AkPostLocation");
            LogOutput("Expected the object is null ", true);
            Assert.AreEqual(null, obj);


            //multiple click
            yield return locationPost.PostTimer();
            yield return new WaitForSeconds(.4f);
            yield return locationPost.PostTimer();
            yield return new WaitForSeconds(2);
            LogOutput("Expected the object is null ", true);
            Assert.AreEqual(null, obj);



            yield return FinishTest(SceneName);
        }

    }
}
