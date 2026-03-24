using UnityEngine;

public class PowerSlot : MonoBehaviour
{
    public int requiredFuel = 2;
    public Lever leverScript;
    public GameObject lever;

    private bool playerInRange = false;
    private PlayerFuel playerFuel;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerFuel == null)
            {
                Debug.Log("No PlayerFuel");
                return;
            }

            if (playerFuel.fuel <= 0)
            {
                Debug.Log("No Power");
            }
            else if (playerFuel.fuel < requiredFuel)
            {
                int need = requiredFuel - playerFuel.fuel;
                Debug.Log("Need more: " + need);
            }
            else
            {
                Debug.Log("Power ON");
                Activate();
            }
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

    void Activate()
    {
        Debug.Log("Activated Power");

        if (leverScript != null)
        {
            leverScript.hasPower = true; 
        }
    }
}
