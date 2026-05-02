using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Header("Settings")]
    // ???????????????????????? ???? "MainMenu"
    public string menuSceneName = "MainMenu";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ????????????????????????????? "??????" ??????????? ?????????????????
        if (scene.name != menuSceneName)
        {
            // 1. ?????????????
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            // 2. ????? Game Object ????
            Destroy(gameObject);

            // 3. ?????? Instance
            Instance = null;
        }
    }
}

