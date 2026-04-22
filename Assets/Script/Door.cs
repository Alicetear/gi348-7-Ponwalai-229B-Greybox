using UnityEngine;

public class Door : MonoBehaviour
{
    public KeyColor color;
    public string doorID;
    public bool isOpened = false;

    private bool playerInRange = false;
    private playerkey player;

    // ? ????? Start/Awake ???? SaveSystem ????? SetOpenedFromSave() ???

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            if (player == null) return;

            if (player.UseKey(1, color))
                Open();
            else
                Debug.Log($"I need a key. Need: {color}");
        }
    }

    public void Open()
    {
        isOpened = true;

        if (SaveSystem.Instance != null && !string.IsNullOrEmpty(doorID))
            if (!SaveSystem.Instance.currentOpenedDoors.Contains(doorID))
                SaveSystem.Instance.currentOpenedDoors.Add(doorID);

        ExecuteDisable();
    }

    public void SetOpenedFromSave()
    {
        isOpened = true;
        ExecuteDisable();
    }

    private void ExecuteDisable()
    {
        if (transform.parent != null)
            transform.parent.gameObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.GetComponent<playerkey>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}