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

using System;
using NUnit.Framework;
using System.Collections;
using System.Threading.Tasks;
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
using AK.Wwise.Unity.WwiseAddressables;
#endif
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BanksReloadTests : GymTests
    {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
        private WwiseAddressableSoundBank bank;
        private bool firstCall = true;
        private DateTime startTime;
        private bool timedOut = false;
        private Task loadingBank;
#if UNITY_WEBGL
        private float timeOut = 10.0f;
#else
        private readonly float timeOut = 5.0f;
#endif
        WwiseEventReference eventRef;

        private IEnumerator WaitForBankLoad()
        {
#if UNITY_WEBGL
            yield return CompleteLoadBank();
#else
            loadingBank = Task.Run(CompleteLoadBank);
            yield return new WaitUntil(() => loadingBank.IsCompleted);
#endif
        }
        
        private IEnumerator WaitForBankUnload()
        {
#if UNITY_WEBGL
            yield return UnloadAutoBankWebGL();
#else
            yield return UnloadAutoBank();
            yield return null;
#endif
        }
        
#if UNITY_WEBGL
        private async Awaitable CompleteLoadBank()
#else
        private async Task CompleteLoadBank()
#endif
        {
            if (firstCall)
            {
                startTime = DateTime.Now;
                timedOut = false;
            }
            while (bank.LoadState == BankLoadState.Loading || bank.LoadState == BankLoadState.WaitingForPrepareEvent || bank.LoadState == BankLoadState.WaitingForInitBankToLoad)
            {
                firstCall = false;
                var elapsedTime = DateTime.Now - startTime;
                if (elapsedTime.TotalSeconds >= timeOut)
                {
                    timedOut = true;
                    firstCall = true;
                    break;
                }
#if UNITY_WEBGL
                await Awaitable.NextFrameAsync();
#else
                await Task.Yield();
#endif
            }
            firstCall = true;
        }
        private void LoadBank()
        {
            AkAddressableBankManager.Instance.LoadBank(bank);
        }

        private void LoadAutoBank()
        {
            eventRef.LoadAutoBank();
        }
        
        private void UnloadBank(bool ignoreRef = true, bool removeFromDict = false)
        {
            AkAddressableBankManager.Instance.UnloadBank(bank, ignoreRef, removeFromDict);
            AkAddressableBankManager.Instance.DoUnloadBank();
        }

        private IEnumerator UnloadAutoBankWebGL(bool ignoreRef = true, bool removeFromDict = false)
        {
            yield return eventRef.UnloadAutoBank();
            AkAddressableBankManager.Instance.DoUnloadBank();
        }

        private IEnumerator UnloadAutoBank()
        {
            yield return eventRef.UnloadAutoBank();
            AkAddressableBankManager.Instance.DoUnloadBank();
        }
        
        private void CheckRefCount(int count)
        {
            AkAddressableBankManager.BankHandles.TryGetValue(AkAddressableBankManager.GetBankHandleName(bank), out var handle);
            Assert.IsTrue(handle.RefCount == count);
        }
        
        private void CheckIsInHandleDict(bool isInDict)
        {
            Assert.IsTrue(AkAddressableBankManager.BankHandles.ContainsKey(AkAddressableBankManager.GetBankHandleName(bank)) == isInDict);
        }
#else
        private string bankName;
        private void LoadBank()
        {
            AkBankManager.LoadInitBank();
        }

        private void UnloadBank()
        {
            AkBankManager.UnloadBank(bankName);
            AkBankManager.DoUnloadBanks();
        }

        private void CheckRefCount(int count)
        {
            //Not Implemented
        }

        private void CheckIsInHandleDict(int count)
        {
            //Not Implemented
        }
#endif
        const string SceneName = "BanksReload";
        [UnityTest]
        public IEnumerator BanksReload_Tests()
        {
            yield return StartTest(SceneName);
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            // User Banks
            var akBank = GameObject.Find("BankHolder").GetComponent<AkBank>();
            WwiseBankReference bankRef = (WwiseBankReference)akBank.data.ObjectReference;
            bank = bankRef.AddressableBank;

            yield return WaitForBankLoad();

            //Check that bank is properly loaded
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            
            //Simple unload while ignoring ref
            UnloadBank(); //Ignoring Ref
            CheckIsInHandleDict(false);
            
            //Multiple ref | Unload ignoring ref
            LoadBank();
            yield return WaitForBankLoad();
           
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            LoadBank();
            CheckRefCount(2);
            
            UnloadBank(); //Ignoring Ref
            CheckIsInHandleDict(false);
            
            //Multiple ref | Unload considering ref
            LoadBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            LoadBank();
            CheckRefCount(2);
            UnloadBank(false); //Considering Ref
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            UnloadBank(false); //Considering Ref
            CheckIsInHandleDict(false);
            
            //Unloading init bank shouldn't automatically unload the bank
            LoadBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            InitBankReloadTests.UnloadInitBank();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            UnloadBank();
            
            //It should however stop bank from loading
            LoadBank();
            yield return WaitForBankLoad();
            yield return new WaitForSeconds(1);
            Assert.IsTrue(timedOut);
            ExpectedLogError("UserDefinedBank bank will be loaded after the init bank is loaded", 1, LogType.Log);
            InitBankReloadTests.LoadInitBank();
            yield return WaitForBankLoad();
            Assert.IsFalse(timedOut);
            
            // Auto Banks
            var akEvent = GameObject.Find("BankHolder").GetComponent<AkEvent>();
            eventRef = (WwiseEventReference)akEvent.data.ObjectReference;
            bank = eventRef.AutoBank;
            
            yield return WaitForBankLoad();  
            
            //Check that bank is properly loaded
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            
            //Simple unload
            yield return WaitForBankUnload();

            CheckIsInHandleDict(false);

            
            LoadAutoBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            yield return FinishTest(SceneName);
            
            //Multiple ref | Unloading will always ignore ref when working with autobank
            //This is intended, there should only be one eventReference (that can count the ref), but won't be used.
            LoadAutoBank();
            CheckRefCount(2);
            
            yield return WaitForBankUnload();
            CheckIsInHandleDict(false);
            
            //Unloading init bank shouldn't automatically unload the bank
            LoadBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            InitBankReloadTests.UnloadInitBank();
            CheckIsInHandleDict(true);
            CheckRefCount(1);
            UnloadBank();
            //It should however stop bank from loading
            LoadBank();
            yield return WaitForBankLoad();
            yield return new WaitForSeconds(1);
            Assert.IsTrue(timedOut);
            ExpectedLogError("Ambient_Event bank will be loaded after the init bank is loaded", 1, LogType.Log);
            InitBankReloadTests.LoadInitBank();
            yield return WaitForBankLoad();
            Assert.IsFalse(timedOut);
#endif
            yield return FinishTest(SceneName);
        }
    }
}
