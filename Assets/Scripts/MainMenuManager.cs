using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameObject MenuCamera;

    public void LoadMainGame()
    {
        Destroy(MenuCamera);
        sceneLoader.LoadNonAdditiveScenes("TopDownCity");
    }
}
