using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask playerLayerMask;

    public float speed = 3.0f;
    public bool movingRight = true;
    public float extraDitst = 0.1f;

    public PlayerController playerHealth;

    private bool isAttacking = false;
    private float attackCooldown = 1;
    private float attackTimer = 0;
    private bool hit = false;

    private int currentHealth = 3;
    private bool dead = false;

    Vector2 down = Vector2.down;
    Vector2 left = Vector2.left;
    Vector2 right = Vector2.right;

    private ICommand currentCommand;

    GameObject player;
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        coll = transform.GetComponent<BoxCollider2D>();
        anim = transform.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
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

                if (attackTimer <= 0.4f && !hit)
                {
                    if (movingRight)
                    {
                        if (transform.position.x - player.transform.position.x < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f)
                        {
                            playerHealth.Damage(8);
                        } 
                    } 
                    else
                    {
                        if (player.transform.position.x - transform.position.x < 3f && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f)
                        {
                            playerHealth.Damage(8);
                        }
                    }
 
                    hit = true;
                }
            }

            if (!isAttacking)
            {
                Movement();
            }

            if (attackTimer <= 0)
            {
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2f)
                {
                    Attack();
                    isAttacking = true;
                } 
                else
                {
                    isAttacking = false;
                }
            }
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
            Invoke("ChangeColor", 0.05f);

            if (currentHealth <= 0)
            {
                Die();
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
