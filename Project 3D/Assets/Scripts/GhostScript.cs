using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostScript : MonoBehaviour
{
    GameObject player;
    public float speed = 2f;
    
    private void Awake()
    {
        player = GameObject.Find("FirstPersonController");
        Debug.Log("ghost was born");
        
    }
    private void Update()
    {
        if (!DataHolder.instance.paused)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) < 0.7f)
            {
                PlayersideCamera.instance.enemies.Remove(gameObject);
                Destroy(gameObject);
                PlayersideCamera.instance.Die();
            }
        }
    }
    public void Die()
    {
        if (PlayersideCamera.instance.poofParticles)
        {
            GameObject explode = Instantiate(PlayersideCamera.instance.poofParticles, transform.position, transform.rotation).gameObject;
            Destroy(explode, 2.0f);
        }
        PlayersideCamera.instance.SpawnGhost();
        Destroy(this.gameObject);
    }
}
