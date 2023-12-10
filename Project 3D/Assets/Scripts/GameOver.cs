using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //AudioManager.instance
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        DataHolder.instance.paused = false;
        DataHolder.instance.updateScene();
    }
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
