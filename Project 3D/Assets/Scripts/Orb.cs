using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Vector3 pointloc;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 target;
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    public float speed = 1;

    // Start is called before the first frame update
    private void Start()
    {
        pointloc = point.position;
        start = transform.position;
        CheckTarget(pointloc);
    }
    private void Update()
    {
        if (!DataHolder.instance.paused)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                if (target != start)
                {
                    CheckTarget(start);
                }
                else
                {
                    CheckTarget(pointloc);
                }
            }
        }
    }
    private void CheckTarget(Vector3 check)
    {
        if (x)
        {
            target.x = check.x;
        }
        else
        {
            target.x = transform.position.x;
        }
        if (y)
        {
            target.y = check.y;
        }
        else
        {
            target.y = transform.position.y;
        }
        if (z)
        {
            target.z = check.z;
        }
        else
        {
            target.z = transform.position.z;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayersideCamera.instance.Die();
        }
    }
}
