using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    // The killzone serves to instantly kill the player once he goes out of bounds.
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        // Checks if killzone collided with player
        if (collisionInfo.gameObject.tag == "Player")
        {
            Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);

            // Killzone applies enough damage to instakill the player.
            collisionInfo.gameObject.GetComponent<PlayerData>().TakeDamage(500);
        }
    }
}
