using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFuel player = other.GetComponent<PlayerFuel>();
                player.fuel += amount;
                Destroy(gameObject);
        }
    }
}
