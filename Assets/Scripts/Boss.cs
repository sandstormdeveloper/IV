using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    private SpriteRenderer sp;

    public float speed = 2.0f;

    public float attackCooldown;
    private float attackTimer = 0;
    private bool isAttacking = false;

    private int currentHealth;
    private bool dead = false;

    private ICommand currentCommand;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            Move();
        }
        else
        {
            rb.gravityScale = 0f;
            col.enabled = false;
        }
    }

    void Move()
    {
        float playerDist = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(playerDist) < 10f)
        {
            currentCommand = new MoveCommand(rb, speed, new Vector2(Mathf.Sign(playerDist), 0));
            currentCommand.Execute();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void Damage(int damage)
    {
        if (!dead)
        {
            currentHealth -= damage;
            sp.color = Color.red;
            Invoke("ChangeColor", 0.05f);

            if (currentHealth <= 0)
            {
                Die();
                GameObject.FindGameObjectWithTag("Flag").GetComponent<Flag>().EliminatedEnemy();
            }
        }
    }

    private void ChangeColor()
    {
        sp.color = Color.white;
    }

    private void Die()
    {
        dead = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetBool("isDead", true);
    }
}
