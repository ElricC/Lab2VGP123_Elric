using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Ground check variables that are set in the inspector
    [SerializeField] //Accessible to inspector, BUT the variable only be used at this place and not controllable anywhere else
    private LayerMask groundLayer; //private means it cannot be accessed by other component
    [SerializeField] private float groundCheckRadius = 0.2f;
    public bool isGrounded { get; private set; }

    private Collider2D col;
    private Rigidbody2D rb;
    private Vector2 groundCheckPos => CalculateGroundCheckPos();

    // Foot position helper function to calculate the ground check position based on the collider's bounds
    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = col.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    public void Init(Collider2D col, Rigidbody2D rb)
    {
        this.col = col;
        this.rb = rb;
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

    public void SetGroundAfterJump()
    {
        isGrounded = false;
    }
}
