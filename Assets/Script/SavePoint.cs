using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SavePoint : MonoBehaviour
{
    public GameObject saveUI;
    public GameObject pressEText;
    public SaveSlotUI[] slots;

    bool playerInRange = false;
    bool isOpen = false;

    void Start()
    {
        if (saveUI != null) saveUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isOpen && Input.GetKeyDown(KeyCode.F))
            StartCoroutine(OpenSaveUI());

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
            CloseSaveUI();
    }

    IEnumerator OpenSaveUI()
    {
        yield return new WaitForEndOfFrame();
        saveUI.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        if (slots == null || SaveSystem.Instance == null) return;
        for (int i = 0; i < slots.Length; i++)
        {
            SaveData data = SaveSystem.Instance.GetSlotData(i);
            slots[i].Setup(i, data, this);
        }
    }

    public void OnSlotSavePressed(int slot)
    {
        SaveSystem.Instance.SaveGame(slot);
        RefreshSlots();
    }

    public void CloseSaveUI()
    {
        if (saveUI != null) saveUI.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEText) pressEText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEText) pressEText.SetActive(false);
            CloseSaveUI();
        }
    }
}