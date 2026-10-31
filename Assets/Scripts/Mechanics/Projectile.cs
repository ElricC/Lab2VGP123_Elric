using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float lifetime = 5.0f;

    [SerializeField]
    private Vector2 initialVelocity = Vector2.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);

        if (initialVelocity != Vector2.zero)
        {

            SetVelocity(initialVelocity);
        }
    }

    // Update is called once per frame
    public void SetVelocity(Vector2 velocity)
    { 
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }
}
