using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    AudioManager audioManager;
    
    public void RetryButton()
    {
        SceneManager.LoadScene("Level1");
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
