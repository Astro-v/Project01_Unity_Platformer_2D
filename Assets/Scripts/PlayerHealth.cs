using UnityEngine;
using System.Collections; // Pour le multithreading

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimmeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.2f;

    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public AudioClip hitSound;

    // Cr��r un unique PlayerHealth acc�ssible de partout
    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // v�rifier si le joueur est toujours vivant
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        // bloquer les movements du personage
        PlayerMovement.instance.enabled = false;

        // jouer l'annimation d'�limination
        PlayerMovement.instance.animator.SetTrigger("Die");

        // emp�cher les int�raction physique avec les autres �l�ments de la sc�ne
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;

        // afficher le menu de game over
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void HealPlayer(int amount)
    {
        if (currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash() // M�thode sur un thread ind�pendant ...
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay); // Met en pause le code un certain temps
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimmeAfterHit);
        isInvincible = false;
    }
}
