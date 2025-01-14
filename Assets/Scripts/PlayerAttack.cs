using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBehaviour>().Damage(1);
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().Damage(1);
        }
    }
}
