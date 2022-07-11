using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public List<string> additionalScenesToLoad = new List<string>();

    private string nextActiveScene;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadAdditiveScenes(string nextActiveScene)
    {
        this.nextActiveScene = nextActiveScene;
        SceneManager.LoadSceneAsync(nextActiveScene, LoadSceneMode.Additive);
        foreach (string scene in additionalScenesToLoad)
        {
            if (scene != nextActiveScene)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
    }

    public void LoadNonAdditiveScenes(string sceneToLoad)
    {
        this.nextActiveScene = sceneToLoad;
        SceneManager.LoadSceneAsync(nextActiveScene, LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == nextActiveScene)
        {
            SceneManager.SetActiveScene(scene);
        }
    }
}
