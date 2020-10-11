using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public void Move(Rigidbody2D rb, float moveDirection, float moveSpeed)
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    public void AirMove(Rigidbody2D rb, float moveDirection, float moveSpeed)
    {
        if (moveDirection < 0 && rb.velocity.x > moveDirection * moveSpeed)
        {
            rb.AddForce(new Vector2(moveDirection * moveSpeed, 0), ForceMode2D.Force);
        }
        else if (moveDirection > 0 && rb.velocity.x < moveDirection * moveSpeed)
        {
            rb.AddForce(new Vector2(moveDirection * moveSpeed, 0), ForceMode2D.Force);
        }
    }

    public void Jump(Rigidbody2D rb, int jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
