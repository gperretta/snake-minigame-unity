using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler gameHandler;
    private static int score;

    private LevelGrid levelGrid;
    // Boundaries for food spawning (not too close to the borders)
    public Collider2D gridArea;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DEBUG: start!");

        // Calling the LevelGrid script to manage the grid
        levelGrid = new LevelGrid();

        // DELAY
        Invoke(nameof(FirstFoodSpawn), 0.5f);

        score = 0;
    }

    void FirstFoodSpawn()
    {
        // The food will be instantiated at runtime - no need to create the game object 
        levelGrid.SpawnFood(gridArea);
    }

    // Get method
    public static int GetScore()
    {
        return score;
    }
    // Increase the score of 1 unit
    public static void IncreaseScore()
    {
        score += 1;
        Debug.Log("DEBUG: Score increased at " + score);
    }
}
