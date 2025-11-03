using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages spawning asteroids and stars in gameplay scene
/// Automatically spawns these objects at regular intervals
/// Supports random spawning from multiple prefabs
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject[] starPrefabs;
    
    [Header("Asteroid Spawn Settings")]
    [SerializeField] private float asteroidSpawnRate = 0.5f;
    [SerializeField] private int maxAsteroids = 20;
    [SerializeField] private int initialAsteroidSpawn = 5;
    
    [Header("Star Spawn Settings")]
    [SerializeField] private float starSpawnRate = 1f;
    [SerializeField] private int maxStars = 15;
    [SerializeField] private int initialStarSpawn = 3;
    
    [Header("Spawn Boundaries")]
    [SerializeField] private float spawnPadding = 1f;
    
    private Camera mainCamera;
    private int currentAsteroidCount = 0;
    private int currentStarCount = 0;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
        
        currentAsteroidCount = 0;
        currentStarCount = 0;
        
        SpawnInitialItems();
        
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(SpawnStars());
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Spawn items immediately when game starts
    /// </summary>
    private void SpawnInitialItems()
    {
        for (int i = 0; i < initialAsteroidSpawn; i++)
        {
            SpawnAsteroid();
        }
        
        for (int i = 0; i < initialStarSpawn; i++)
        {
            SpawnStar();
        }
    }

    /// <summary>
    /// Coroutine to spawn asteroids periodically
    /// </summary>
    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(asteroidSpawnRate);
            
            if (currentAsteroidCount < maxAsteroids && asteroidPrefab != null)
            {
                SpawnAsteroid();
            }
        }
    }

    /// <summary>
    /// Coroutine to spawn stars periodically
    /// </summary>
    private IEnumerator SpawnStars()
    {
        while (true)
        {
            yield return new WaitForSeconds(starSpawnRate);
            
            if (currentStarCount < maxStars && starPrefab != null)
            {
                SpawnStar();
            }
        }
    }

    /// <summary>
    /// Get random asteroid prefab from list
    /// </summary>
    private GameObject GetRandomAsteroidPrefab()
    {
        if (asteroidPrefabs != null && asteroidPrefabs.Length > 0)
        {
            List<GameObject> validPrefabs = new List<GameObject>();
            foreach (var prefab in asteroidPrefabs)
            {
                if (prefab != null)
                {
                    validPrefabs.Add(prefab);
                }
            }
            
            if (validPrefabs.Count > 0)
            {
                return validPrefabs[Random.Range(0, validPrefabs.Count)];
            }
        }
        
        return asteroidPrefab;
    }

    /// <summary>
    /// Get random star prefab from list
    /// </summary>
    private GameObject GetRandomStarPrefab()
    {
        if (starPrefabs != null && starPrefabs.Length > 0)
        {
            List<GameObject> validPrefabs = new List<GameObject>();
            foreach (var prefab in starPrefabs)
            {
                if (prefab != null)
                {
                    validPrefabs.Add(prefab);
                }
            }
            
            if (validPrefabs.Count > 0)
            {
                return validPrefabs[Random.Range(0, validPrefabs.Count)];
            }
        }
        
        return starPrefab;
    }

    /// <summary>
    /// Spawn an asteroid at random position on screen edge (random prefab)
    /// </summary>
    private void SpawnAsteroid()
    {
        GameObject prefabToSpawn = GetRandomAsteroidPrefab();
        
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Asteroid prefab is null! Please assign prefab in SpawnManager.");
            return;
        }
        
        Vector3 spawnPosition = GetRandomSpawnPosition();
        
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        
        GameObject asteroid = Instantiate(prefabToSpawn, spawnPosition, randomRotation);
        
        currentAsteroidCount++;
        
        StartCoroutine(WaitForDestroy(asteroid, () => currentAsteroidCount--));
    }

    /// <summary>
    /// Spawn a star at random position INSIDE screen (random prefab)
    /// </summary>
    private void SpawnStar()
    {
        GameObject prefabToSpawn = GetRandomStarPrefab();
        
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Star prefab is null! Please assign prefab in SpawnManager.");
            return;
        }
        
        Vector3 spawnPosition = GetRandomPositionInScreen();
        
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        
        GameObject star = Instantiate(prefabToSpawn, spawnPosition, randomRotation);
        
        currentStarCount++;
        
        StartCoroutine(WaitForDestroy(star, () => currentStarCount--));
    }

    /// <summary>
    /// Get random spawn position INSIDE screen (used for stars)
    /// </summary>
    private Vector3 GetRandomPositionInScreen()
    {
        if (mainCamera == null) return Vector3.zero;
        
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camPos = mainCamera.transform.position;
        
        Vector3 spawnPos = new Vector3(
            Random.Range(camPos.x - camWidth * 0.8f, camPos.x + camWidth * 0.8f),
            Random.Range(camPos.y - camHeight * 0.8f, camPos.y + camHeight * 0.8f),
            0
        );
        
        return spawnPos;
    }

    /// <summary>
    /// Get random spawn position on screen edge
    /// </summary>
    private Vector3 GetRandomSpawnPosition()
    {
        if (mainCamera == null) return Vector3.zero;
        
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;
        Vector3 camPos = mainCamera.transform.position;
        
        int edge = Random.Range(0, 4);
        
        Vector3 spawnPos = Vector3.zero;
        
        switch (edge)
        {
            case 0: // Top
                spawnPos = new Vector3(
                    Random.Range(camPos.x - camWidth + spawnPadding, camPos.x + camWidth - spawnPadding),
                    camPos.y + camHeight + spawnPadding,
                    0
                );
                break;
            case 1: // Right
                spawnPos = new Vector3(
                    camPos.x + camWidth + spawnPadding,
                    Random.Range(camPos.y - camHeight + spawnPadding, camPos.y + camHeight - spawnPadding),
                    0
                );
                break;
            case 2: // Bottom
                spawnPos = new Vector3(
                    Random.Range(camPos.x - camWidth + spawnPadding, camPos.x + camWidth - spawnPadding),
                    camPos.y - camHeight - spawnPadding,
                    0
                );
                break;
            case 3: // Left
                spawnPos = new Vector3(
                    camPos.x - camWidth - spawnPadding,
                    Random.Range(camPos.y - camHeight + spawnPadding, camPos.y + camHeight - spawnPadding),
                    0
                );
                break;
        }
        
        return spawnPos;
    }

    /// <summary>
    /// Wait for object to be destroyed then call callback
    /// </summary>
    private IEnumerator WaitForDestroy(GameObject obj, System.Action onDestroy)
    {
        while (obj != null)
        {
            yield return null;
        }
        onDestroy?.Invoke();
    }
}
