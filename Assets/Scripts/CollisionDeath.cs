using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{
    private Vector2 initPos;

    public PlayerController playerHealth;
    public int fallDamage = 20;

    public AudioSource audioSource;
    public AudioClip soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            initPos = player.transform.position;
        }
    }

    //Función encargada de recolocar al jugador si cae al vacio
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.Damage(fallDamage);
                audioSource.clip = soundEffect;
                audioSource.Play();
            }
            player.transform.position = initPos;
        }
    }
}
