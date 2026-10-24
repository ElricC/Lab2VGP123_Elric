using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");

        rb.linearVelocityX = moveX * moveSpeed;

        if (jumpPressed)

            rb.AddForce(Vector2.up *jumpForce, ForceMode2D.Impulse);


    }
}
