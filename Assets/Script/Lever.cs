using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public bool hasPower;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("HasPower = " + hasPower);

            if (!hasPower)
            {
                Debug.Log("No Power");
            }
            else if (hasPower)
            {
                Debug.Log("Door Open");
                door.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
