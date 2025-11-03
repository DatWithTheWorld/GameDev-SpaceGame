using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions
/// Handles smooth transitions between Main Menu, Gameplay, and End Game
/// </summary>
public class SceneTransitionManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private string endGameSceneName = "EndGame";
    
    [Header("Transition Settings")]
    [SerializeField] private float transitionDelay = 1f;

    /// <summary>
    /// Transition to Main Menu scene
    /// </summary>
    public void LoadMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Transition to Gameplay scene (start new game)
    /// </summary>
    public void LoadGameplay()
    {
        LoadScene(gameplaySceneName);
    }

    /// <summary>
    /// Transition to End Game scene
    /// </summary>
    public void LoadEndGame()
    {
        LoadScene(endGameSceneName);
    }

    /// <summary>
    /// Load scene with specific name
    /// </summary>
    /// <param name="sceneName">Name of scene to load</param>
    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty!");
            return;
        }
        
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings! Please add it to File > Build Settings > Scenes In Build");
        }
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
