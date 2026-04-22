using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;

    void Start()
    {
        if (!SaveSystem.isRespawning)
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0) { currentHealth = 0; Die(); }
    }

    void Die()
    {
        if (isDead) return; // ? ??? Die ???
        isDead = true;

        DeathManager dm = FindFirstObjectByType<DeathManager>();
        if (dm != null) dm.ShowDeathUI();

        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        if (SaveSystem.Instance != null)
            SaveSystem.Instance.RespawnAtLastSave();
    }

    public void ResetDeathState()
    {
        isDead = false; // ? reset ?????? damage ???????
    }
}