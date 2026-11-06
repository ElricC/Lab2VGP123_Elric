using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator), typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    #region Tunable Parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public int maxLives = 9;
    public float initialPowerupDuration = 5f;
    public float jumpForcePowerup = 10;
    public float dashDistance = 5f;
    public float dashTime = 0.2f;
    #endregion

    #region Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Animator anim;
    private GroundCheck gndChk;
    #endregion

    #region Getters and Setters
   
   
    private int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (value >= maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                _lives = 0;
                //go to game over
            }
            else if (_lives > value)
            {
                _lives = value;
                //respawn player
            }
            else
            {
                _lives = value;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            value = Mathf.Max(0, value);
            _score = value;
        }
    }
    #endregion

    #region Properties
    private bool isDashing = false;
    private float initDashForce => dashDistance / dashTime;
    private float currentDashTime = 0f;
    private int currentJumps = 0;
    private float currentPowerupDuration = 0f;
    private float initialJumpForce = 5f;

    private Coroutine jumpForceCoroutine = null;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        gndChk = GetComponent<GroundCheck>();
        gndChk.Init(bc, rb);

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {

        gndChk.CheckGround();

        //collect inputs for current frame
        //bool upPressed = Input.GetAxisRaw("Vertical") > 0f;
        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPresssed = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");
        bool dashInput = Input.GetButtonDown("Fire2");

        if (!isDashing)
        {
            rb.linearVelocityX = moveX * moveSpeed;

            if (jumpPresssed && currentJumps < maxJumps)
            {
                currentJumps++;
                gndChk.SetGroundAfterJump();
                rb.linearVelocityY = 0f;
                rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            }

            if (dashInput && !isDashing)
            {
                isDashing = true;
                float dir = sr.flipX ? -1f : 1f;
                rb.gravityScale = 0f;
                rb.linearVelocityY = 0f;
                rb.AddForceX(dir * initDashForce, ForceMode2D.Impulse);
            }

            if (moveX != 0f) SpriteFlip(moveX);
            if (gndChk.isGrounded) currentJumps = 0;
        }
        else
        {
            currentDashTime += Time.deltaTime;
            if (currentDashTime >= dashTime)
            {
                isDashing = false;
                currentDashTime = 0f;
                rb.gravityScale = 1f;
            }
        }


        // Update animator parameters
        anim.SetBool("isGrounded", gndChk.isGrounded);
        anim.SetFloat("moveX", Mathf.Abs(moveX));
        if (fireInput)
        {
            anim.SetTrigger("attack");
        }
        anim.SetFloat("YVel", rb.linearVelocityY);
    }

    void SpriteFlip(float moveX) => sr.flipX = (moveX < 0f);
    //{
    //    if (sr.flipX && moveX > 0f || !sr.flipX && moveX< 0f)
    //    {
    //        sr.flipX = !sr.flipX;
    //    }
    //}

    public void JumpForceChange()
    {
        if (jumpForceCoroutine != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = initialJumpForce;
        }

        jumpForceCoroutine = StartCoroutine(JumpForceChangeCoroutine());
    }

    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
        jumpForce = jumpForcePowerup;

        while (currentPowerupDuration > 0)
        {
            currentPowerupDuration -= Time.deltaTime;
            Debug.Log($"Jump Powerup Time Remaining {currentPowerupDuration}");
            yield return null;
        }

        jumpForce = initialJumpForce;
        jumpForceCoroutine = null;
        currentPowerupDuration = 0f;
    }
}
