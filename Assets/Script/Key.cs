using UnityEngine;
using UnityEngine.InputSystem;

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
    public string keyID;

    void Start()
    {
        if (!string.IsNullOrEmpty(keyID) &&
            SaveSystem.Instance != null &&
            SaveSystem.Instance.currentOpenedDoors.Contains(keyID))
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            inventory.AddKey(amount, color);

            if (!string.IsNullOrEmpty(keyID) && SaveSystem.Instance != null)
                if (!SaveSystem.Instance.currentOpenedDoors.Contains(keyID))
                    SaveSystem.Instance.currentOpenedDoors.Add(keyID);

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
