using TMPro;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Collections;

public class EndCradit : MonoBehaviour
{
    [System.Serializable]
    public class TutorialPage
    {
        [TextArea(3, 10)]
        public string message;
        public Sprite image;
    }

    [Header("UI References")]
    public TutorialPage[] pages;
    public TextMeshProUGUI messageText;
    public Image tutorialImage;
    public GameObject tutorialPanel;

    [Header("Settings")]
    public float displayTime = 5f;

    [Header("Scene Load Settings")]
    public bool useIndex = true;
    public int mainMenuIndex = 0;
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        if (pages != null && pages.Length > 0)
        {
            StartCoroutine(ShowTutorial());
        }
    }

    IEnumerator ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0f;

            foreach (TutorialPage page in pages)
            {
                if (messageText != null) messageText.text = page.message;
                if (tutorialImage != null)
                {
                    if (page.image != null)
                    {
                        tutorialImage.sprite = page.image;
                        tutorialImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        tutorialImage.gameObject.SetActive(false);
                    }
                }
                yield return new WaitForSecondsRealtime(displayTime);
            }

            Time.timeScale = 1f;
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f; 
        if (useIndex)
        {
            SceneManager.LoadScene(mainMenuIndex);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName); 
        }
    }
}
