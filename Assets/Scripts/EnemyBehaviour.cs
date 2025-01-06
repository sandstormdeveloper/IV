using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask groundLayerMask;

    public float speed = 3.0f;
    public bool movingRight = true;
    public float extraDitst = 0.1f;

    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        //Ray positions
        Vector2 leftRayPos = new Vector2(boxCollider2d.bounds.min.x, boxCollider2d.bounds.center.y);
        Vector2 centerRayPos = boxCollider2d.bounds.center;
        Vector2 rightRayPos = new Vector2(boxCollider2d.bounds.max.x, boxCollider2d.bounds.center.y);
        Vector2 down = Vector2.down;
        Vector2 left = Vector2.left;
        Vector2 right = Vector2.right;


        //If on edge or wall turn around
        if ((!IsGrounded(rightRayPos, down) && movingRight) || (!IsGrounded(leftRayPos, down) && !movingRight))
        {           //right edge                                            //left edge 
            if (!IsGrounded(centerRayPos, down))
            {       //center  
                movingRight = !movingRight;
            }
        }
        if ((IsGrounded(rightRayPos, right) && movingRight) || (IsGrounded(leftRayPos, left) && !movingRight))
        {          //right wall                                             //left wall
            movingRight = !movingRight;
        }

        if (movingRight)
        {
            rigidbody2d.velocity = new Vector2(+speed, rigidbody2d.velocity.y);     //Flip movement
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));  //Flip animator
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y);
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
    private bool IsGrounded(Vector2 rayPos, Vector2 rayDir)
    {
        //Detection ray
        RaycastHit2D raycastHit = Physics2D.Raycast(rayPos, rayDir, boxCollider2d.bounds.extents.y + extraDitst, groundLayerMask);
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
        Debug.DrawRay(rayPos, rayDir * (boxCollider2d.bounds.extents.y + extraDitst), rayColor);

        //return
        return raycastHit.collider != null;
    }
    
}
