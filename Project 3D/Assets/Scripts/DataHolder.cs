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
        if (instance != null)
        {
            instance.updateScene();
            Destroy(instance);
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public Input input = new Input();
    public int lastSceneIndex;
    public int sceneIndex;
    public bool paused = false;
    public class Input
    {
        public float? x;
        public float? y;
        public void clear()
        {
            x = 0;
            y = 0;
        }
    } 
    public void updateScene()
    {
        if (paused)
        {
            paused = false;
        }
        lastSceneIndex = instance.sceneIndex;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
