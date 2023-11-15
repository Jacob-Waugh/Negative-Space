using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    //this part is sort of a pseudo-singleton, which lets us call it from wherever using AudioManager.instance
    public static AudioManager instance;
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
        }
    }

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
       LevelMusic();
   }
   public void LevelMusic()
   {
       string currentScene = SceneManager.GetActiveScene().name;
       switch (currentScene)
       {
           case "MainMenu":
            musicSource.clip=mainMenuMusic;
            musicSource.Play();
           break;

           case "Level1":
            musicSource.clip=backgroundMusic;
            musicSource.Play();
           break;
       }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
