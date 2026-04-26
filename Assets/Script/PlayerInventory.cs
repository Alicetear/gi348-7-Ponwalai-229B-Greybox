using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [Header("Fuel")]
    public int maxFuel = 10;
    public int currentFuel = 0;

    [Header("Keys")]
    private Dictionary<KeyColor, int> keys = new Dictionary<KeyColor, int>();

    [Header("Generic Items")]
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public event System.Action OnInventoryChanged;

    // Unity
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (KeyColor color in System.Enum.GetValues(typeof(KeyColor)))
        {
            if (color != KeyColor.None)
                keys[color] = 0;
        }
    }

    // Fuel
    public bool AddFuel(int amount)
    {
        if (currentFuel >= maxFuel) return false;
        currentFuel = Mathf.Clamp(currentFuel + amount, 0, maxFuel);
        Debug.Log($"[Inventory] Fuel: {currentFuel}/{maxFuel}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool UseFuel(int amount)
    {
        if (currentFuel < amount) return false;
        currentFuel -= amount;
        Debug.Log($"[Inventory] Fuel used. Remaining: {currentFuel}/{maxFuel}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public int GetFuel() => currentFuel;
    public bool HasFuel(int amount) => currentFuel >= amount;

    // Keys
    public void AddKey(int amount, KeyColor color)
    {
        if (color == KeyColor.None) return;
        keys[color] += amount;
        Debug.Log($"[Inventory] Added {amount}x {color} key. Total: {keys[color]}");
        OnInventoryChanged?.Invoke();
    }

    public bool UseKey(int amount, KeyColor color)
    {
        if (color == KeyColor.None) return false;
        if (!keys.ContainsKey(color) || keys[color] < amount) return false;
        keys[color] -= amount;
        Debug.Log($"[Inventory] Used {amount}x {color} key. Remaining: {keys[color]}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public int GetKeyCount(KeyColor color) => keys.TryGetValue(color, out int count) ? count : 0;
    public bool HasKey(int amount, KeyColor color) => GetKeyCount(color) >= amount;
    public Dictionary<KeyColor, int> GetAllKeys() => new Dictionary<KeyColor, int>(keys);

    // Generic Items

    public void AddItem(string itemName, int amount = 1)
    {
        if (!items.ContainsKey(itemName)) items[itemName] = 0;
        items[itemName] += amount;
        Debug.Log($"[Inventory] Added {amount}x {itemName}. Total: {items[itemName]}");
        OnInventoryChanged?.Invoke();
    }

    public bool UseItem(string itemName, int amount = 1)
    {
        if (!items.ContainsKey(itemName) || items[itemName] < amount) return false;
        items[itemName] -= amount;
        if (items[itemName] <= 0) items.Remove(itemName);
        Debug.Log($"[Inventory] Used {amount}x {itemName}.");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public int GetItemCount(string itemName) => items.TryGetValue(itemName, out int count) ? count : 0;
    public bool HasItem(string itemName, int amount = 1) => GetItemCount(itemName) >= amount;
    public Dictionary<string, int> GetAllItems() => new Dictionary<string, int>(items);
}
