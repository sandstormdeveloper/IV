using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrototype; // El prefab del enemigo
    public int numberOfEnemies = 5;   // Cantidad de enemigos a generar
    public float spawnRadius = 10f;   // Radio en el que se generar�n los enemigos

    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generar una posici�n aleatoria dentro del radio
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; // Mantener en el plano horizontal

            // Clonar el prototipo del enemigo
            GameObject newEnemy = Instantiate(enemyPrototype, spawnPosition, Quaternion.identity);

            // Personalizar el enemigo 
            newEnemy.name = "Enemy_" + i;
        }
    }
}