using UnityEngine;

/// <summary>
/// Script for collectible star
/// When player touches star, receives points and star disappears
/// </summary>
public class StarCollectible : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    
    [Header("Float Animation")]
    [SerializeField] private bool useFloatAnimation = true;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatAmount = 0.3f;
    
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        if (useFloatAnimation)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    /// <summary>
    /// Handle when player touches star
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectStar();
        }
    }

    /// <summary>
    /// Collect star: add points and destroy star
    /// </summary>
    private void CollectStar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        
        Destroy(gameObject);
    }
}
