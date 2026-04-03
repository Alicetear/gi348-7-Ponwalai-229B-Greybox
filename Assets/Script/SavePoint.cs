using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject saveUI;
    public GameObject pressEText; 

    private bool playerInRange = false;
    private bool isOpen = false;

    void Start()
    {
        if (saveUI != null)
            saveUI.SetActive(false);

        if (pressEText != null)
            pressEText.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                OpenSaveUI();
            }
        }

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSaveUI();
        }
    }

    void OpenSaveUI()
    {
        saveUI.SetActive(true);

        if (pressEText != null)
            pressEText.SetActive(false);

        Time.timeScale = 0f;
        isOpen = true;
    }

    public void CloseSaveUI()
    {
        saveUI.SetActive(false);

        if (playerInRange && pressEText != null)
            pressEText.SetActive(true);

        Time.timeScale = 1f;
        isOpen = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isOpen && pressEText != null)
                pressEText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }
}
