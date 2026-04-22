using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SaveSlotUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI slotNumberText;
    public TextMeshProUGUI saveTimeText;

    public RawImage screenshotImage;

    public Button saveButton;
    public Button loadButton;
    public Button deleteButton;

    public GameObject emptyLabel;
    public GameObject filledGroup;

    [HideInInspector] public int slotIndex;
    [HideInInspector] public SavePoint savePoint;

    public void Setup(int index, SaveData data, SavePoint sp)
    {
        slotIndex = index;
        savePoint = sp;

        if (slotNumberText != null)
            slotNumberText.text = "Slot " + (index + 1);

        if (data != null)
        {
            if (emptyLabel != null) emptyLabel.SetActive(false);
            if (filledGroup != null) filledGroup.SetActive(true);

            if (saveTimeText != null)
            {
                TimeSpan t = TimeSpan.FromSeconds(data.playTime);
                string playTimeStr = "Play Time : "     + t.ToString(@"hh\:mm\:ss");

                string dateStr = data.saveTime;

                saveTimeText.text = playTimeStr + "    " + dateStr;
            }

            LoadImage(data.screenshotB64);
        }
        else
        {
            if (emptyLabel != null) emptyLabel.SetActive(true);
            if (filledGroup != null) filledGroup.SetActive(false);

            if (saveTimeText != null)
                saveTimeText.text = "";

            if (screenshotImage != null)
                screenshotImage.texture = null;
        }

        if (saveButton != null)
        {
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(() =>
            {
                savePoint.OnSlotSavePressed(slotIndex);
            });
        }


    }

    void LoadImage(string b64)
    {
        if (screenshotImage == null) return;

        if (string.IsNullOrEmpty(b64))
        {
            screenshotImage.texture = null;
            return;
        }

        byte[] bytes = Convert.FromBase64String(b64);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        screenshotImage.texture = tex;
    }
}
