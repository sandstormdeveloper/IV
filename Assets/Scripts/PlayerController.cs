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

        if(value.isPressed)
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
        }
    }

    void Run()
    {
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
        }
    }
}

