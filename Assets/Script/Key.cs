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
    public float hideDistance = 4f;
    public AudioClip pickupSound;
    private Transform playerTransform;

    void Start()
    {
        HidePrompt();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
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
                HidePrompt();
            }
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (inventory != null) inventory.AddKey(amount, color);
            if (!string.IsNullOrEmpty(keyID) && SaveSystem.Instance != null)
                if (!SaveSystem.Instance.currentOpenedDoors.Contains(keyID))
                    SaveSystem.Instance.currentOpenedDoors.Add(keyID);
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, 5f);
            Destroy(gameObject);
        }

    }

    void ShowPrompt()
    {
        if (promptUI == null) return;
        if (promptUI.transform.parent != null)
            promptUI.transform.parent.gameObject.SetActive(true);
        promptUI.SetActive(true);
    }

    void HidePrompt()
    {
        if (promptUI == null) return;
        promptUI.SetActive(false);
        if (promptUI.transform.parent != null)
            promptUI.transform.parent.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInRange)
        {
            playerInRange = true;
            playerTransform = other.transform;
            inventory = other.GetComponent<PlayerInventory>();
            ShowPrompt();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;
            inventory = null;
            HidePrompt();
        }
    }
}
