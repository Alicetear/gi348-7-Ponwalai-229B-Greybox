using UnityEngine;

public class CloseSaveUI1 : MonoBehaviour
{
    public GameObject saveUI;

    public void Close()
    {
        saveUI.SetActive(false);
        Time.timeScale = 1f; 
    }
}
