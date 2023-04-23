using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private float chanceToSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Random.Range(0, 100) <= chanceToSpawn)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, respawnPosition.position, Quaternion.identity);
            Destroy(newEnemy, 30);
        }

    }

}
