using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth)
            {
                // Jouer le son
                AudioManager.instance.PlayClipAt(pickupSound, transform.position);
                // Rendre de la vie au joueur
                PlayerHealth.instance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}
