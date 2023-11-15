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
    private void Start()
    {
        player = GameObject.Find("FirstPersonController");
    }
    private void Update()
    {
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, player.transform.position) < 0.001f)
        {
            PlayersideCamera.instance.enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
