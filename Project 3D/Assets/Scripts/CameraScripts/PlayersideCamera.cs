using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Autodesk.Fbx;
using Unity.VisualScripting;

public class PlayersideCamera : MonoBehaviour
{
    public GameObject flash;
    Animator flashAnimator;

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

    public GameObject[] FindGameObjectsWithLayer(int layer) { 
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[]; 
        var goList = new List<GameObject>(); 
        for (int i = 0; i < goArray.Length; i++) { 
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            } 
        } 
        if (goList.Count == 0) 
        { return null; } 
        return goList.ToArray(); 
    }

    [SerializeField] GameObject polaroid;
    [SerializeField] GameObject camcam;
    public Transform poofParticles;
    public GameObject poltergeist;
    public GameObject ghost;
    [SerializeField] CameraCamera cam;
    [SerializeField] RawImage window;
    [SerializeField] List<GameObject> films;
    public List<GameObject> enemies;
    [SerializeField] List<GameObject> clues;
    public GameObject GameOverScreen;
    public BoxCollider spawnBox;
    public bool dead = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Animator cutsceneHandler;
    [SerializeField] Animator winHandler;
    public List<GameObject> interacts;
    [SerializeField] float outlineWidth = 3f;

    private void Start()
    {
        pauseMenu = GameObject.Find("Canvas/PausePanel");
        pauseMenu.SetActive(false);;
        cutsceneHandler.enabled = false;
        DataHolder.instance.updateScene();
        spawnBox = GameObject.Find("SpawnZone").GetComponent<BoxCollider>();
        interacts = FindGameObjectsWithLayer(LayerMask.NameToLayer("interact")).ToList();
        flash = GameObject.Find("Canvas/Flash");
        flashAnimator = flash.GetComponent<Animator>();
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
        SpawnGhost();
    }
    private void Update()
    {
        if (dead)
        {
            DataHolder.instance.paused = true;
            dead = false;
        }
        if (Input.GetButtonDown("Fire1") && !DataHolder.instance.paused)
        {
            flashAnimator.Play("Flash", -1, 0f);
            AudioManager.instance.PlaySFX(AudioManager.instance.cameraClick);
            polaroid.SetActive(true);
            cam.Take();
            int hidden = LayerMask.NameToLayer("hidden");
            LayerMask player = LayerMask.GetMask("player");
            Debug.Log(player);
            bool flash = false;
            foreach (GameObject obj in films)
            {

                if (tryPicture(obj.GetComponent<Collider>().bounds.center))
                {
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        obj.gameObject.GetComponent<Collider>().excludeLayers = 0;
                    }
                    obj.layer = 0;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
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
                        obj.gameObject.GetComponent<Collider>().excludeLayers = player;
                    }
                    obj.layer = hidden;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            child.gameObject.GetComponent<Collider>().excludeLayers = player;
                        }
                        child.gameObject.layer = hidden;
                    }
                }
            }
            foreach (GameObject obj in enemies)
            {
                if (obj.GetComponent<PoltergeistScript>())
                {
                    Debug.Log("polt active");
                    PoltergeistScript polt = obj.GetComponent<PoltergeistScript>();
                    polt.active = true;
                    int poltLayer = LayerMask.NameToLayer("polt");
                    if (obj.gameObject.GetComponent<Collider>() != null)
                    {
                        obj.gameObject.GetComponent<Collider>().excludeLayers = 0;
                    }
                    obj.layer = poltLayer;
                    foreach (Transform child in obj.transform)
                    {
                        if (child.gameObject.GetComponent<Collider>() != null)
                        {
                            child.gameObject.GetComponent<Collider>().excludeLayers = 0;
                        }
                        child.gameObject.layer = poltLayer;
                    }
                }
                if (tryPicture(obj.GetComponent<Collider>().bounds.center))
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
                foreach (GameObject obj in interacts)
                {
                    if (obj != null)
                    {
                        if (obj.GetComponent<Outline>() != null && obj.GetComponent<Outline>().enabled)
                        {
                            obj.GetComponent<Outline>().enabled = false;
                        }
                    }
                }
                //highlight
                if (go.GetComponent<Outline>() != null)
                {
                    go.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    var outline = go.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineVisible;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = outlineWidth;
                }
                //interact
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gop.tag == "key")
                    {
                        Key keyScript = gop.GetComponent<Key>();
                        keyScript.Turn(go.transform);
                    }
                    if (go.tag == "lock")
                    {
                        Debug.Log("try unlock");
                        Lock lockScript = go.GetComponent<Lock>();
                        if (lockScript.unlocked == true)
                        {
                            lockScript.Open();
                        }
                    }
                }
            }
            else
            {
                if (interacts != null)
                {
                    foreach (GameObject obj in interacts)
                    {
                        if (obj != null)
                        {
                            if (obj.GetComponent<Outline>() != null && obj.GetComponent<Outline>().enabled)
                            {
                                obj.GetComponent<Outline>().enabled = false;
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!DataHolder.instance.paused)
            {
                Cursor.lockState = CursorLockMode.Confined;
                DataHolder.instance.paused = true;
                //pause appear
                pauseMenu.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                DataHolder.instance.paused = false;
                //pause disappear
                pauseMenu.SetActive(false);
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
            enemy.GetComponent<GhostScript>().Die();
        }
        int hidden = LayerMask.NameToLayer("hidden");
        LayerMask player = LayerMask.GetMask("player");
        if (enemy.GetComponent<PoltergeistScript>())
        {
            PoltergeistScript polt = enemy.GetComponent<PoltergeistScript>();
            polt.active = false;
            Debug.Log("polt inactive");
            if (enemy.gameObject.GetComponent<Collider>() != null)
            {
                enemy.gameObject.GetComponent<Collider>().excludeLayers = player;
            }
            enemy.layer = hidden;
            foreach (Transform child in enemy.transform)
            {
                if (child.gameObject.GetComponent<Collider>() != null)
                {
                    child.gameObject.GetComponent<Collider>().excludeLayers = player;
                }
                child.gameObject.layer = hidden;
            }
        }
    }
    public void SpawnGhost()
    {
        Debug.Log("Spawning");
        StartCoroutine(SpawnCoroutine(ghost));
    }
    IEnumerator SpawnCoroutine(GameObject enemy)
    {
        yield return new WaitForSeconds(Random.Range(1,5));
        AudioManager.instance.PlaySFX(AudioManager.instance.clock);
        GameObject newguy;
        if (spawnBox != null)
        {
            Bounds bounds = spawnBox.bounds;
            newguy = Instantiate(ghost, new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)), Quaternion.identity);
        }
        else
        {
            newguy = Instantiate(ghost, Vector3.zero, Quaternion.identity);
        }
        enemies.Add(newguy);
    }
    public void Die()
    {
        StartCoroutine(DieCoroutine());
        dead = true;
    }
    IEnumerator DieCoroutine()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.death);
        cutsceneHandler.enabled = true;
        yield return new WaitWhile(() => cutsceneHandler.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        SceneManager.LoadScene("GameOver");
    }
    public void Win()
    {
        StartCoroutine(WinCoroutine());
        Debug.Log("ding!");
    }
    IEnumerator WinCoroutine()
    {
        GameObject winCutLoc = GameObject.Find("WinLocation");
        transform.position = winCutLoc.transform.position;
        DataHolder.instance.paused = true;
        winHandler.enabled = true;
        yield return new WaitWhile(() => winHandler.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}