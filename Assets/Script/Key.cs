using UnityEngine;

public enum KeyColor
{
    None,
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Pink,
    Fuel
}
public class Key : MonoBehaviour
{
    public int amount = 1;
    public KeyColor color;

    private bool playerInRange = false;
    private PlayerInventory inventory;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            inventory.AddKey(amount, color);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            inventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            inventory = null;
        }
    }
}
