using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string code = "0000";
    public GameObject key;
    Key keyScript;
    public bool unlocked;
    public GameObject openDoor;
    private void Start()
    {
        code = code.Substring(0, key.transform.childCount);
        keyScript = key.GetComponent<Key>();
        keyScript.InputCode = new int[code.Length];
        foreach (int i in keyScript.InputCode)
        {
            keyScript.InputCode[i] = 0;
        }
    }
    private void Update()
    {
        if (keyScript != null)
        {
            if (keyScript.code == code)
            {
                unlocked = true;
                gameObject.layer = LayerMask.NameToLayer("interact");
                PlayersideCamera.instance.interacts.Add(gameObject);
            }
            else
            {
                unlocked = false;
                gameObject.layer = 0;
            }
        }
    }
     public void Open()
    {
        Debug.Log("open!");
        Destroy(key);
        gameObject.layer = LayerMask.NameToLayer("hidden");
        openDoor.layer = 0;
        PlayersideCamera.instance.Win();
    }
}
