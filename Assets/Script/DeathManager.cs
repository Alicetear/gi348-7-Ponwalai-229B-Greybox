using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject deathUI;

    private bool isDead = false;

    void Start()
    {
        if (deathUI != null)
            deathUI.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ShowDeathUI()
    {
        Debug.Log("ShowDeathUI called");

        if (isDead) return;

        isDead = true;

        if (deathUI != null)
        {
            deathUI.SetActive(true);
            Debug.Log("DeathUI opened");
        }
        else
        {
            Debug.LogError("deathUI is NULL! ??? DeathUI ????? Inspector ????");
        }

        Time.timeScale = 0f;
    }

    public void LoadLastSave()
    {
        SaveSystem save = FindObjectOfType<SaveSystem>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (save != null)
        {
            save.LoadGame();
        }

        if (playerHealth != null)
        {
            playerHealth.ResetDeathState();
            save.LoadGame();
        }

        deathUI.SetActive(false);
        Time.timeScale = 1f;
        isDead = false;
    }

    public void CloseDeathUI()
    {
        if (deathUI != null)
            deathUI.SetActive(false);

        Time.timeScale = 1f;
        isDead = false;
    }

    public void RetryScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
