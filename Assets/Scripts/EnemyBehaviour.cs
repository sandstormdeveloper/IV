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
        //If on edge turn around
        if(!CenterGrounded() && !RightGrounded() && movingRight)
        {
            movingRight = !movingRight;
        }else if(!CenterGrounded() && !LeftGrounded() && !movingRight)
        {
            movingRight = !movingRight;
        }

        if (movingRight)
        {
            rigidbody2d.velocity = new Vector2(+speed, rigidbody2d.velocity.y);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y);
        }

    }

    private bool LeftGrounded()
    {
        
        Vector2 leftRayPos = new Vector2(boxCollider2d.bounds.min.x, boxCollider2d.bounds.center.y);
        
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(leftRayPos, Vector2.down, boxCollider2d.bounds.extents.y + extraDitst, groundLayerMask);

        Color leftRayColor;
        if (raycastHitLeft.collider != null)
        {
            leftRayColor = Color.green;
        }
        else
        {
            leftRayColor = Color.red;
        }
        Debug.DrawRay(leftRayPos, Vector2.down * (boxCollider2d.bounds.extents.y + extraDitst), leftRayColor);


        return raycastHitLeft.collider != null;

    }
    private bool CenterGrounded()
    {
        
        Vector2 centerRayPos = boxCollider2d.bounds.center;

        RaycastHit2D raycastHitCenter = Physics2D.Raycast(centerRayPos, Vector2.down, boxCollider2d.bounds.extents.y + extraDitst, groundLayerMask);
        
        Color centerRayColor;
        if (raycastHitCenter.collider != null)
        {
            centerRayColor = Color.green;
        }
        else
        {
            centerRayColor = Color.red;
        }
        Debug.DrawRay(centerRayPos, Vector2.down * (boxCollider2d.bounds.extents.y + extraDitst), centerRayColor);
        return raycastHitCenter.collider != null;

    }
    private bool RightGrounded()
    {
        Vector2 rightRayPos = new Vector2(boxCollider2d.bounds.max.x, boxCollider2d.bounds.center.y);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(rightRayPos, Vector2.down, boxCollider2d.bounds.extents.y + extraDitst, groundLayerMask);
        Color rightRayColor;
        if (raycastHitRight.collider != null)
        {
            rightRayColor = Color.green;
        }
        else
        {
            rightRayColor = Color.red;
        }
        Debug.DrawRay(rightRayPos, Vector2.down * (boxCollider2d.bounds.extents.y + extraDitst), rightRayColor);
        return raycastHitRight.collider != null;

    }
}
