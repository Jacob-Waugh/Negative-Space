using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayersideCamera : MonoBehaviour
{
    public static PlayersideCamera instance;
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

    [SerializeField] GameObject polaroid;
    [SerializeField] GameObject camcam;
    [SerializeField] CameraCamera cam;
    [SerializeField] RawImage window;
    [SerializeField] GameObject[] films;

    private void Start()
    {
        films = GameObject.FindGameObjectsWithTag("film");
        if (polaroid == null)
        {
            polaroid = GameObject.Find("Joint/PlayerCamera/Polaroid").gameObject;
        }
        if (camcam == null)
        {
            camcam = transform.Find("camcam").gameObject;
        }
        cam = camcam.GetComponent<CameraCamera>();
        window = polaroid.transform.Find("Canvas/Panel/Window").gameObject.GetComponent<RawImage>();
        int hidden = LayerMask.NameToLayer("hidden");
        foreach (GameObject obj in films)
        {
            foreach (Transform child in obj.transform)
            {
                if (child.gameObject.GetComponent<Collider>() != null)
                {
                    child.gameObject.GetComponent<Collider>().isTrigger = true;
                }
                child.gameObject.layer = hidden;
            }
            if (obj.GetComponent<Collider>() != null)
            {
                obj.GetComponent<Collider>().isTrigger = true;
            }
            obj.layer = hidden;
        }
        polaroid.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            polaroid.SetActive(true);
            cam.Take();
            int hidden = LayerMask.NameToLayer("hidden");
            foreach (GameObject obj in films)
            {
                
                if (tryPicture(obj.transform.position))
                {
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        obj.GetComponent<Collider>().isTrigger = false;
                    }
                    obj.layer = 0;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            child.gameObject.GetComponent<Collider>().isTrigger = false;
                        }
                        child.gameObject.layer = 0;
                    }
                }
                else
                {
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        obj.GetComponent<Collider>().isTrigger = true;
                    }
                    obj.layer = hidden;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            child.gameObject.GetComponent<Collider>().isTrigger = true;
                        }
                        child.gameObject.layer = hidden;
                    }
                }
            }

        }
    }

    public void PictureChange(Texture2D image)
    {
        //anim for this
        window.texture = image;
        Debug.Log("trychange");
    }
    public void PictureStartup(Texture2D image)
    {
        //etc
        Debug.Log("trystart");
        //then change
        PictureChange(image);
    }
    bool tryPicture(Vector3 pos)
    {
        Vector3 point = camcam.GetComponent<Camera>().WorldToViewportPoint(pos);
        if (point.x < 1 && point.y < 1 && point.x > 0 && point.y > 0 && point.z > 0)
        {
            Debug.Log(pos);
            Debug.Log(camcam.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(camcam.transform.position, pos - camcam.transform.position, out hit, 500))
            {
                if (hit.transform.gameObject.tag != "film")
                {
                    Debug.Log(hit.transform.gameObject);
                    return false;
                }
                else return true;
            }
            else { return false; }
        }
        else return false;
    }
}
