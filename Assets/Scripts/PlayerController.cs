using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D col;
    public float attackCooldown;
    float attackTimer = 0;
    bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        UpdateSprite();
        UpdateTimers();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!col.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (!IsGrounded())
        {
            return;
        }

        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && attackTimer <= 0)
        {
            anim.SetTrigger("attack");
            attackTimer = attackCooldown;
            isAttacking = true;
        }
    }

    void Run()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0 * moveSpeed, rb.velocity.y);
            return;
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
    }

    void UpdateSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0.1f;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            anim.SetBool("isRunning", true);
        } 
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void UpdateTimers()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer < attackCooldown - 0.5)
            {
                isAttacking = false;
            }
        }
    }

    private bool IsGrounded()
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int contactCount = col.GetContacts(contacts);

        for (int i = 0; i < contactCount; i++)
        {
            if (contacts[i].normal == Vector2.up)
            {
                return true;
            }
        }
        return false;
    }


}

