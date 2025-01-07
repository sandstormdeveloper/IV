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
    private float cooldownTimer = Mathf.Infinity;

    //Ray directions
    Vector2 down = Vector2.down;
    Vector2 left = Vector2.left;
    Vector2 right = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        coll = transform.GetComponent<BoxCollider2D>();
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //player detection rays
        Vector2 leftRayPos = new Vector2(coll.bounds.min.x, coll.bounds.center.y);
        Vector2 rightRayPos = new Vector2(coll.bounds.max.x, coll.bounds.center.y);

        cooldownTimer += Time.deltaTime;

        if (!isAttacking)
        {
            Movement();
        }

        if(cooldownTimer >= attackCooldown)
        { 
            isAttacking = false;
            if ((RayDetection(rightRayPos, right, playerLayerMask) && movingRight) || (RayDetection(leftRayPos, left, playerLayerMask) && !movingRight)) 
            {
                Attack();
            }
        }
    }
    
    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        isAttacking = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        playerHealth.Damage(8);
    }
    private void Movement()
    {

        //Ray positions
        Vector2 leftRayPos = new Vector2(coll.bounds.min.x, coll.bounds.center.y);
        Vector2 rightRayPos = new Vector2(coll.bounds.max.x, coll.bounds.center.y);
        Vector2 centerRayPos = coll.bounds.center;


        //If on edge or wall turn around
        if (!RayDetection(centerRayPos, down, groundLayerMask))
        {           //center
            if ((!RayDetection(rightRayPos, down, groundLayerMask) && movingRight) || (!RayDetection(leftRayPos, down, groundLayerMask) && !movingRight))
            {       //right edge                                                            //left edge  
                movingRight = !movingRight;
            }
        }
        if ((RayDetection(rightRayPos, right, groundLayerMask) && movingRight) || (RayDetection(leftRayPos, left, groundLayerMask) && !movingRight))
        {          //right wall                                                             //left wall
            movingRight = !movingRight;
        }


        //move and change sprite
        if (movingRight)
        {
            rb.velocity = new Vector2(+speed, rb.velocity.y);                       //move right
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));  //flip animator
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);                       //move left
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    
    }

    private bool RayDetection(Vector2 rayPos, Vector2 rayDir, LayerMask layer)
    {
        //Detection ray
        RaycastHit2D raycastHit = Physics2D.Raycast(rayPos, rayDir, coll.bounds.extents.y + extraDitst, layer);
        
        /*    //Paint ray
        //Ray color
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        //Draw ray
        Debug.DrawRay(rayPos, rayDir * (coll.bounds.extents.y + extraDitst), rayColor);
        */

        //return
        return raycastHit.collider != null;
    }

    
}
