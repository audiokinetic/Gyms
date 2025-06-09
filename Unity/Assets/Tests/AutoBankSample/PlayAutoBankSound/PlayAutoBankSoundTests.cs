using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayAutoBankSoundTests : GymTests
    {
        const string SceneName = "PlayAutoBankSound";
        [UnityTest]
        public IEnumerator PlayAutoBankSound_Tests()
        {
            yield return StartTest(SceneName);
            
            GameObject cylinder = GameObject.Find("Cylinder");
            AK.Wwise.Event _event = cylinder.GetComponent<AkEvent>().data;
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
#if UNITY_WEBGL
            yield return _event.WwiseObjectReference.CompleteLoadBank();
#else
            yield return new WaitUntil(() => _event.WwiseObjectReference.CompleteLoadBank().IsCompleted);
#endif
#endif

            uint expected = PostSilence() + 1;

            //Post an event test
            uint id = _event.Post(cylinder);
            Assert.AreEqual(expected, id);
            LogOutput("Post an event from an autobank: ", true);


            yield return FinishTest(SceneName);
        }
    }
}
