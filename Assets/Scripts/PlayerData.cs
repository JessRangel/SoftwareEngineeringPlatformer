using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;

    [SerializeField] GameObject[] spawn;

    static int checkPoint = 0;
    
    int health;
    int maxHealth = 100;
    bool invul = false;
    bool isPaused = false;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Image healthBar;
    GameObject deathText;
    GameObject pauseScreen;

    [HideInInspector] public AudioSource audioSource;
    public AudioClip buttonSFX;
    public AudioClip checkpointSFX;
    public AudioClip damageSFX;
    public AudioClip dashSFX;
    public AudioClip jumpSFX;
    public AudioClip shootSFX;
    public AudioClip bruhSFX;

    void Start()
    {
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        healthBar = GameObject.Find("Canvas").transform.Find("HealthBar").transform.Find("Fill").GetComponent<Image>();
        deathText = GameObject.Find("Canvas").transform.Find("YouDiedText").gameObject;
        pauseScreen = GameObject.Find("Canvas").transform.Find("PauseScreen").gameObject;
        deathText.SetActive(false);
        pauseScreen.SetActive(false);

        // Move the player to their spawn point
        Spawn();
    }

    void Update()
    {
        if (!isDead) // Actions when the player is alive
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Pause key
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if(transform.position.y < -20f)
            {
                TakeDamage(500);
            }
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void TakeDamage(int _damage)
    {
        // The player takes damage.
        if (!invul && health > 0)
        {
            health -= _damage;
            audioSource.PlayOneShot(damageSFX);
            Debug.Log("Health: " + health);
            rb.velocity = new Vector2(0, 5);
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else // Only flash if the player survives damage
            {
                StartCoroutine(Flash(Color.red));
            }
            UpdateUI();
        }
    }

    void Heal(int _healthAmount)
    {
        // Heals the player
        if (!invul)
        {
            health += _healthAmount;
            StartCoroutine(Flash(Color.green));
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            UpdateUI();
        }
    }

    IEnumerator Die()
    {
        // When the player dies
        if(Random.Range(0, 100) == 0)
        {
            audioSource.PlayOneShot(bruhSFX);
        }
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        sr.enabled = false;
        isDead = true;
        deathText.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Spawn()
    {
        transform.position = spawn[checkPoint].transform.position;
        isDead = false;
    }

    IEnumerator Flash(Color _color)
    {
        // When the player takes damagge or heals, the player flashes to visually indicate that.
        Color currentColor = sr.color;
        invul = true;
        for(int i = 0; i < 10; i++)
        {
            sr.color = _color;
            yield return new WaitForSeconds(0.05f);
            sr.color = currentColor;
            yield return new WaitForSeconds(0.05f);
        }
        invul = false;
    }

    // IMPORTANT
    // First checkpoint is always invisible as to not confuse the player as it is the initial spawn
    void GetCheckpoint(int num)
    {
        if(checkPoint != num)
        {
            checkPoint = num;
            audioSource.PlayOneShot(checkpointSFX);
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        audioSource.PlayOneShot(buttonSFX);
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        audioSource.PlayOneShot(buttonSFX);
    }

    public void PlayEnemyDeathSound(AudioClip enemyDeathSFX)
    {
        audioSource.PlayOneShot(enemyDeathSFX);
    }

    void UpdateUI()
    {
        // This is called every time the UI needs to update after values change.
        healthBar.fillAmount = (float)health / maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Damages the player if they touch spikes
        if(collision.gameObject.tag == "Spike")
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            // Set checkpoint
            GetCheckpoint(collision.gameObject.GetComponent<Checkpoint>().checkpointVal);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "End" && (Input.GetKey(KeyCode.C) || Input.GetKeyDown(KeyCode.L)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
