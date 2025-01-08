using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EnemyBehaviour : MonoBehaviour, IEnemy
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask playerLayerMask;

    public float speed = 3.0f;
    public bool movingRight = true;
    public float extraDitst = 0.1f;

    private bool isAttacking = false;
    private float attackCooldown = 2;
    private float attackTimer = 0;
    private bool hit = false;

    private int currentHealth = 3;
    private bool dead = false;

    Vector2 down = Vector2.down;
    Vector2 left = Vector2.left;
    Vector2 right = Vector2.right;

    private ICommand currentCommand;
    private Flag flag;

    PlayerController player;
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        coll = transform.GetComponent<BoxCollider2D>();
        anim = transform.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector2 leftRayPos = new Vector2(coll.bounds.min.x, coll.bounds.center.y);
            Vector2 rightRayPos = new Vector2(coll.bounds.max.x, coll.bounds.center.y);

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 1.4f && !hit)
                {
                    if (movingRight)
                    {
                        if (player.transform.position.x > transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f)
                        {
                            player.Damage(8);
                        } 
                    } 
                    else
                    {
                        if (player.transform.position.x < transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f)
                        {
                            player.Damage(8);
                        }
                    }
 
                    hit = true;
                }
            }

            if (movingRight)
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

            Movement();
            UpdateSprite();
        } 
        else
        {
            rb.gravityScale = 0f;
            coll.enabled = false;
        }
    }
    
    private void Attack()
    {
        currentCommand = new AttackCommand(anim, attackCooldown, cooldown => attackTimer = cooldown);
        currentCommand.Execute();
        rb.velocity = new Vector2(0, rb.velocity.y);
        hit = false;
    }
    private void Movement()
    {
        Vector2 leftRayPos = new Vector2(coll.bounds.min.x, coll.bounds.center.y);
        Vector2 rightRayPos = new Vector2(coll.bounds.max.x, coll.bounds.center.y);
        Vector2 centerRayPos = coll.bounds.center;

        if (!RayDetection(centerRayPos, down, groundLayerMask))
        {
            if ((!RayDetection(rightRayPos, down, groundLayerMask) && movingRight) || (!RayDetection(leftRayPos, down, groundLayerMask) && !movingRight))
            {
                movingRight = !movingRight;
            }
        }
        if ((RayDetection(rightRayPos, right, groundLayerMask) && movingRight) || (RayDetection(leftRayPos, left, groundLayerMask) && !movingRight))
        {
            movingRight = !movingRight;
        }

        if(!isAttacking)
        {
            if (movingRight)
            {
                currentCommand = new MoveCommand(rb, speed, new Vector2(1, 0));
                currentCommand.Execute();
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            }
            else
            {
                currentCommand = new MoveCommand(rb, speed, new Vector2(-1, 0));
                currentCommand.Execute();
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void UpdateSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("isMoving", playerHasHorizontalSpeed);
    }

    private bool RayDetection(Vector2 rayPos, Vector2 rayDir, LayerMask layer)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(rayPos, rayDir, coll.bounds.extents.y + extraDitst, layer);
        return raycastHit.collider != null;
    }

    public void Damage(int damage)
    {
        if(!dead)
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
        gameObject.tag = "Death";
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetBool("isDead", true);
    }
    public void OnSpawn()
    {
        // Lógica de inicialización específica al generarse
        Debug.Log($"{gameObject.name} ha sido generado");
    }
}
