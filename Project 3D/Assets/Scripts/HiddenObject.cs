using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] private GameObject hidden;
    [SerializeField] private GameObject unhidden;
    int hiddenLayer;

    private void Start()
    {
        hiddenLayer = LayerMask.NameToLayer("hidden");
        hidden.layer = hiddenLayer;
        unhidden.layer = 0;

    }
    public void Change()
    {

        Debug.Log("change");
        hidden.layer = 0;
        unhidden.layer = hiddenLayer;
    }
    public void Unchange()
    {
        Debug.Log("unchange");
        hidden.layer = hiddenLayer;
        unhidden.layer = 0;
    }
}
