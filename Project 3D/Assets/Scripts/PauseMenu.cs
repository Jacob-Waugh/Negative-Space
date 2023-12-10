using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //AudioManager.instance
    public void RetryThis()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(DataHolder.instance.lastSceneIndex);
    }
    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
    public void ReturnToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
