using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Transform pivot;
    GameObject pendulum;
    int amp;
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.Find("pivot");
        pendulum = transform.Find("pendulum").gameObject;
    }
    private void Update()
    {
        if (!DataHolder.instance.paused)
        {
            if (pendulum.transform.localRotation.eulerAngles.x >= amp)
            {
                pendulum.transform.RotateAround(pivot.position, Vector3.right, 20 * Time.deltaTime);
            }
            if (pendulum.transform.localRotation.eulerAngles.x <= -amp)
            {
                pendulum.transform.RotateAround(pivot.position, Vector3.right, -20 * Time.deltaTime);
            }
        }
    }

}
