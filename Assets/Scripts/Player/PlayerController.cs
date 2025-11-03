using UnityEngine;

/// <summary>
/// Controls player spaceship
/// Handles movement with arrow keys and screen boundaries
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Boundary Settings")]
    [SerializeField] private float padding = 0.5f;
    
    [Header("References")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    
    private Camera mainCamera;
    private float nextFireTime = 0f;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogError("Camera.main not found! Please tag your main camera as 'MainCamera'");
        }
        
        CalculateScreenBounds();
        
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    /// <summary>
    /// Calculate screen boundaries based on camera
    /// </summary>
    private void CalculateScreenBounds()
    {
        if (mainCamera == null) return;
        
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        
        minX = mainCamera.transform.position.x - camWidth + padding;
        maxX = mainCamera.transform.position.x + camWidth - padding;
        minY = mainCamera.transform.position.y - camHeight + padding;
        maxY = mainCamera.transform.position.y + camHeight - padding;
    }

    /// <summary>
    /// Handle spaceship movement with arrow keys
    /// </summary>
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        
        transform.position = newPosition;
    }

    /// <summary>
    /// Handle shooting laser when Space or fire key is pressed
    /// </summary>
    private void HandleShooting()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            ShootLaser();
            nextFireTime = Time.time + fireRate;
        }
    }

    /// <summary>
    /// Create laser at firePoint and shoot forward
    /// </summary>
    private void ShootLaser()
    {
        if (laserPrefab == null)
        {
            Debug.LogWarning("Laser Prefab is not assigned!");
            return;
        }
        
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }

    /// <summary>
    /// Handle when spaceship collides with asteroid
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            GameManager.Instance?.PlayerHit();
        }
    }
}
