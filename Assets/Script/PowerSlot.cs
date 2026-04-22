using UnityEngine;

public class PowerSlot : MonoBehaviour
{
    public int requiredFuel = 2;
    public Lever leverScript;
    public GameObject lever;

    [Header("Save System")]
    public string powerSlotID;

    private bool playerInRange = false;
    private PlayerFuel playerFuel;
    private bool isActivated = false;

    // ? ?? Awake ??? SaveSystem ????? Activate(false) ???

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            if (playerFuel == null)
            {
                Debug.Log("No PlayerFuel");
                return;
            }

            if (playerFuel.fuel <= 0)
                Debug.Log("No Power");
            else if (playerFuel.fuel < requiredFuel)
                Debug.Log("Need more: " + (requiredFuel - playerFuel.fuel));
            else
                Activate(true);
        }
    }

    // ? ??????????? public ???????? SaveSystem ????????
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
        {
            playerInRange = true;
            playerFuel = col.GetComponent<PlayerFuel>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
            playerFuel = null;
        }
    }
}
