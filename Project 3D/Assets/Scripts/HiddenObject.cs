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
        unhidden.layer = hiddenLayer;
        hidden.layer = 0;

    }
    public void Change()
    {
        unhidden.layer = 0;
        hidden.layer = hiddenLayer;
    }
    public void Unchange()
    {
        unhidden.layer = hiddenLayer;
        hidden.layer = 0;
    }
}
