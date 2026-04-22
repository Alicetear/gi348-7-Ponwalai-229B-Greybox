using UnityEngine;

public enum KeyColor
{
    None,
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Pink
}
public class Key : MonoBehaviour
{
    public int amount = 1;
    public KeyColor color;

    private bool playerInRange = false;
    private playerkey player;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            player.AddKey(amount, color);
            Destroy(gameObject);
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
}
