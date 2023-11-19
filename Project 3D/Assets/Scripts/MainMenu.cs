using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called before the first frame update

    private void Awake()
    {
       //audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        
    }

    public void OnClickStart()
    {
       //audioManager.PlaySFX(audioManager.buttonClick);
       //yeild return new WaitForSeconds(3);
        SceneManager.LoadScene("Level1");
    }

     public void OnClickOptions()
    {
        SceneManager.LoadScene("Options");
    }

     public void OnClickHelp()
    {
        SceneManager.LoadScene("Help");
    }

     public void OnClickQuit()
    {
       // Application.Quit;
       Debug.Log("Application Quit");
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
