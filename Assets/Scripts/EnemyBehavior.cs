using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
                     private GameObject target;             //this object's target to follow
    [SerializeField] private Projectile projectile;         // the projectile object to spawn when shooting
    [SerializeField] private float moveSpeed = 2;           // move speed
    [SerializeField] private float followDistance = 3;      // range of follow
    [SerializeField] private bool isShooter = true;        // defines if this enemy shoots projectiles 
                     private bool isFollowing = false;      // holds if it is currently chasing a target
                     private Rigidbody2D rb;                // component
                     private BoxCollider2D boxCollider;     // component
                     private float projectileSpeed = 6f;

    AudioSource audioSource;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;

    void Start()
    {
        // Gets this object's rigidbody component
        if (GetComponent<Rigidbody2D>() != null)
            rb = GetComponent<Rigidbody2D>();
        else
            Debug.LogError(this.name + ": RigidBody component is null");

        // Gets this object's box collider component
        if (GetComponent<BoxCollider2D>() != null)
            boxCollider = GetComponent<BoxCollider2D>();
        else
            Debug.LogError(this.name + ": BoxCollider component is null");

        // Target consistency
        if (target == null) 
            // Debug.LogError(this.name + ": Target object is null");

        // changes behavior if defined as a shooter
        if (isShooter)
            InvokeRepeating(nameof(FireProjectile), 1.0f, 0.5f); // InvokeRepeating(method name, start time, interval time)

        // Find the player object the enemy will target
        target = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Called every fixed frame-rate frame. Best for physics and rigidbodies.
    void FixedUpdate()
    {
        FollowTarget();
    }

    // Follows a specified target.
    void FollowTarget()
    {
        // Calculates distance between this object and target
        float distance = Vector3.Distance(target.transform.position, transform.position);

        // calculates the direction to move towards
        Vector3 direction = target.transform.position - transform.position;

        // normalizes vector
        direction.Normalize();

        // moves towards target is within chase distance
        if (distance <= followDistance && !target.GetComponent<PlayerData>().isDead)
        {
            isFollowing = true;
            // MovePosition checks for collisions
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * moveSpeed);
        }
        else
            isFollowing = false;
    }

    // If this enemy is a shooter, spawns a projectile object.
    void FireProjectile()
    {
        if (isFollowing == true)
        {
            //Debug.Log("Fire Projectile");
            
            // calculates the direction to fire towards
            Vector3 direction = target.transform.position - transform.position;
            audioSource.PlayOneShot(shootSFX);

            // normalizes vector
            direction.Normalize();

            // Instantiates Projectile object
            Projectile projectileInstance;
            projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.GetComponent<Rigidbody2D>().velocity = (direction * projectileSpeed);
            //projectileInstance.GetComponent<Rigidbody2D>().AddForce(direction * 100);
            //Projectile bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            //rb.velocity.normalized
            //bullet.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * projectileSpeed);
        }
        else
            return;
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(deathSFX);
    }

    // Handles collison behavior.
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        //Debug.Log(this.name + "is colliding");

       if (collisionInfo.gameObject.tag == "Player")
        {
            //Debug.Log(this.name + " is colliding with " + collisionInfo.gameObject.name);
        }
    }
}
