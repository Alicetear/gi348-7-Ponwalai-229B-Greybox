using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public Transform player;
    public PlayerHealth playerHealth;
    public playerkey inventory;

    public static bool shouldLoadAfterSceneReset = false;

    void Start()
    {
        if (shouldLoadAfterSceneReset)
        {
            LoadGame();
            shouldLoadAfterSceneReset = false;
        }
    }
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("posX", player.position.x);
        PlayerPrefs.SetFloat("posY", player.position.y);

        PlayerPrefs.SetInt("health", playerHealth.currentHealth);

        PlayerPrefs.SetInt("redKey", inventory.redKey);
        PlayerPrefs.SetInt("blueKey", inventory.blueKey);
        PlayerPrefs.SetInt("greenKey", inventory.greenKey);
        PlayerPrefs.SetInt("yellowKey", inventory.yellowKey);
        PlayerPrefs.SetInt("purpleKey", inventory.purpleKey);
        PlayerPrefs.SetInt("pinkKey", inventory.pinkKey);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        float x = PlayerPrefs.GetFloat("posX");
        float y = PlayerPrefs.GetFloat("posY");
        player.position = new Vector2(x, y);

        playerHealth.currentHealth = PlayerPrefs.GetInt("health");

        inventory.redKey = PlayerPrefs.GetInt("redKey");
        inventory.blueKey = PlayerPrefs.GetInt("blueKey");
        inventory.greenKey = PlayerPrefs.GetInt("greenKey");
        inventory.yellowKey = PlayerPrefs.GetInt("yellowKey");
        inventory.purpleKey = PlayerPrefs.GetInt("purpleKey");
        inventory.pinkKey = PlayerPrefs.GetInt("pinkKey");

        playerHealth.ResetDeathState();

        Time.timeScale = 1f;
        Debug.Log("Game Loaded");
    }
}
