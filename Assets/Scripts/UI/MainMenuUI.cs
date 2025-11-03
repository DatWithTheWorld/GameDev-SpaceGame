using UnityEngine;

/// <summary>
/// UI Manager for Main Menu Scene
/// Handles buttons in main menu
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject instructionsPanel;
    
    [Header("References")]
    [SerializeField] private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        if (sceneTransitionManager == null)
        {
            sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        }
        
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Called when Play button is clicked
    /// </summary>
    public void OnPlayButtonClicked()
    {
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.LoadGameplay();
        }
        else
        {
            Debug.LogError("SceneTransitionManager not found!");
        }
    }

    /// <summary>
    /// Called when Instructions button is clicked
    /// </summary>
    public void OnInstructionsButtonClicked()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
        }
    }

    /// <summary>
    /// Called when Close Instructions button is clicked
    /// </summary>
    public void OnCloseInstructionsButtonClicked()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
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
        else
        {
            Debug.LogError("SceneTransitionManager not found!");
        }
    }
}
