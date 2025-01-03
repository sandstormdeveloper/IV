using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour
{
    private Vector2 initPos;

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
            player.transform.position = initPos;
        }
    }
}
