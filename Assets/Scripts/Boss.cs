using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    private SpriteRenderer sp;

    public float speed = 2.5f;

    public float attackCooldown;
    private float attackTimer = 0;
    private bool isAttacking = false;

    private int currentHealth = 10;
    private bool dead = false;
    private bool hit = false;
    private float flipped;

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
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 1.4f && !hit)
                {
                    if (flipped < 0)
                    {
                        if (player.transform.position.x > transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 4f && Mathf.Abs(transform.position.y - player.transform.position.y) < 3f)
                        {
                            player.Damage(12);
                        }
                    }
                    else
                    {
                        if (player.transform.position.x < transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 4f && Mathf.Abs(transform.position.y - player.transform.position.y) < 3f)
                        {
                            player.Damage(12);
                        }
                    }

                    hit = true;
                }
            }

            if (flipped < 0)
            {
                if (player.transform.position.x > transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 2f)
                {
                    isAttacking = true;

                    if (attackTimer <= 0)
                    {
                        Attack();
                    }
                }
                else
                {
                    if (hit)
                    {
                        isAttacking = false;
                    }
                }
            }
            else
            {
                if (player.transform.position.x < transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 2f)
                {
                    isAttacking = true;

                    if (attackTimer <= 0)
                    {
                        Attack();
                    }
                }
                else
                {
                    if (hit)
                    {
                        isAttacking = false;
                    }
                }
            }

            Move();
            UpdateSprite();
        }
        else
        {
            rb.gravityScale = 0f;
            col.enabled = false;
        }
    }

    void Move()
    {
        if(!isAttacking)
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
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Attack()
    {
        currentCommand = new AttackCommand(anim, attackCooldown, cooldown => attackTimer = cooldown);
        currentCommand.Execute();
        rb.velocity = new Vector2(0, rb.velocity.y);
        hit = false;
    }

    private void UpdateSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("isMoving", playerHasHorizontalSpeed);

        if (playerHasHorizontalSpeed)
        {
            flipped = Mathf.Sign(-rb.velocity.x);
            transform.localScale = new Vector2(flipped * 5, 5f);
            
        }
    }

    public void Damage(int damage)
    {
        if (!dead)
        {
            currentHealth -= damage;
            sp.color = Color.red;
            Invoke("ChangeColor", 0.1f);

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
        anim.SetTrigger("dead");
    }
}
