using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody2D rb;
    private float moveSpeed;
    private Vector2 direction;

    public MoveCommand(Rigidbody2D rb, float moveSpeed, Vector2 direction)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.direction = direction;
    }

    public void Execute()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
}
