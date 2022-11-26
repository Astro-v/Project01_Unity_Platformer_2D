using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public AudioClip killSound;
    public int coinsToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(killSound, transform.position);
            Inventory.instance.AddCoins(coinsToAdd);
            Destroy(objectToDestroy);
         }
    }
}
