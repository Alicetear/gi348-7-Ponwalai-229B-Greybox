using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public bool hasPower = false;

    [Header("Save System")]
    public string leverID;

    private bool playerInRange = false;
    private bool isActivated = false;

    // ? ????? Awake ???? SaveSystem ????? Activate(false) ???

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            if (!hasPower)
                Debug.Log("No Power");
            else
                Activate(true);
        }
    }

    public void Activate(bool saveState)
    {
        isActivated = true;

        if (saveState && SaveSystem.Instance != null && !string.IsNullOrEmpty(leverID))
            if (!SaveSystem.Instance.currentOpenedDoors.Contains(leverID))
                SaveSystem.Instance.currentOpenedDoors.Add(leverID);

        if (door != null)
            door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) playerInRange = false;
    }
}
