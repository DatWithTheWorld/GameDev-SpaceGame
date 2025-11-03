using UnityEngine;

/// <summary>
/// Controls asteroid: random movement and collision handling
/// Asteroids can move in different directions
/// </summary>
public class AsteroidController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 50f;
    
    [Header("Random Movement")]
    [SerializeField] private bool useRandomMovement = true;
    [SerializeField] private float directionChangeInterval = 2f;
    [SerializeField] private float directionChangeChance = 0.3f;
    
    private Vector2 moveDirection;
    private float lastDirectionChange;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
        
        if (useRandomMovement)
        {
            moveDirection = Random.insideUnitCircle.normalized;
        }
        else
        {
            moveDirection = Vector2.down;
        }
        
        lastDirectionChange = Time.time;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        if (useRandomMovement && Time.time - lastDirectionChange >= directionChangeInterval)
        {
            if (Random.value < directionChangeChance)
            {
                moveDirection = Random.insideUnitCircle.normalized;
            }
            lastDirectionChange = Time.time;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    /// <summary>
    /// Handle when asteroid goes off screen - teleport to other side or destroy
    /// </summary>
    private void OnBecameInvisible()
    {
    }

    /// <summary>
    /// Handle when asteroid collides with player
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyAsteroid();
        }
    }

    /// <summary>
    /// Destroy asteroid (can add explosion effect or animation)
    /// </summary>
    public void DestroyAsteroid()
    {
        GameManager.Instance?.AsteroidDestroyed();
        
        Destroy(gameObject);
    }
}
