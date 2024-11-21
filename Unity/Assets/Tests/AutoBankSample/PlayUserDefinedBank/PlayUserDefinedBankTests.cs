using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
using AK.Wwise.Unity.WwiseAddressables;
#endif

namespace Tests
{
    public class PlayUserDefinedBankTests : GymTests
    {
        const string SceneName = "PlayUserDefinedBank";
        [UnityTest]
        public IEnumerator PlayUserDefinedBank_Tests()
        {
            yield return StartTest(SceneName);

            GameObject cylinder = GameObject.Find("Cylinder");
            AK.Wwise.Event _event = cylinder.GetComponent<AkEvent>().data;
            AkBank bank = gameObject.GetComponent<AkBank>();
            
            LoadBank(bank);
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            yield return new WaitUntil(() => bank.data.WwiseObjectReference.AddressableBank.LoadState == BankLoadState.Loaded);
#endif
            
            uint expected = PostSilence() + 1;

            //Post an event test
            uint id = _event.Post(cylinder);
            Assert.AreEqual(expected, id);
            LogOutput("Post an event from a user defined bank: ", true);

            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
