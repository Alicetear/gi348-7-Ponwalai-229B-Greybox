using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyContextMenu : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI keyLabel;
    public Button useButton;
    public Button closeButton;

    private KeyColor targetColor;
    private KeySlotUI parentSlot;

    // ?????????????????????????????????????????????
    // Setup (called by KeySlotUI after Instantiate)
    // ?????????????????????????????????????????????

    public void Setup(KeyColor color, KeySlotUI slot)
    {
        targetColor = color;
        parentSlot = slot;

        int count = PlayerInventory.Instance != null
            ? PlayerInventory.Instance.GetKeyCount(color)
            : 0;

        if (keyLabel != null)
            keyLabel.text = $"{color} Key  x{count}";

        if (useButton != null)
            useButton.onClick.AddListener(OnUse);

        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
    }


    void OnUse()
    {
        if (PlayerInventory.Instance == null) { Close(); return; }

        bool used = PlayerInventory.Instance.UseKey(1, targetColor);

        if (used)
            Debug.Log($"[Inventory] Used 1x {targetColor} key manually.");
        else
            Debug.Log($"[Inventory] No {targetColor} key to use.");

        Close();
    }

    void Close()
    {
        if (parentSlot != null)
            parentSlot.CloseMenu();
        else
            Destroy(gameObject);
    }

    // Close if clicked outside
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // Small delay so the right-click that opened us doesn't immediately close us
            if (Time.frameCount > spawnFrame + 1)
                Close();
        }
    }

    private int spawnFrame;
    void Awake() => spawnFrame = Time.frameCount;
}
