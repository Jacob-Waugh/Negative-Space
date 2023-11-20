using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    [SerializeField] List<GameObject> films;
    public List<GameObject> enemies;
    [SerializeField] List<GameObject> clues;
    public GameObject GameOverScreen;

    private void Start()
    {
        flash = GameObject.Find("Flash");
        animator = flash.GetComponent<Animator>();
        films = GameObject.FindGameObjectsWithTag("film").ToList();
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        clues = GameObject.FindGameObjectsWithTag("clue").ToList();
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
        int player = LayerMask.NameToLayer("player");
        foreach (GameObject obj in films)
        {
            foreach (Transform child in obj.transform)
            {
                if (child.gameObject.GetComponent<Collider>() != null)
                {
                    child.gameObject.GetComponent<Collider>().excludeLayers = player;
                }
                child.gameObject.layer = hidden;
            }
            if (obj.GetComponent<Collider>() != null)
            {
                obj.gameObject.GetComponent<Collider>().excludeLayers = player;
            }
            obj.layer = hidden;
        }
        polaroid.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.Play("Flash", -1, 0f);
            AudioManager.instance.PlaySFX(AudioManager.instance.cameraClick);
            polaroid.SetActive(true);
            cam.Take();
            int hidden = LayerMask.NameToLayer("hidden");
            LayerMask player = LayerMask.GetMask("player");
            Debug.Log(player);
            bool flash = false;
            foreach (GameObject obj in films)
            {

                if (tryPicture(obj.transform.position))
                {
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        //Physics.IgnoreCollision(obj.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), false);
                        Debug.Log("Ignore false");
                        obj.gameObject.GetComponent<Collider>().excludeLayers = 0;
                    }
                    obj.layer = 0;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            //Physics.IgnoreCollision(child.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), false);
                            Debug.Log("Ignore child false");
                            child.gameObject.GetComponent<Collider>().excludeLayers = 0;
                        }
                        child.gameObject.layer = 0;
                    }
                    flash = true;
                }
                else
                {
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        //Physics.IgnoreCollision(obj.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), true);
                        obj.gameObject.GetComponent<Collider>().excludeLayers = player;
                    }
                    obj.layer = hidden;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            //Physics.IgnoreCollision(child.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), true);
                            child.gameObject.GetComponent<Collider>().excludeLayers = player;
                        }
                        child.gameObject.layer = hidden;
                    }
                }
            }
            foreach (GameObject obj in enemies)
            {
                if (tryPicture(obj.transform.position))
                {
                    hitEnemy(obj);
                    flash = true;
                }
            }
            foreach (GameObject obj in clues)
            {
                HiddenObject hideScript = obj.GetComponent<HiddenObject>();
                if (tryPicture(obj.GetComponent<Collider>().bounds.center))
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
        //said I was gonna leave this for camera stuff, that was a LIE I do not want to touch playercontroller

        RaycastHit hit;
        if (Physics.Raycast(transform.Find("Joint/PlayerCamera").transform.position, transform.Find("Joint/PlayerCamera").TransformDirection(Vector3.forward), out hit, 5f))
        {
            GameObject go = hit.transform.gameObject;
            int interactLayer = LayerMask.NameToLayer("interact");
            if (go.layer == interactLayer)
            {
                GameObject gop = hit.transform.root.gameObject;

                //highlight

                //interact
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gop.tag == "key")
                    {
                        Key keyScript = gop.GetComponent<Key>();
                        keyScript.Turn(go.transform);
                    }
                    if (gop.tag == "lock")
                    {
                        Lock lockScript = gop.GetComponent<Lock>();
                        if (lockScript.unlocked == true)
                        {
                            lockScript.Open();
                        }
                    }
                }

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
            RaycastHit hit;
            if (Physics.Raycast(camcam.transform.position, pos - camcam.transform.position, out hit, 500f))
            {
                Debug.Log(hit.transform.gameObject);
                Debug.DrawRay(camcam.transform.position, pos - camcam.transform.position, UnityEngine.Color.yellow, 1);
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
        if (enemy.layer == LayerMask.NameToLayer("ghosts"))
        {
            enemies.Remove(enemy);
            Destroy(enemy);
        }
    }
    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.death);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }
}