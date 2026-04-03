using UnityEngine;

public class playerkey : MonoBehaviour
{
    public int redKey = 0;
    public int blueKey = 0;
    public int greenKey = 0;
    public int yellowKey = 0;
    public int purpleKey = 0;
    public int pinkKey = 0;

    public void AddKey(int amount, KeyColor color)
    {
        if (color == KeyColor.Red) redKey += amount;
        if (color == KeyColor.Blue) blueKey += amount;
        if (color == KeyColor.Green) greenKey += amount;
        if (color == KeyColor.Yellow) yellowKey += amount;
        if (color == KeyColor.Purple) purpleKey += amount;
        if (color == KeyColor.Pink) pinkKey += amount;

        Debug.Log("Red: " + redKey + " Blue: " + blueKey + " Green: " + greenKey + " Yellow: " + yellowKey + " Purple: " + purpleKey + " Pink: " + pinkKey);
    }

    public bool UseKey(int amount, KeyColor doorColor)
    {
        if (doorColor == KeyColor.Red && redKey >= amount)
        {
            redKey -= amount;
            return true;
        }

        if (doorColor == KeyColor.Blue && blueKey >= amount)
        {
            blueKey -= amount;
            return true;
        }

        if (doorColor == KeyColor.Green && greenKey >= amount)
        {
            greenKey -= amount;
            return true;
        }

        if (doorColor == KeyColor.Yellow && yellowKey >= amount)
        {
            yellowKey -= amount;
            return true;
        }

        if (doorColor == KeyColor.Purple && purpleKey >= amount)
        {
            purpleKey -= amount;
            return true;
        }

        if (doorColor == KeyColor.Pink && pinkKey >= amount)
        {
            pinkKey -= amount;
            return true;
        }

        return false;
    }
}
