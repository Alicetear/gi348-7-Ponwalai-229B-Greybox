using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.AddFuel(amount))
            {
                Destroy(gameObject);
            }
        }
    }
}