using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    private Rigidbody2D rb;
    private float jumpSpeed;

    public JumpCommand(Rigidbody2D rb, float jumpSpeed)
    {
        this.rb = rb;
        this.jumpSpeed = jumpSpeed;
    }

    public void Execute()
    {
        rb.velocity += new Vector2(0f, jumpSpeed);
    }
}
