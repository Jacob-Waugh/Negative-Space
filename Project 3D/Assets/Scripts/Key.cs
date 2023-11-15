using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string code;
    public int[] InputCode;
    List<string> StringifyCode = new List<string>();
    public int maxValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        
        Stringify();
        code = string.Join("", StringifyCode);
        Debug.Log(InputCode);
        Debug.Log(code);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Alpha1)) { Turn(transform.GetChild(0)); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { Turn(transform.GetChild(1)); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { Turn(transform.GetChild(2)); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { Turn(transform.GetChild(3)); }
        */
        
    }
    void Stringify()
    {
        StringifyCode.Clear();
        foreach (int i in InputCode)
        {
            StringifyCode.Add(i.ToString());
        }
    }
    void ValueUpdate(Transform dial)
    {
        InputCode[dial.transform.GetSiblingIndex()] += 1;
        InputCode[dial.transform.GetSiblingIndex()] %= (maxValue+1);
        Stringify();
        code = string.Join("", StringifyCode);
        Debug.Log(code);
    }
    public void Turn(Transform dial)
    {
        dial.Rotate(0, 0, 90f, Space.Self);
        ValueUpdate(dial);
    }
}
