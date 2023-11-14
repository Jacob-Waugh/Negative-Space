using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundMusic;
    public AudioClip mainMenuMusic;
    public AudioClip death;
    public AudioClip footsteps;
    public AudioClip monster;
    public AudioClip clock;
    public AudioClip cameraClick;
    public AudioClip cameraFlash;
    public AudioClip transition;
    public AudioClip buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        // scene = SceneManager.GetActiveScene();
       // Debug.Log("Active Scene is " + scene.name+".");
       //Scene currentScene = SceneManager.GetActiveScene ();
       //SceneManager.GetActiveScene().name;
       //string sceneName = currentScene.name ();
       //if (sceneName == "MainMenu")
      // {
       // musicSource.clip=mainMenuMusic;
       // musicSource.Play(); 
      // }
      // else if (sceneName== "Level1")
      // {
       // musicSource.clip=backgroundMusic;
       // musicSource.Play();
       //}
    
       musicSource.clip=mainMenuMusic;
       musicSource.Play(); 
    }
    //public void LevelMusic()
   // {
     //   string currentScene = SceneManager.GetActiveScene().name;
      //  switch (currentScene)
      //  {
       //     case "MainMenu";
        //    musicSource.clip=mainMenuMusic;
        //    musicSource.Play();
        //    break;

        //    case "Level1";
         //   musicSource.clip=backgroundMusic;
         //   musicSource.Play();
         //   break;
       // }
   // }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
