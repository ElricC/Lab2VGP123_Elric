using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Vector2 initShotVelocity = new Vector2(5, 5);
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(5, 5);
            Debug.LogWarning("Shoot: InitShotVelocity not defined - setting to a default 5, 5");
        }

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Shoot: one or more spawn points or projectile prefab is not assigned - in order to use the shoot component - it has to be assigned");
        }
    }

    // Update is called once per frame
    public void Fire()
    {
        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Fire will not work because the shoot script is missing a spawn point or projectile prefab reference");
            return;
        }

        Projectile curProjectile;

        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }    
    }
}
