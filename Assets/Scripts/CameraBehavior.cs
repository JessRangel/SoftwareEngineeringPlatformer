using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the camera behavior to follow player.
public class CameraBehavior : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        transform.position = player.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void Update()
    {
        Vector3 playerDir = player.transform.position - transform.position;
        playerDir = new Vector3(playerDir.x, playerDir.y, -10);
        transform.position += new Vector3(playerDir.x * Time.deltaTime * 2, playerDir.y * Time.deltaTime * 2, 0);
    }
}
