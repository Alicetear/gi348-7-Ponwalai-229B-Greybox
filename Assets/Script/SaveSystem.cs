using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public float posX, posY;
    public int health;
    public int redKey, blueKey, greenKey, yellowKey, purpleKey, pinkKey;
    public int fuel;
    public string sceneName;
    public string saveTime;
    public float playTime;           
    public string screenshotB64;
    public List<string> openedDoorIDs = new List<string>();
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public static bool isRespawning = false;
    public static int lastUsedSlot = 0;

    public List<string> currentOpenedDoors = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isRespawning)
            StartCoroutine(ApplyLoadData());
    }

    IEnumerator ApplyLoadData()
    {
        yield return new WaitForEndOfFrame();

        SaveData data = GetSlotData(lastUsedSlot);
        if (data == null) { isRespawning = false; yield break; }

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null) { isRespawning = false; yield break; }

        // ???????
        p.transform.position = new Vector2(data.posX, data.posY);

        // HP
        PlayerHealth ph = p.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.currentHealth = data.health;
            ph.ResetDeathState();
        }

        // Inventory
        PlayerInventory inv = PlayerInventory.Instance;
        if (inv != null)
        {
            inv.SetKeys(data.redKey, data.blueKey, data.greenKey,
                        data.yellowKey, data.purpleKey, data.pinkKey);
            inv.currentFuel = data.fuel;
        }

        // Doors / Levers / PowerSlots
        currentOpenedDoors = new List<string>(data.openedDoorIDs);

        Door[] allDoors = UnityEngine.Object.FindObjectsByType<Door>(FindObjectsSortMode.None);
        foreach (Door d in allDoors)
            if (!string.IsNullOrEmpty(d.doorID) && currentOpenedDoors.Contains(d.doorID))
                d.SetOpenedFromSave();

        Lever[] allLevers = UnityEngine.Object.FindObjectsByType<Lever>(FindObjectsSortMode.None);
        foreach (Lever l in allLevers)
            if (!string.IsNullOrEmpty(l.leverID) && currentOpenedDoors.Contains(l.leverID))
                l.Activate(false);

        PowerSlot[] allPowerSlots = UnityEngine.Object.FindObjectsByType<PowerSlot>(FindObjectsSortMode.None);
        foreach (PowerSlot ps in allPowerSlots)
            if (!string.IsNullOrEmpty(ps.powerSlotID) && currentOpenedDoors.Contains(ps.powerSlotID))
                ps.Activate(false);

        isRespawning = false;
    }

    public void SaveGame(int slot)
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null) return;

        lastUsedSlot = slot;

        PlayerHealth ph = p.GetComponent<PlayerHealth>();
        PlayerInventory inv = PlayerInventory.Instance;

        SaveData data = new SaveData
        {
            posX = p.transform.position.x,
            posY = p.transform.position.y,
            health = ph != null ? ph.currentHealth : 100,
            fuel = inv != null ? inv.currentFuel : 0,
            redKey = inv != null ? inv.GetKeyCount(KeyColor.Red) : 0,
            blueKey = inv != null ? inv.GetKeyCount(KeyColor.Blue) : 0,
            greenKey = inv != null ? inv.GetKeyCount(KeyColor.Green) : 0,
            yellowKey = inv != null ? inv.GetKeyCount(KeyColor.Yellow) : 0,
            purpleKey = inv != null ? inv.GetKeyCount(KeyColor.Purple) : 0,
            pinkKey = inv != null ? inv.GetKeyCount(KeyColor.Pink) : 0,
            sceneName = SceneManager.GetActiveScene().name,
            saveTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            openedDoorIDs = new List<string>(currentOpenedDoors)
        };

        PlayerPrefs.SetString("save_slot_" + slot, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
        Debug.Log($"Saved slot {slot} | Scene: {data.sceneName} | Doors: {currentOpenedDoors.Count}");
    }

    public void LoadGame(int slot)
    {
        SaveData data = GetSlotData(slot);
        if (data == null) { Debug.LogWarning("No save data in slot " + slot); return; }

        lastUsedSlot = slot;
        isRespawning = true;
        SceneManager.LoadScene(data.sceneName);
    }

    public void RespawnAtLastSave()
    {
        LoadGame(lastUsedSlot);
    }

    public SaveData GetSlotData(int slot)
    {
        string json = PlayerPrefs.GetString("save_slot_" + slot, "");
        return string.IsNullOrEmpty(json) ? null : JsonUtility.FromJson<SaveData>(json);
    }

    public void DeleteSlot(int slot)
    {
        PlayerPrefs.DeleteKey("save_slot_" + slot);
        PlayerPrefs.Save();
    }
}