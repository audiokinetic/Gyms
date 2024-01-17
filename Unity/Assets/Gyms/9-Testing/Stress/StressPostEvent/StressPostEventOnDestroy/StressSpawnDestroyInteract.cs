using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressSpawnDestroyInteract : OnOffManager
{
    private GameObject Object = default;

    [SerializeField] private GameObject _prefabToSpawn = default;
    public override void OnAction()
    {
       Object = Instantiate(_prefabToSpawn);
       Object.gameObject.transform.position = gameObject.transform.position;
       Object.gameObject.transform.Translate(new Vector3(2, 0, 0));
    }

    public override void OffAction()
    {
        Destroy(Object);
    }
}
