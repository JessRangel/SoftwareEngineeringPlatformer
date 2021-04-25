using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        //Debug.Log(this.name + "is colliding");

        // Checks if projectile collided with the level environment
        if (collisionInfo.gameObject.layer == 8)        // 8 is Ground layer
        {
            //Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);

            // Destroys object instantly
            Destroy(this.gameObject, 0.0f);
        }
        
        // Checks if projectile collided with player
        if (collisionInfo.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);

            collisionInfo.gameObject.GetComponent<PlayerData>().TakeDamage(10); // Should Projectile have variable damage?
            // Debug.Log("Current player health: " + collisionInfo.gameObject.GetComponent<PlayerData>().getHealth());
            
            // Self destruct on contact
            Destroy(gameObject);
        }
        
    }
}
