using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            instance = null;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int lastSceneIndex;
    public int sceneIndex;
    public bool paused = false;
    public void updateScene()
    {
        lastSceneIndex = sceneIndex;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
