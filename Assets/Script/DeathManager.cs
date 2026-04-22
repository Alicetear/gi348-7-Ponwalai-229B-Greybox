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
        if (isDead) return;
        isDead = true;

        if (deathUI != null)
        {
            deathUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LoadLastSave()
    {
        SaveSystem save = FindFirstObjectByType<SaveSystem>();

        if (save != null)
        {
            if (deathUI != null)
                deathUI.SetActive(false);

            Time.timeScale = 1f;

            save.LoadGameFromMenu(0); 
        }
    }

    public void NewGame()
    {
        if (deathUI != null)
            deathUI.SetActive(false);

        Time.timeScale = 1f;

        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
