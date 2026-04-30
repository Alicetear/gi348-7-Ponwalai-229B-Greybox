using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;
    public GameObject deathUI;
    private bool isDead = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (deathUI == null)
            deathUI = GameObject.Find("UIDeath");

        if (deathUI != null)
        {
            DontDestroyOnLoad(deathUI);
            deathUI.SetActive(false);

            UnityEngine.UI.Button newGameBtn = deathUI.transform
                .Find("Panel/Button")?.GetComponent<UnityEngine.UI.Button>();
            UnityEngine.UI.Button loadSaveBtn = deathUI.transform
                .Find("Panel/LoadSave")?.GetComponent<UnityEngine.UI.Button>();

            if (newGameBtn != null)
            {
                newGameBtn.onClick.RemoveAllListeners();
                newGameBtn.onClick.AddListener(NewGame);
            }
            else
                Debug.LogWarning("?????????? Button ?? Panel");

            if (loadSaveBtn != null)
            {
                loadSaveBtn.onClick.RemoveAllListeners();
                loadSaveBtn.onClick.AddListener(LoadLastSave);
            }
            else
                Debug.LogWarning("?????????? LoadSave ?? Panel");
        }

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
        isDead = false;

        if (deathUI != null)
            deathUI.SetActive(false);

        Time.timeScale = 1f;

        if (SaveSystem.Instance != null)
            SaveSystem.Instance.RespawnAtLastSave();
        else
            Debug.LogWarning("SaveSystem not found!");
    }

    public void NewGame()
    {
        isDead = false;
        Time.timeScale = 1f;

        if (deathUI != null)
            deathUI.SetActive(false);

        SceneManager.LoadScene("Scene1");
    }
}
