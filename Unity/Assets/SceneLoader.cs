using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public string[] gymNames;
    public TextMesh textPrefab; // Drag your TextMeshPro object prefab here in the Inspector.
    public float radius = 10f; // Set your desired radius here.

    void Start()
    {
        StartCoroutine(LoadSublevels());
    }

    IEnumerator LoadSublevels()
    {
        float angleIncrement = 360f / gymNames.Length;

        for (int i = 0; i < gymNames.Length; i++)
        {
            string gymName = gymNames[i];

            float angle = i * angleIncrement;
            Vector3 position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, 0f, Mathf.Sin(Mathf.Deg2Rad * angle) * radius);

            LoadSceneParameters sceneParameters = new LoadSceneParameters();
            sceneParameters.loadSceneMode = LoadSceneMode.Additive;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(gymName, LoadSceneMode.Additive);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            Scene loadedScene = SceneManager.GetSceneByName(gymName);

            Quaternion rotation;
            
            if (angle > 180f)
            {
                rotation = Quaternion.FromToRotation(Vector3.forward, -position.normalized);
                rotation *= Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rotation = Quaternion.FromToRotation(Vector3.forward, position.normalized);
            }
            

            // Move the loaded scene to the specified position
            if (loadedScene.IsValid())
            {
                GameObject[] rootObjects = loadedScene.GetRootGameObjects();
                foreach (GameObject obj in rootObjects)
                {
                    Vector3 fromDirection = obj.transform.position;

                    float initialFromDirectionY = fromDirection.y;
                    // Ignore the y-axis in the rotation
                    fromDirection.y = 0;

                    // Calculate the rotation around the y-axis

                    // Apply the rotation to the vector
                    Vector3 rotatedVector = rotation * fromDirection;
                    rotatedVector += position;
                    rotatedVector.y = initialFromDirectionY;

                    obj.transform.position = rotatedVector;
                    obj.transform.rotation = rotation;

                }

                // Instantiate text prefab above the sublevel
                CreateTextAboveSublevel(position*1.5f, rotation, gymName);
            }
        }
    }

    void CreateTextAboveSublevel(Vector3 position, Quaternion rotation, string text)
    {
        // Instantiate text prefab
        TextMesh textObject = Instantiate(textPrefab, position + Vector3.up * 5f, rotation);

        // Set the text
        textObject.text = text;
    }
}
