using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Life : Pickup
{
    [SerializeField] private int lifeAmount = 1;
    private Rigidbody2D rb;

    public override void OnPlayerPickup(PlayerController interactor)
    {
        if (interactor != null)
        {
            interactor.lives += lifeAmount;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-4f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = -2f;
    }
}
