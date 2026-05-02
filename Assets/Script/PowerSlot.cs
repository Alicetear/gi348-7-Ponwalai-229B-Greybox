using TMPro;
using UnityEngine;

public class PowerSlot : MonoBehaviour
{
    public int requiredFuel = 2;
    public Lever leverScript;
    public GameObject lever;

    [Header("UI Settings")]
    public GameObject interactionUI;    
    public TextMeshProUGUI hintText;

    [Header("Save System")]
    public string powerSlotID;

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip activateSound;

    private bool playerInRange = false;
    private bool isActivated = false;

    void Update()
    {
        if (playerInRange && !isActivated)
        {
            UpdateUI();

            if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivated)
            {
                PlayerInventory inventory = PlayerInventory.Instance;

                if (inventory == null)
                {
                    Debug.Log("No PlayerInventory");
                    return;
                }

                if (inventory.GetFuel() <= 0)
                    Debug.Log("No Power");
                else if (inventory.GetFuel() < requiredFuel)
                    Debug.Log("Need more: " + (requiredFuel - inventory.GetFuel()));
                else
                {
                    inventory.UseFuel(requiredFuel);
                    Activate(true);

                    if (audioSource != null && activateSound != null)
                    {
                        audioSource.PlayOneShot(activateSound);
                    }
                }
            }
        }
    }

    void UpdateUI()
    {
        if (hintText != null)
        {
            PlayerInventory inventory = PlayerInventory.Instance;
            int currentFuel = (inventory != null) ? inventory.GetFuel() : 0;

            if (currentFuel < requiredFuel)
            {
                int needed = requiredFuel - currentFuel;
                hintText.text = "Need " + needed + " more Fuel (E)";
            }
            else
            {
                hintText.text = "Press (E) to Activate Power";
            }
        }
    }

    public void Activate(bool saveState)
    {
        isActivated = true;

        if (saveState && SaveSystem.Instance != null && !string.IsNullOrEmpty(powerSlotID))
            if (!SaveSystem.Instance.currentOpenedDoors.Contains(powerSlotID))
                SaveSystem.Instance.currentOpenedDoors.Add(powerSlotID);

        if (leverScript != null)
        {
            leverScript.hasPower = true;
            Debug.Log("Power ON");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInRange = false;
    }
}
