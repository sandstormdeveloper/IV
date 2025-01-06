using Navegacion;
using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float attackCooldown;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;

    private float attackTimer = 0;
    private bool isAttacking = false;

    //Variables vida jugador
    public Slider playerHealthSlider;
    public int maxHealth = 50;
    private int currentHealth;

    private ICommand currentCommand;
    public UIController uiController;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (playerHealthSlider != null)
        {
            playerHealthSlider.maxValue = maxHealth;
            playerHealthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        UpdateTimers();
        Run();
        UpdateSprite();
        playerHealthSlider.value = currentHealth;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            currentCommand = new JumpCommand(rb, jumpSpeed);
            currentCommand.Execute();
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && attackTimer <= 0)
        {
            currentCommand = new AttackCommand(anim, attackCooldown, cooldown => attackTimer = cooldown);
            currentCommand.Execute();
            isAttacking = true;
        }
    }

    private void Run()
    {
        if (!isAttacking)
        {
            currentCommand = new MoveCommand(rb, moveSpeed, moveInput);
            currentCommand.Execute();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void UpdateSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void UpdateTimers()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0.5f)
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

    public void Damage(int damage)
    {
        currentHealth -= damage;
        
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(5);
        }
    }

    private void Die()
    {
        if (uiController != null)
        {
            uiController.setState(new Die(uiController));
        }

        Debug.Log("The player died");
    }

}


