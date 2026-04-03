using UnityEngine;

public class Door : MonoBehaviour
{
    public KeyColor color;

    private bool playerInRange = false;
    private playerkey player;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (player.UseKey(1, color))
            {
                Open();
                Debug.Log("Door opened");
            }
            else
            {
                Debug.Log("I need a key.");
            }
        }
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

    void Open()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
