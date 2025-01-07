using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrototype; // El prefab del enemigo
    public int numberOfEnemies;   // Cantidad de enemigos a generar
    public float spawnRadius;   // Radio en el que se generarán los enemigos

    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // se genera una posición aleatoria dentro del radio
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y; // se mantiene la pos y del spawner

            // Clona el enemigo
            GameObject newEnemy = Instantiate(enemyPrototype, spawnPosition, Quaternion.identity);

            // Nombra al enemigo
            newEnemy.name = "Enemy_" + i;
            
        }
    }
}