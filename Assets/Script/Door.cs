using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public KeyColor color;
    public string doorID;
    public bool isOpened = false;

    [Header("UI")]
    public GameObject promptUI;
    public TextMeshProUGUI promptText;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;      
    public AudioClip lockedSound;

    [Header("Animation")]
    public Animator doorAnimator;

    private bool playerInRange = false;


    void Update()
    {
        if (!playerInRange || isOpened) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            var inventory = PlayerInventory.Instance;
            if (inventory == null) return;

            if (inventory.HasKey(1, color))
            {
                inventory.UseKey(1, color);
                PlaySound(openSound);
                Open();
            }
            else
            {
                PlaySound(lockedSound);
                StartCoroutine(ShowMessage("Locked. I need a key."));
            }
        }
    }
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowPrompt("Press [E] to Open");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HidePrompt();
        }
    }

    void ShowPrompt(string message)
    {
        if (promptUI == null) return;
        promptUI.SetActive(true);
        if (promptText != null)
            promptText.text = message;
    }

    void HidePrompt()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    System.Collections.IEnumerator ShowMessage(string message)
    {
        ShowPrompt(message);
        yield return new WaitForSeconds(2f);
        if (playerInRange)
            ShowPrompt("Press [E] to Open");
        else
            HidePrompt();
    }

    public void Open()
    {
        isOpened = true;
        HidePrompt();

        if (SaveSystem.Instance != null && !string.IsNullOrEmpty(doorID))
            if (!SaveSystem.Instance.currentOpenedDoors.Contains(doorID))
                SaveSystem.Instance.currentOpenedDoors.Add(doorID);

        StartCoroutine(OpenAfterSound());
    }


    System.Collections.IEnumerator OpenAfterSound()
    {
        if (doorAnimator != null)
            doorAnimator.SetTrigger("Open");

        PlaySound(openSound);

        yield return new WaitForSeconds(
            openSound != null ? openSound.length : 1f
        );

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
}