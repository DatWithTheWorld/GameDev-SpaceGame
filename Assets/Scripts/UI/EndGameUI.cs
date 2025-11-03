using UnityEngine;
using TMPro;

/// <summary>
/// UI Manager for End Game Scene
/// Displays final score and options
/// </summary>
public class EndGameUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    [Header("References")]
    [SerializeField] private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        if (sceneTransitionManager == null)
        {
            sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        }
        
        DisplayFinalScore();
    }

    /// <summary>
    /// Display final score
    /// </summary>
    private void DisplayFinalScore()
    {
        if (GameManager.Instance != null)
        {
            int finalScore = GameManager.Instance.GetCurrentScore();
            
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + finalScore;
            }
            
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (finalScore > highScore)
            {
                highScore = finalScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
            
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    /// <summary>
    /// Called when Play Again button is clicked
    /// </summary>
    public void OnPlayAgainButtonClicked()
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.LoadGameplay();
        }
    }

    /// <summary>
    /// Called when Main Menu button is clicked
    /// </summary>
    public void OnMainMenuButtonClicked()
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.LoadMainMenu();
        }
    }

    /// <summary>
    /// Called when Quit button is clicked
    /// </summary>
    public void OnQuitButtonClicked()
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.QuitGame();
        }
    }
}
