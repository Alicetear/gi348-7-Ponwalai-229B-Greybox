using UnityEngine;

public class CloseSaveUI : MonoBehaviour
{
    public GameObject saveUI;

    public void Close()
    {
        saveUI.SetActive(false);
        Time.timeScale = 1f; 
    }
}
