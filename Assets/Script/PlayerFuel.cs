using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    public int fuel = 0;
    public int currentFuel;

    public void AddFuel(int amount)
    {
        fuel += amount;
        Debug.Log("Fuel: " + fuel);
    }

    public bool UseFuel(int amount)
    {
        if (fuel >= amount)
        {
            fuel -= amount;
            return true;
        }
        return false;
    }
}
