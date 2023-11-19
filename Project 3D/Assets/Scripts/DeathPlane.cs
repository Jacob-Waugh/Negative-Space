using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    public GameObject GameOverScreen;
    AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(currentSceneName);
        audioManager.PlaySFX(audioManager.death);
        GameOverScreen.SetActive(true);
    }
}
