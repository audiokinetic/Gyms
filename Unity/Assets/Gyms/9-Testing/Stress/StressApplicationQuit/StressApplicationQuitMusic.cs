using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressAplicationQuitMusic : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;

    public AK.Wwise.Bank Bank;

    // Use this for initialization.
    void Start () {
        Bank.Load();
        MusicEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallbackFunction);
    }
    void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info){
        Debug.Log("Callback");
    }
}
