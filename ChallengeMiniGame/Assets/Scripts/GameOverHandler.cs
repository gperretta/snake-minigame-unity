using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{

    public GameObject gameOverMenu;

    // Subscribing to the system event (for snake death)
    private void OnEnable()
    {
        SnakeControl.onSnakeDeath += EnableGameOverMenu;
    }
    // and unsubscribing [both necessary when dealing w/ system events]
    private void OnDisable()
    {
        SnakeControl.onSnakeDeath -= EnableGameOverMenu;
    }

    // To display the menu, it needs to be Active
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Debug.Log("DEBUG: Game over displayed.");
    }

    // Handle Retry Button
    public void RestartGame()
    {
        // File > Build Settings > Each scene + index
        SceneManager.LoadScene(1);
        Debug.Log("DEBUG: Load scene");
    }
}
