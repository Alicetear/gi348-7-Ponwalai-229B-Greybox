using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damagePerSecond = 10f;
    private PlayerHealth player;
    private float damageAccumulator = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
            player = ph;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            player = null;
            damageAccumulator = 0f;
        }
    }

    void Update()
    {
        if (player != null)
        {
            damageAccumulator += damagePerSecond * Time.deltaTime;
            if (damageAccumulator >= 1f)
            {
                int dmg = (int)damageAccumulator;
                player.TakeDamage(dmg);
                damageAccumulator -= dmg;
                Debug.Log("Health: " + player.currentHealth);
            }
        }
    }
}
