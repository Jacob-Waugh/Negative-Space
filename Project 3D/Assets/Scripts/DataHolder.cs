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
        instance = this;
    }
    public Input input;
    public int lastSceneIndex;
    public int sceneIndex;
    public bool paused = false;
    public class Input
    {
        public float? x;
        public float? y;
        public bool empty
        {
            get
            {
                if(y == null && x == null)
                {
                    return true;
                }
                return false;
            }
            set
            {
                if(value == true)
                {
                    x = null;
                    y = null;
                }
            }
        }
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
        lastSceneIndex = sceneIndex;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
