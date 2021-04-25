using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Class variables
    //private float moveSpeed = 2;
    private float timeToLive = 1.5f;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;


    // Start is called before the first frame update
    void Start()
    {
        // Gets this object's rigidbody component
        if (GetComponent<Rigidbody2D>() != null)
            rb = GetComponent<Rigidbody2D>();
        else
            Debug.LogError(this.name + ": RigidBody component is null");

        // Gets this object's box collider component
        if (GetComponent<CircleCollider2D>() != null)
            circleCollider = GetComponent<CircleCollider2D>();
        else
            Debug.LogError(this.name + ": BoxCollider component is null");

        // Queues object for destruction at the end of its TTL
        Destroy(this.gameObject, timeToLive);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 

    }

    void ProjectileMovement()
    {
        //rb.MovePosition(transform.position + Vector3.left * Time.fixedDeltaTime * moveSpeed);

        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        //rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
    }
    
    /*virtual protected void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        //Debug.Log(this.name + "is colliding");

        // Checks if projectile collided with the level environment
        if (collisionInfo.gameObject.tag == "Level" || collisionInfo.gameObject.layer == 8)        // 8 is Ground layer
        {
            Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);

            // Destroys object instantly
            Destroy(this.gameObject, 0.0f);
        }
        /*
        // Checks if projectile collided with player
        if (collisionInfo.gameObject.tag == "Player")
        {
            Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);

            collisionInfo.gameObject.GetComponent<PlayerData>().TakeDamage(10); // Should Projectile have variable damage?
            Destroy(gameObject);
        }
        
    }
    */
    /*
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {

    }
    */
}
