using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    public void StartGame()
    {
        // File > Build Settings > Each scene + index
        SceneManager.LoadScene(1);
        Debug.Log("DEBUG: Load menu");
    }
}
