using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lever : MonoBehaviour
{
    public Light2D globalLight;
    public GameObject door;
    public bool hasPower = false;

    [Header("Save System")]
    public string leverID;

    [Header("Audio Settings")]
    public AudioSource audioSource;    
    public AudioClip activateSound;   
    public AudioClip noPowerSound;

    private bool playerInRange = false;
    private bool isActivated = false;


    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivated)
        {
            if (!hasPower)
            {
                Debug.Log("No Power");
                if (audioSource != null && noPowerSound != null)
                {
                    audioSource.PlayOneShot(noPowerSound);
                }
            }
            else
            {
                Activate(true);
                if (audioSource != null && activateSound != null)
                {
                    audioSource.PlayOneShot(activateSound);
                }
            }
        }
    }

    public void Activate(bool saveState)
    {
        isActivated = true;

        if (globalLight != null)       
            globalLight.intensity = 1f;

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
