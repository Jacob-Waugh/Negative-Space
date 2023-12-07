using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoltergeistScript : MonoBehaviour
{
    GameObject player;
    public float speed = 2f;
    public bool active;
    public bool sparkling;
    [SerializeField] GameObject sparklePrefab;
    [SerializeField] GameObject sparkle;
    private void Awake()
    {
        player = GameObject.Find("FirstPersonController");
        active = true;
        PlayersideCamera.instance.enemies.Add(this.gameObject);
        this.gameObject.layer = LayerMask.NameToLayer("polt");
    }
    private void Update()
    {
        if (!DataHolder.instance.paused)
        {
            if (active)
            {
                if (sparkling)
                {
                    Destroy(sparkle);
                    sparkling = false;
                }
                transform.LookAt(player.transform);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
                {
                    PlayersideCamera.instance.enemies.Remove(this.gameObject);
                    Destroy(this.gameObject);
                    PlayersideCamera.instance.Die();
                }
            }
            else
            {
                if (!sparkling)
                {
                    sparkle = Instantiate(sparklePrefab, transform.position, Quaternion.identity);
                    sparkle.layer = 0;
                    sparkling = true;
                }
            }
        }
    }
}
