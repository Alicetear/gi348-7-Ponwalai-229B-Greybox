using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;

    [Header("Invincibility")]
    private bool isInvincible = false;
    public float invincibleDuration = 0.5f;

    void Start()
    {
        if (!SaveSystem.isRespawning)
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0) { currentHealth = 0; Die(); }

        StartCoroutine(InvincibleCooldown());
    }

    System.Collections.IEnumerator InvincibleCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (DeathManager.Instance != null)
            DeathManager.Instance.ShowDeathUI();
        else
            Debug.LogError("DeathManager Instance not found!");
    }

    void Respawn()
    {
        if (SaveSystem.Instance != null)
            SaveSystem.Instance.RespawnAtLastSave();
    }

    public void ResetDeathState()
    {
        isDead = false;
    }
}