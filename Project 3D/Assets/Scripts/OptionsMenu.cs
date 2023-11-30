using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
 
 public AudioMixer audioMixer;
 public Dropdown resolutionDropdown;
 Resolution[] resolutions;

 void start()
{
    resolutions= Screen.resolutions;
    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

    int currentResolutionIdenx=0;

    for (int i = 0; i< resolutions.Length; i++)
    {
        string option = resolutions[i].width + "x" + resolutions[i].height;
        options.Add(option);

        if(resolutions[i].width== Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        {
            currentResolutionIdenx=i;
        }
    }

    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIdenx;
    resolutionDropdown.RefreshShownValue();
}

 public void SetResolution (int resolutionIndex)
 {
    Resolution resolution= resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
 }
 public void SetVolume (float volume)
 {
    audioMixer.SetFloat("volume", volume);
    Debug.Log(volume);
 }

 public void SetQuality (int qualityIndex)
 {
    QualitySettings.SetQualityLevel(qualityIndex);
 }

 public void SetFullscreen (bool isFullscreen)
 {
    Screen.fullScreen=isFullscreen;
 }

 public void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
