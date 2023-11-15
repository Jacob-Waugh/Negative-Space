using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public string code = "0000";
    public GameObject key;
    Key keyScript;
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
                //open sesame
                Debug.Log("and open!");
            }
        }
    }
}
