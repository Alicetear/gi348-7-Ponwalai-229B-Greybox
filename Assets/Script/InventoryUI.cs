using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject inventoryPanel;
    private bool isOpen = false;

    [Header("Fuel UI")]
    public TextMeshProUGUI fuelText;
    public Slider fuelSlider;

    [Header("Key UI")]
    public KeySlotUI[] keySlots;

    [Header("Generic Item UI")]
    public Transform itemListContainer;
    public GameObject itemRowPrefab;

    private Dictionary<string, GameObject> itemRows = new Dictionary<string, GameObject>();

    void Start()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryChanged += RefreshUI;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        RefreshUI();
    }

    void OnDestroy()
    {
        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.OnInventoryChanged -= RefreshUI;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
            TogglePanel();
    }

    public void TogglePanel()
    {
        isOpen = !isOpen;
        if (inventoryPanel != null)
            inventoryPanel.SetActive(isOpen);

        if (isOpen) RefreshUI();
    }

    public void RefreshUI()
    {
        if (PlayerInventory.Instance == null) return;
        RefreshFuel();
        RefreshKeys();
        RefreshItems();
    }

    void RefreshFuel()
    {
        int cur = PlayerInventory.Instance.GetFuel();
        int max = PlayerInventory.Instance.maxFuel;

        if (fuelText != null)
            fuelText.text = $"Fuel: {cur} / {max}";

        if (fuelSlider != null)
        {
            fuelSlider.maxValue = max;
            fuelSlider.value = cur;
        }
    }

    void RefreshKeys()
    {
        if (keySlots == null) return;

        foreach (var slot in keySlots)
        {
            if (slot == null) continue;
            int count = PlayerInventory.Instance.GetKeyCount(slot.color);
            slot.UpdateSlot(count);
        }
    }

    void RefreshItems()
    {
        if (itemListContainer == null || itemRowPrefab == null) return;

        var allItems = PlayerInventory.Instance.GetAllItems();

        var toRemove = new List<string>();
        foreach (var kv in itemRows)
            if (!allItems.ContainsKey(kv.Key)) toRemove.Add(kv.Key);
        foreach (var key in toRemove)
        {
            Destroy(itemRows[key]);
            itemRows.Remove(key);
        }

        foreach (var kv in allItems)
        {
            if (!itemRows.ContainsKey(kv.Key))
            {
                var row = Instantiate(itemRowPrefab, itemListContainer);
                itemRows[kv.Key] = row;
            }

            var tmp = itemRows[kv.Key].GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = $"{kv.Key}  x{kv.Value}";
        }
    }
}
