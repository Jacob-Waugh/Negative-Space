using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayersideCamera : MonoBehaviour
{
    public GameObject flash;
    Animator animator;

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
    [SerializeField] GameObject[] clues;

    private void Start()
    {
        animator = flash.GetComponent<Animator>();
        films = GameObject.FindGameObjectsWithTag("film");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        clues = GameObject.FindGameObjectsWithTag("clue");
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
            animator.ResetTrigger("DoFlash");
            animator.SetTrigger("DoFlash");
            AudioManager.instance.PlaySFX(AudioManager.instance.cameraClick);
            polaroid.SetActive(true);
            cam.Take();
            int hidden = LayerMask.NameToLayer("hidden");
            bool flash = false;
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
                    flash = true;
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
                if (tryPicture(obj.transform.position))
                {
                    Debug.Log(obj.transform.position);
                    hitEnemy(obj);
                    flash = true;
                }
            }
            foreach(GameObject obj in clues)
            {
                HiddenObject hideScript = obj.GetComponent<HiddenObject>();
                if (tryPicture(new Vector3(obj.transform.position.x*1.1f, obj.transform.position.y*1.1f, obj.transform.position.z*1.1f)))
                {
                    if (hideScript != null)
                    {
                        hideScript.Change();
                    }
                    flash = true;
                }
                else
                {
                    if (hideScript != null)
                    {
                        hideScript.Unchange();
                    }
                }
            }
            if (flash)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.cameraFlash);
            }
            else
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.cameraClick);
            }
            
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
            Debug.Log("Somethings wong, I can feel it");
            RaycastHit hit;
            if (Physics.Raycast(camcam.transform.position, pos - camcam.transform.position, out hit, 500))
            {
                Debug.Log(hit.transform.gameObject);
                Debug.DrawRay(camcam.transform.position, pos - camcam.transform.position, UnityEngine.Color.yellow, 30);
                if (hit.transform.gameObject.tag != "film" && hit.transform.gameObject.tag != "enemy" && hit.transform.gameObject.tag != "clue")
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
