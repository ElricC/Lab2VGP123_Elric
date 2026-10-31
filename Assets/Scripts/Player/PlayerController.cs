using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    #endregion
    #region Ground Check 
   // public Transform groundCheckPos;
    public LayerMask groundLayer;

    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

    private Vector2 CalculateGroundCheckPos()
    {
        Bounds bounds = bc.bounds;
      return new Vector2(bounds.center.x, bounds.min.y);
    }
    #endregion
    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private Animator anim;
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {

        bool FireInput = Input.GetButtonDown("Fire");
        if (rb.linearVelocityY <= 0f)
        {
            isGrounded = Physics2D.OverlapCircle(CalculateGroundCheckPos(), groundCheckRadius, groundLayer);

        }

        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed && isGrounded)
        {

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (moveX != 0f) SpriteFlip(moveX);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));

        if (FireInput)
        {
            anim.SetTrigger("attack");
        }

    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);

   
    //{ 
    //  if (sr.flipX && moveX > 0f || !sr.flipX && moveX < 0f)
    //  {
    //     sr.flipX = !sr.flipX;
    //  }



}
