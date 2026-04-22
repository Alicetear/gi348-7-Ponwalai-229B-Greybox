using UnityEngine;

public class CloseSaveUI : MonoBehaviour
{
    public SavePoint savePoint;

    public void Close()
    {
        if (savePoint == null)
        {
            Debug.LogError("CloseSaveUI: savePoint is not assigned");
            return;
        }

        savePoint.CloseSaveUI(); 
    }
}
