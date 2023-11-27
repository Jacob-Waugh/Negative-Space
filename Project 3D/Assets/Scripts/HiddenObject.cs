using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] private GameObject hidden;
    [SerializeField] private GameObject unhidden;
    [SerializeField] private GameObject polty;
    private bool poltergeist;
    private bool poltergeisted;
    int hiddenLayer;

    private void Start()
    {
        hiddenLayer = LayerMask.NameToLayer("hidden");
        unhidden.layer = hiddenLayer;
        hidden.layer = 0;
        poltergeist = false;
        poltergeisted = false;

    }
    public void Change()
    {
        poltergeist=true;
        unhidden.layer = 0;
        hidden.layer = hiddenLayer;
    }
    public void Unchange()
    {
        unhidden.layer = hiddenLayer;
        hidden.layer = 0;
        if (poltergeist && !poltergeisted)
        {
            if(polty == null)
            {
                polty = PlayersideCamera.instance.poltergeist;
            }
            poltergeisted = true;
            Instantiate(polty, GetComponent<Collider>().bounds.center, Quaternion.identity);
        }
    }
}
