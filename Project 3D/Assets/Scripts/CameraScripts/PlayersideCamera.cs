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
    [SerializeField] GameObject[] enemies;

    private void Start()
    {
        films = GameObject.FindGameObjectsWithTag("film");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
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
            AudioManager.instance.PlaySFX(AudioManager.instance.cameraClick);
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
            foreach(GameObject obj in enemies)
            {
                Debug.Log(obj.transform.position);
                if (tryPicture(obj.transform.position))
                {
                    hitEnemy(obj);
                }
            }
            AudioManager.instance.PlaySFX(AudioManager.instance.cameraFlash);
        }
    }

    public void PictureChange(Texture2D image)
    {
        //anim for this
        window.texture = image;

    }
    public void PictureStartup(Texture2D image)
    {
        //etc

        //then change
        PictureChange(image);
    }
    bool tryPicture(Vector3 pos)
    {
        Vector3 point = camcam.GetComponent<Camera>().WorldToViewportPoint(pos);
        if (point.x < 1 && point.y < 1 && point.x > 0 && point.y > 0 && point.z > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(camcam.transform.position, pos - camcam.transform.position, out hit, 500))
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.transform.gameObject.tag != "film" && hit.transform.gameObject.tag != "enemy")
                {
                    return false;
                }
                else return true;
            }
            else { return false; }
        }
        else return false;
    }

    void hitEnemy(GameObject enemy)
    {
        Debug.Log("I hit an enemy, " + enemy.name);
        Destroy(enemy);
    }
}
