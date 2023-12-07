using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveDuplicateObjects : MonoBehaviour
{
    // List to store the names of the spawned game objects
    private List<string> spawnedObjectNames = new List<string>();

    // List of names to compare against
    public List<string> namesToRemove;

    void Start()
    {
        // Subscribe to the sceneLoaded event to detect when a new sublevel is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Call the function to remove duplicates in the current sublevel
        RemoveDuplicates(namesToRemove);
    }

    // This function is called whenever a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Clear the list of spawned object names when a new scene is loaded
        spawnedObjectNames.Clear();

        // Call the function to remove duplicates in the new sublevel
        RemoveDuplicates(namesToRemove);
    }

    // Function to remove duplicate top-level game objects based on their names, only if the name is in the specified list
    void RemoveDuplicates(List<string> namesToRemove)
    {
        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // Find all game objects in the scene hierarchy
            GameObject[] allObjects = scene.GetRootGameObjects();

            foreach (GameObject obj in allObjects)
            {
                // Check if the object has a name
                if (obj.name != null)
                {
                    // Check if the object's name is in the specified list
                    if (namesToRemove.Contains(obj.name))
                    {
                        // Check if the object's name is already in the list
                        if (spawnedObjectNames.Contains(obj.name))
                        {
                            // Delay the destruction until the end of the frame to avoid issues with component enable/disable during the same frame
                            DestroyImmediate(obj, false);
                        }
                        else
                        {
                            // Add the object's name to the list
                            spawnedObjectNames.Add(obj.name);
                        }
                    }
                }
            }
        }
    }
}
