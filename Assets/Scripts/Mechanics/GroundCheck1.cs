using UnityEngine;

public class GroundCheck1
{
    // Ground check variables that are set in the inspector
    private Collider2D col;
    private Rigidbody2D rb;
    private LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    public bool isGrounded { get; private set; }

    private Vector2 groundCheckPos => CalculateGroundCheckPos();

    // Foot position helper function to calculate the ground check position based on the collider's bounds
    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = col.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    public GroundCheck1(Collider2D col, Rigidbody2D rb, LayerMask groundLayer, float radius)
    {
        this.col = col;
        this.rb = rb;
        this.groundLayer = groundLayer;
        groundCheckRadius = radius;
    }

    // Update is called once per frame
    public bool CheckGround()
    {
        if (!isGrounded && rb.linearVelocityY <= 0 || isGrounded)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
        }

        return isGrounded;
    }
}
