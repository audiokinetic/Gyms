using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackIncrement : MonoBehaviour
{
    public static int CallbackCount = 0;

    void Increment()
    {
        CallbackCount++;
    }
}
