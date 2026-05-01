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
    public GameObject promptUI;
     public float hideDistance = 2f;

    private Transform playerTransform;

    void Start()
    {
        if (promptUI != null) promptUI.SetActive(false);
        if (!string.IsNullOrEmpty(keyID) &&
            SaveSystem.Instance != null &&
            SaveSystem.Instance.currentOpenedDoors.Contains(keyID))
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (playerTransform != null)
        {
            float dist = Vector2.Distance(transform.position, playerTransform.position);
            if (dist > hideDistance)
            {
                playerInRange = false;
                playerTransform = null;
                if (promptUI != null) promptUI.SetActive(false);
            }
        }

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
        if (other.CompareTag("Player") && !playerInRange)
        {
            playerInRange = true;
            playerTransform = other.transform;
            inventory = other.GetComponent<PlayerInventory>();
            if (promptUI != null)
            {
                if (promptUI.transform.parent != null)
                    promptUI.transform.parent.gameObject.SetActive(true);
                promptUI.SetActive(true);
            }
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
