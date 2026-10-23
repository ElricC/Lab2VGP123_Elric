using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{

    public float moveSpeed = 5f;
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
        
    }
}
