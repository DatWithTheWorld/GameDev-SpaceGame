using UnityEngine;

/// <summary>
/// Automatically generates stars on background to create space effect
/// These stars don't interact with player (no collider)
/// </summary>
public class BackgroundStarGenerator : MonoBehaviour
{
    [Header("Star Settings")]
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 100;
    
    [Header("Spawn Boundaries")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;
    [SerializeField] private float zPosition = 5f;
    
    [Header("Random Settings")]
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 0.3f;
    [SerializeField] private bool randomColor = true;
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found! Using default boundaries.");
        }
        else
        {
            CalculateCameraBounds();
        }
        
        GenerateStars();
    }

    /// <summary>
    /// Calculate boundaries based on camera view
    /// </summary>
    private void CalculateCameraBounds()
    {
        if (mainCamera == null) return;
        
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camPos = mainCamera.transform.position;
        
        minX = camPos.x - camWidth - 2f;
        maxX = camPos.x + camWidth + 2f;
        minY = camPos.y - camHeight - 2f;
        maxY = camPos.y + camHeight + 2f;
    }

    /// <summary>
    /// Generate random stars on background
    /// </summary>
    private void GenerateStars()
    {
        if (starPrefab == null)
        {
            Debug.LogWarning("Star Prefab is not assigned!");
            return;
        }
        
        for (int i = 0; i < starCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                zPosition
            );
            
            GameObject star = Instantiate(starPrefab, randomPos, Quaternion.identity);
            
            float randomScale = Random.Range(minScale, maxScale);
            star.transform.localScale = new Vector3(randomScale, randomScale, 1);
            
            if (randomColor)
            {
                SpriteRenderer sr = star.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    Color[] starColors = new Color[]
                    {
                        Color.white,
                        Color.yellow,
                        new Color(0.7f, 0.9f, 1f)
                    };
                    
                    Color randomColor = starColors[Random.Range(0, starColors.Length)];
                    
                    randomColor *= Random.Range(0.7f, 1f);
                    
                    sr.color = randomColor;
                }
            }
            
            star.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Clear all stars (can be called from editor)
    /// </summary>
    [ContextMenu("Clear Stars")]
    public void ClearStars()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// Regenerate stars (can be called from editor)
    /// </summary>
    [ContextMenu("Regenerate Stars")]
    public void RegenerateStars()
    {
        ClearStars();
        GenerateStars();
    }
}
