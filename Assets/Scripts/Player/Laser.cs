using UnityEngine;

/// <summary>
/// Script for laser shot from spaceship
/// Laser moves straight forward and self-destructs when off screen or on collision
/// </summary>
public class Laser : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    
    [Header("Life Time")]
    [SerializeField] private float lifeTime = 5f;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    /// <summary>
    /// Handle when laser collides with asteroid or enemy
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            AsteroidController asteroid = other.GetComponent<AsteroidController>();
            if (asteroid != null)
            {
                asteroid.DestroyAsteroid();
            }
            
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroy laser when off screen (if using OnBecameInvisible)
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
