using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        DataHolder.instance.paused = false;
        DataHolder.instance.updateScene();
    }

    public void ReturnToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
       //Application.Quit;
       Debug.Log("Application Quit");
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
