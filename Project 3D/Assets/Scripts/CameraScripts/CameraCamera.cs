using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCamera : MonoBehaviour
{
    [SerializeField] Camera camcam;
    [SerializeField] RenderTexture CameraRT;
    Texture2D LastImage;
    bool startupped;
    private void Start()
    {
        LastImage = new Texture2D(CameraRT.width, CameraRT.height, CameraRT.graphicsFormat, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
        startupped = false;
    }
    private void Update()
    {

    }
    public void Take()
    {
        RenderTexture.active = CameraRT;
        LastImage.ReadPixels(new Rect(0, 0, CameraRT.width, CameraRT.height), 0, 0);
        LastImage.Apply();
        RenderTexture.active = null;
        if (startupped)
        {
            Debug.Log((PlayersideCamera.instance));
            PlayersideCamera.instance.PictureChange(LastImage);
        }
        else
        {
            PlayersideCamera.instance.PictureStartup(LastImage);
            startupped = true;
        }
    }
}
