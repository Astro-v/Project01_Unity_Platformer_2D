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

    // Créér un unique PlayerHealth accéssible de partout
    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
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

            // vérifier si le joueur est toujours vivant
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

        // jouer l'annimation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Die");

        // empêcher les intéraction physique avec les autres éléments de la scéne
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

    public IEnumerator InvincibilityFlash() // Méthode sur un thread indépendant ...
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
