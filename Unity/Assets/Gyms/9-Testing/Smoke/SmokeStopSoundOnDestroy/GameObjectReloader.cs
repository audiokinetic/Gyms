using UnityEngine;
using UnityEngine.Events;

public class GameObjectReloader : MonoBehaviour
{
    public GameObject objectToDestroy; // Reference to the GameObject to be destroyed and recreated
    private GameObject recreatedObject; // To store the recreated instance

    public void PostEvent()
    {
        objectToDestroy.GetComponent<AkEvent>().data.Post(objectToDestroy);
    }

    public void DestroyAndRecreateObject()
    {
        // Get the original object's position and rotation
        Vector3 originalPosition = objectToDestroy.transform.position;
        Quaternion originalRotation = objectToDestroy.transform.rotation;

        // Destroy the object (this triggers OnDestroy on the object's components)
        Destroy(objectToDestroy);

        // Recreate the object (this triggers Start on the new object's components)
        recreatedObject = Instantiate(objectToDestroy, originalPosition, originalRotation);
        foreach (var script in recreatedObject.GetComponents<Behaviour>())
        {
            if (!script.enabled)
            {
                script.enabled = true;

            }
        }
        objectToDestroy = recreatedObject;
        objectToDestroy.name = "GameObjectToReload";
    }
}