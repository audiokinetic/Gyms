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
using UnityEngine;
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
using AK.Wwise.Unity.WwiseAddressables;
#endif
using UnityEngine.TestTools;

namespace Tests
{
    public class InitBankReloadTests : GymTests
    {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
        private WwiseAddressableSoundBank bank;
#endif
        private bool firstCall = true;
        private DateTime startTime;
        private bool timedOut = false;
        private Task loadingBank;
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES && UNITY_WEBGL
        private float timeOut = 10.0f;
#elif UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
        private float timeOut = 2.5f;
#endif

        public static void LoadInitBank()
        {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            AK.Wwise.Unity.WwiseAddressables.AkAddressableBankManager.Instance.LoadInitBank(AkWwiseInitializationSettings.Instance.LoadBanksAsynchronously);
#else
            AkBankManager.LoadInitBank();
#endif
        }

        public static void UnloadInitBank()
        {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            AK.Wwise.Unity.WwiseAddressables.AkAddressableBankManager.Instance.UnloadInitBank();
            AkAddressableBankManager.Instance.DoUnloadBank();
#else
            AkBankManager.UnloadInitBank();
            AkBankManager.DoUnloadBanks();
#endif
        }
        private static void CheckRefCount(int count)
        {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            AkAddressableBankManager.BankHandles.TryGetValue("Init", out var handle);
            Assert.IsTrue(handle.RefCount == count);
#else
            //Not implemented
#endif
        }

        private static void CheckIsInHandleDict(bool isInDict)
        {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            Assert.IsTrue(AkAddressableBankManager.BankHandles.ContainsKey("Init") == isInDict);
#else
            //Not implemented
#endif
        }
        
private IEnumerator WaitForBankLoad()
        {
#if UNITY_WEBGL
            yield return CompleteLoadBank();
#else
            loadingBank = Task.Run(CompleteLoadBank);
            yield return new WaitUntil(() => loadingBank.IsCompleted);
#endif
        }
        
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES && UNITY_WEBGL
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
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
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
#endif
            firstCall = true;
        }
        
        const string SceneName = "InitBankReload";
        [UnityTest]
        public IEnumerator InitBankReload_Tests()
        {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            bank = AkAddressableBankManager.InitBank;
#endif
            yield return StartTest(SceneName);
            yield return WaitForBankLoad();
            // Basic reload
            UnloadInitBank();
            CheckIsInHandleDict(false);
                
            LoadInitBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            
            CheckRefCount(1);
           
            // Unload with 2 refs
            LoadInitBank();
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
            CheckRefCount(2);
#else
            ExpectedLogError("WwiseUnity: Failed load Init.bnk");            
#endif
            UnloadInitBank();
            CheckIsInHandleDict(false);
            
            // Reload and ref should be at 1
            LoadInitBank();
            yield return WaitForBankLoad();
            CheckIsInHandleDict(true);
            CheckRefCount(1);

            yield return FinishTest(SceneName);
        }
    }
}
