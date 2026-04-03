using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player Died");

        DeathManager dm = FindObjectOfType<DeathManager>();

        if (dm != null)
        {
            dm.ShowDeathUI();
        }
        else
        {
            Debug.LogError("DeathManager not found in scene!");
        }
    }

    public void RevivePlayer()
    {
        isDead = false;

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth; 
        }

        gameObject.SetActive(true);
    }

    public void ResetDeathState()
    {
        isDead = false;
    }
}
