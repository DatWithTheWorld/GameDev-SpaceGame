using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// GameManager manages game state, score, and game over
/// Uses Singleton pattern for easy access from anywhere
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private bool isGameActive = false;
    
    [Header("Score Settings")]
    private int currentScore = 0;
    [SerializeField] private int scorePenaltyOnAsteroidHit = -5;
    [SerializeField] private bool applyScorePenaltyOnHit = true;
    
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI gameOverScoreText;
    
    public System.Action<int> OnScoreChanged;
    public System.Action OnGameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        StartGame();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Handles when scene is loaded (after restart)
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {
            StartCoroutine(DelayedStartGame());
        }
    }

    /// <summary>
    /// Coroutine to delay finding UI references and starting game
    /// </summary>
    private System.Collections.IEnumerator DelayedStartGame()
    {
        yield return null;
        
        FindUIReferences();
        
        StartGame();
    }

    /// <summary>
    /// Find UI references again if lost after scene reload
    /// </summary>
    private void FindUIReferences()
    {
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log("Found ScoreText reference");
            }
            else
            {
                Debug.LogWarning("ScoreText not found in scene!");
            }
        }

        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel");
            if (gameOverPanel != null)
            {
                Debug.Log("Found GameOverPanel reference");
            }
            else
            {
                Debug.LogWarning("GameOverPanel not found in scene!");
            }
        }

        if (gameOverScoreText == null)
        {
            GameObject gameOverScoreObj = GameObject.Find("GameOverScoreText");
            if (gameOverScoreObj != null)
            {
                gameOverScoreText = gameOverScoreObj.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log("Found GameOverScoreText reference");
            }
            else
            {
                Debug.LogWarning("GameOverScoreText not found in scene!");
            }
        }

        SetupButtonEvents();
    }

    /// <summary>
    /// Setup button OnClick events after scene reload
    /// </summary>
    private void SetupButtonEvents()
    {
        GameObject restartButtonObj = GameObject.Find("RestartButton");
        if (restartButtonObj != null)
        {
            UnityEngine.UI.Button restartButton = restartButtonObj.GetComponent<UnityEngine.UI.Button>();
            if (restartButton != null)
            {
                restartButton.onClick.RemoveAllListeners();
                restartButton.onClick.AddListener(RestartGame);
                Debug.Log("Setup RestartButton OnClick event");
            }
        }
        else
        {
            Debug.LogWarning("RestartButton not found in scene!");
        }

        GameObject mainMenuButtonObj = GameObject.Find("MainMenuButton");
        if (mainMenuButtonObj != null)
        {
            UnityEngine.UI.Button mainMenuButton = mainMenuButtonObj.GetComponent<UnityEngine.UI.Button>();
            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.RemoveAllListeners();
                mainMenuButton.onClick.AddListener(ReturnToMainMenu);
                Debug.Log("Setup MainMenuButton OnClick event");
            }
        }
        else
        {
            Debug.LogWarning("MainMenuButton not found in scene!");
        }
    }

    /// <summary>
    /// Start new game
    /// </summary>
    public void StartGame()
    {
        isGameActive = true;
        currentScore = 0;
        UpdateScoreUI();
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Add score when collecting star
    /// </summary>
    /// <param name="points">Points to add</param>
    public void AddScore(int points)
    {
        if (!isGameActive) return;
        
        currentScore += points;
        
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        
        UpdateScoreUI();
        OnScoreChanged?.Invoke(currentScore);
    }

    /// <summary>
    /// Handle when player collides with asteroid
    /// </summary>
    public void PlayerHit()
    {
        if (!isGameActive) return;
        
        if (applyScorePenaltyOnHit && scorePenaltyOnAsteroidHit < 0)
        {
            AddScore(scorePenaltyOnAsteroidHit);
        }
        
        GameOver();
    }

    /// <summary>
    /// Handle when asteroid is destroyed (can add points if desired)
    /// </summary>
    public void AsteroidDestroyed()
    {
    }

    /// <summary>
    /// End game
    /// </summary>
    public void GameOver()
    {
        if (!isGameActive) return;
        
        isGameActive = false;
        OnGameOver?.Invoke();
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            
            if (gameOverScoreText != null)
            {
                gameOverScoreText.text = "Final Score: " + currentScore;
            }
        }
    }

    /// <summary>
    /// Update score UI
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    /// <summary>
    /// Get current score
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Check if game is active
    /// </summary>
    public bool IsGameActive()
    {
        return isGameActive;
    }

    /// <summary>
    /// Reload gameplay scene
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("RestartGame called");
        
        Time.timeScale = 1f;
        
        isGameActive = false;
        
        StopAllCoroutines();
        
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Loading scene: " + sceneName);
        
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu called");
        
        Time.timeScale = 1f;
        
        StopAllCoroutines();
        
        string sceneName = "MainMenu";
        
        bool sceneExists = false;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                sceneExists = true;
                break;
            }
        }
        
        if (!sceneExists)
        {
            Debug.LogError("Scene 'MainMenu' not found in Build Settings! Please add it via File > Build Settings.");
            Debug.LogError("To fix: File > Build Settings > Add Open Scenes (after opening MainMenu scene)");
            return;
        }
        
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
