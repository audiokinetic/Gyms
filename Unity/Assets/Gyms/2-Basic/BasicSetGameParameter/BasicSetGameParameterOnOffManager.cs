using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSetGameParameterOnOffManager : OnOffManager
{
    [SerializeField]
    AkAmbient _ambient = default;
    public override void OnAction()
    {
        _ambient.data.Post(_ambient.gameObject);
    }

    public override void OffAction()
    {
        _ambient.Stop(0);
    }
}
