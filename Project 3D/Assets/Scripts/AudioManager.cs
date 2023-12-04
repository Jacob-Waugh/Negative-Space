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
    [SerializeField] AudioSource clockSource;

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
    public AudioClip footstep;

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
       // if(Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))
        //{
        //    SFXSource.clip=footsteps;
        //    SFXSource.Play();
        //}
        
        

        
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void ClockPlay()
    {
        clockSource.Play();
    }
    public void ClockStop()
    {
        clockSource.Stop();
    }
}
