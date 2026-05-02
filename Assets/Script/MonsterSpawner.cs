using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject monsterPrefab; 
    public Transform spawnPoint;    
    public bool destroyAfterSpawn = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something hit the trigger: " + other.name);
        if (other.CompareTag("Player"))
        {
            SpawnMonster();

            if (destroyAfterSpawn)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefab != null)
        {
            Vector3 positionToSpawn = spawnPoint != null ? spawnPoint.position : transform.position;

            Instantiate(monsterPrefab, positionToSpawn, Quaternion.identity);

            Debug.Log("Monster Spawned!");
        }
        else
        {
            Debug.LogWarning("?????????? Monster Prefab ??????? Inspector ??????!");
        }
    }
}
