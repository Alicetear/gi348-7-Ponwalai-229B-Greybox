using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialUI : MonoBehaviour
{
    [System.Serializable]
    public class TutorialPage
    {
        public string message;
        public Sprite image;
    }

    public TutorialPage[] pages;
    public TextMeshProUGUI messageText;
    public Image tutorialImage;
    public GameObject tutorialPanel;
    public float displayTime = 5f;

    void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false); // ???????????
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowTutorial());
            Destroy(GetComponent<Collider2D>()); 
        }
    }

    IEnumerator ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;

        foreach (TutorialPage page in pages)
        {
            messageText.text = page.message;

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

        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
