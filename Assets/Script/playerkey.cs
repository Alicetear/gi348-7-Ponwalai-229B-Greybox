using UnityEngine;

public class playerkey : MonoBehaviour
{
    [Header("Key Inventory")]
    public int redKey = 0;
    public int blueKey = 0;
    public int greenKey = 0;
    public int yellowKey = 0;
    public int purpleKey = 0;
    public int pinkKey = 0;


    public void AddKey(int amount, KeyColor color)
    {
        switch (color)
        {
            case KeyColor.Red: redKey += amount; break;
            case KeyColor.Blue: blueKey += amount; break;
            case KeyColor.Green: greenKey += amount; break;
            case KeyColor.Yellow: yellowKey += amount; break;
            case KeyColor.Purple: purpleKey += amount; break;
            case KeyColor.Pink: pinkKey += amount; break;
        }

        Debug.Log($"Key Added! Total -> R:{redKey} B:{blueKey} G:{greenKey} Y:{yellowKey} P:{purpleKey} Pi:{pinkKey}");
    }

    public bool UseKey(int amount, KeyColor doorColor)
    {
        switch (doorColor)
        {
            case KeyColor.Red:
                if (redKey >= amount) { redKey -= amount; return true; }
                break;
            case KeyColor.Blue:
                if (blueKey >= amount) { blueKey -= amount; return true; }
                break;
            case KeyColor.Green:
                if (greenKey >= amount) { greenKey -= amount; return true; }
                break;
            case KeyColor.Yellow:
                if (yellowKey >= amount) { yellowKey -= amount; return true; }
                break;
            case KeyColor.Purple:
                if (purpleKey >= amount) { purpleKey -= amount; return true; }
                break;
            case KeyColor.Pink:
                if (pinkKey >= amount) { pinkKey -= amount; return true; }
                break;
        }
        return false;
    }
}