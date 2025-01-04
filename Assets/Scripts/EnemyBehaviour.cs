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
        Vector2 leftRayPos = new Vector2(boxCollider2d.bounds.min.x, boxCollider2d.bounds.center.y);
        Vector2 centerRayPos = boxCollider2d.bounds.center;
        Vector2 rightRayPos = new Vector2(boxCollider2d.bounds.max.x, boxCollider2d.bounds.center.y);

        //If on edge turn around
        if (!IsGrounded(centerRayPos) && !IsGrounded(rightRayPos) && movingRight)
        {
            movingRight = !movingRight;
        }else if(!IsGrounded(centerRayPos) && !IsGrounded(leftRayPos) && !movingRight)
        {
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
    private bool IsGrounded(Vector2 rayPos)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(rayPos, Vector2.down, boxCollider2d.bounds.extents.y + extraDitst, groundLayerMask);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(rayPos, Vector2.down * (boxCollider2d.bounds.extents.y + extraDitst), rayColor);


        return raycastHit.collider != null;
    }

}
