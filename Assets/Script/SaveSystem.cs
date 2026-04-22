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

    [Header("References")]
    public Transform player;
    public PlayerHealth playerHealth;
    public playerkey inventory;

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
        AutoFind();
        if (isRespawning)
            StartCoroutine(ApplyLoadData());
    }

    public void AutoFind()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            playerHealth = p.GetComponent<PlayerHealth>();
            inventory = p.GetComponent<playerkey>();
        }
    }

    IEnumerator ApplyLoadData()
    {
        yield return new WaitForEndOfFrame();

        SaveData data = GetSlotData(lastUsedSlot);
        if (data == null || player == null)
        {
            isRespawning = false;
            yield break;
        }

        // ? ???? IDs ????
        currentOpenedDoors = new List<string>(data.openedDoorIDs);

        // ? ???????????????????????
        Door[] allDoors = UnityEngine.Object.FindObjectsByType<Door>(FindObjectsSortMode.None);
        foreach (Door d in allDoors)
            if (!string.IsNullOrEmpty(d.doorID) && currentOpenedDoors.Contains(d.doorID))
                d.SetOpenedFromSave();

        // ? ???? Lever ?????????????
        Lever[] allLevers = UnityEngine.Object.FindObjectsByType<Lever>(FindObjectsSortMode.None);
        foreach (Lever l in allLevers)
            if (!string.IsNullOrEmpty(l.leverID) && currentOpenedDoors.Contains(l.leverID))
                l.Activate(false);

        // ? ???? PowerSlot ????????????????
        PowerSlot[] allPowerSlots = UnityEngine.Object.FindObjectsByType<PowerSlot>(FindObjectsSortMode.None);
        foreach (PowerSlot ps in allPowerSlots)
            if (!string.IsNullOrEmpty(ps.powerSlotID) && currentOpenedDoors.Contains(ps.powerSlotID))
                ps.Activate(false);

        // ? ?????????????? HP
        player.position = new Vector2(data.posX, data.posY);

        if (playerHealth != null)
        {
            playerHealth.currentHealth = data.health;
            playerHealth.ResetDeathState();
        }

        // ? ?????????
        if (inventory != null)
        {
            inventory.redKey = data.redKey;
            inventory.blueKey = data.blueKey;
            inventory.greenKey = data.greenKey;
            inventory.yellowKey = data.yellowKey;
            inventory.purpleKey = data.purpleKey;
            inventory.pinkKey = data.pinkKey;
        }

        isRespawning = false;
    }

    public void SaveGame(int slot, string screenshotB64 = "")
    {
        AutoFind();
        if (player == null) return;

        lastUsedSlot = slot;

        SaveData data = new SaveData
        {
            posX = player.position.x,
            posY = player.position.y,
            health = playerHealth != null ? playerHealth.currentHealth : 100,
            redKey = inventory != null ? inventory.redKey : 0,
            blueKey = inventory != null ? inventory.blueKey : 0,
            greenKey = inventory != null ? inventory.greenKey : 0,
            yellowKey = inventory != null ? inventory.yellowKey : 0,
            purpleKey = inventory != null ? inventory.purpleKey : 0,
            pinkKey = inventory != null ? inventory.pinkKey : 0,
            sceneName = SceneManager.GetActiveScene().name,
            saveTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            playTime = Time.timeSinceLevelLoad,
            screenshotB64 = screenshotB64,
            openedDoorIDs = new List<string>(currentOpenedDoors)
        };

        PlayerPrefs.SetString("save_slot_" + slot, JsonUtility.ToJson(data));
        PlayerPrefs.Save();

        Debug.Log($"Saved Slot {slot + 1} | Doors opened: {currentOpenedDoors.Count}");
    }

    public void RespawnAtLastSave()
    {
        SaveData data = GetSlotData(lastUsedSlot);
        if (data != null)
        {
            // ? ?? save data ? ???? save
            currentOpenedDoors = new List<string>(data.openedDoorIDs);
            isRespawning = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // ? ????? save data ? reload scene ?????? ??????? load save
            isRespawning = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadGameFromMenu(int slot)
    {
        if (!PlayerPrefs.HasKey("save_slot_" + slot)) return;

        lastUsedSlot = slot;
        isRespawning = true;

        SceneManager.LoadScene(GetSlotData(slot).sceneName);
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