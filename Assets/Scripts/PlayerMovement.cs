using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Class Variables
    [SerializeField] private float playerSpeed = 150;
    [SerializeField] private float jumpSpeed = 200;
    /*[SerializeField]*/ private float projectileSpeed = 6f;
    [SerializeField] private Projectile projectile;

    // Flags
    bool slideOnRight;
    bool isDashing;

    // Dash control
    int dashesLeft;
    int maxDashes;

    // Components
    LayerMask groundLayer;
    Rigidbody2D rb;
    PlayerData playerData;

    // Called when initialized
    void Start()
    {
        slideOnRight = true; // doesn't matter as it is set in IsWallSliding() method.
        isDashing = false;
        maxDashes = 1;
        rb = GetComponent<Rigidbody2D>();
        playerData = GetComponent<PlayerData>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Called every fixed frame-rate frame. For physics 2D.
    void FixedUpdate()
    {
        if (!playerData.isDead) // The player can only move if they are not dead
        {
            // Horizonal Movement
            // "Horizontal" Axis refers to using 'a' and 'd' to move left and right respectively (or 'leftArrow' and 'rightArrow').
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;


            // If the player is moving on the ground, change velocity directly.
            if (IsGrounded())
            {
                rb.velocity = new Vector2(x, rb.velocity.y);
            }
            // If the player is moving too fast to the right, you can move left.
            else if (rb.velocity.x > Time.deltaTime * playerSpeed && x < 0)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }
            // If the player is moving too fast to the left, you can move right.
            else if (rb.velocity.x < -Time.deltaTime * playerSpeed && x > 0)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }
            // If the player is moving at a normal velocity, then the player can move either left or right.
            else if (rb.velocity.x <= Time.deltaTime * playerSpeed && rb.velocity.x >= -Time.deltaTime * playerSpeed)
            {
                rb.AddForce(new Vector2(x * 4, 0));
            }
        }
    }

    void Update()
    {
        if (!playerData.isDead) // The player can only act if they are not dead
        {
            // Wall Slide
            if (IsWallSliding())
            {
                if (rb.velocity.y > 0)
                {
                    rb.gravityScale = 1.0f;
                }
                else
                {
                    rb.gravityScale = 0.1f;
                }

                // Check for wall jump
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (slideOnRight)
                    {
                        rb.velocity = new Vector2(-jumpSpeed / 40, jumpSpeed / 50);
                    }
                    else
                    {
                        rb.velocity = new Vector2(jumpSpeed / 40, jumpSpeed / 50);
                    }
                    playerData.audioSource.PlayOneShot(playerData.jumpSFX);
                }
            }
            else
            {
                if (!isDashing)
                {
                    rb.gravityScale = 1.0f;
                }

                // Jump Movement
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
                {
                    rb.AddForce(new Vector2(0, jumpSpeed));
                    playerData.audioSource.PlayOneShot(playerData.jumpSFX);
                }
            }

            // Dash
            if ((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z)) && !isDashing && dashesLeft > 0)
            {
                StartCoroutine(Dash());
                playerData.audioSource.PlayOneShot(playerData.dashSFX);
            }

            // Fires projectile
            if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) && !isDashing)
            {
                FireProjectile();
                playerData.audioSource.PlayOneShot(playerData.shootSFX);
            }
        }
    }

    // Fires projectile
    void FireProjectile()
    {
        Vector3 direction;
        float x;
        float y;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // In case there's no current input, do nothing
        if (x == 0 && y ==0)
        {
            return;
        }

        direction = new Vector3(x, y, 0).normalized;

        // Instantiates Projectile object
        Projectile projectileInstance;
        projectileInstance = Instantiate(projectile, transform.position, transform.rotation);

        // Can this be within the Projectile class?
        projectileInstance.GetComponent<Rigidbody2D>().velocity = (direction * projectileSpeed);
    }

    // Method used to check if the player is on the ground.
    bool IsGrounded()
    {
        Collider2D[] collisions = new Collider2D[2];
        // Checks for collisions made under the player
        // The "GroundCheckSprite" child object is a collider with the same position and size to check for visual purposes.
        int numOfCollisions = Physics2D.OverlapBoxNonAlloc(new Vector2(transform.position.x, transform.position.y - 0.1f), new Vector2(0.1f, 0.05f), 0.0f, collisions, groundLayer); // Change Position and Size
        for(int i = 0; i < numOfCollisions; i++)
        {
            if(!ReferenceEquals(gameObject, collisions[i].gameObject))
            {
                dashesLeft = maxDashes;
                return true;
            }
        }
        return false;
    }

    // Method used to check if the player is wall sliding.
    bool IsWallSliding()
    {
        if (!IsGrounded())
        {
            // Have to check both left and right sides
            Collider2D[] leftCollisions = new Collider2D[2];
            Collider2D[] rightCollisions = new Collider2D[2];
            int numOfLeftCollisions = Physics2D.OverlapBoxNonAlloc(new Vector2(transform.position.x - 0.1f, transform.position.y), new Vector2(0.05f, 0.1f), 0.0f, leftCollisions, groundLayer);
            int numOfRightCollisions = Physics2D.OverlapBoxNonAlloc(new Vector2(transform.position.x + 0.1f, transform.position.y), new Vector2(0.05f, 0.1f), 0.0f, rightCollisions, groundLayer);

            // Check if sliding on left wall
            for (int i = 0; i < numOfLeftCollisions; i++)
            {
                if (!ReferenceEquals(gameObject, leftCollisions[i].gameObject))
                {
                    slideOnRight = false;
                    return true;
                }
            }

            // Check if sliding on right wall
            for (int i = 0; i < numOfRightCollisions; i++)
            {
                if (!ReferenceEquals(gameObject, rightCollisions[i].gameObject))
                {
                    slideOnRight = true;
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator Dash()
    {
        int dirx = 0;
        int diry = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { dirx = -1; }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { dirx = 1; }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { diry = -1; }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { diry = 1; }

        isDashing = true;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dirx, diry).normalized * jumpSpeed / 30;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(rb.velocity.x / 10, rb.velocity.y / 10);
        rb.gravityScale = 1f;
        isDashing = false;
        dashesLeft--;
    }
}
