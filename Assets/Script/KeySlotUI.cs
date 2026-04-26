using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class KeySlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Slot Config")]
    public KeyColor color;

    [Header("Slot References")]
    public Image icon;
    public TextMeshProUGUI countText;
    public GameObject highlight;

    [Header("Context Menu")]
    public GameObject contextMenuPrefab;
    public Transform contextMenuParent;

    private GameObject activeMenu;

    public void UpdateSlot(int count)
    {
        if (countText != null)
            countText.text = count > 0 ? $"x{count}" : "x0";
        if (icon != null)
            icon.color = count > 0 ? Color.white : new Color(1, 1, 1, 0.3f);
        if (highlight != null)
            highlight.SetActive(count > 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked! Button: " + eventData.button);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int count = PlayerInventory.Instance != null
                ? PlayerInventory.Instance.GetKeyCount(color)
                : 0;
            if (count <= 0) return;
            CloseMenu();
            SpawnMenu(eventData.position);
        }
        else
        {
            CloseMenu();
        }
    }

    void SpawnMenu(Vector2 screenPos)
{
    if (contextMenuPrefab == null || contextMenuParent == null) return;

    activeMenu = Instantiate(contextMenuPrefab, contextMenuParent);

    RectTransform rt = activeMenu.GetComponent<RectTransform>();
    rt.localScale = Vector3.one;

    // spawn ?????????? slot ???
    RectTransform myRT = GetComponent<RectTransform>();
    rt.position = myRT.position;

    KeyContextMenu menu = activeMenu.GetComponent<KeyContextMenu>();
    if (menu != null)
        menu.Setup(color, this);
}

    public void CloseMenu()
    {
        if (activeMenu != null)
        {
            Destroy(activeMenu);
            activeMenu = null;
        }
    }

    void OnDisable() => CloseMenu();
}