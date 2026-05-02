using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class changemainmenu : MonoBehaviour
{
    [Header("Scene Load Settings")]
    public bool useIndex = true;
    public int mainMenuIndex = 0;
    public string mainMenuSceneName = "MainMenu";
    public void ToMainMenu()
    {
        Debug.Log("Button Clicked!"); 
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
