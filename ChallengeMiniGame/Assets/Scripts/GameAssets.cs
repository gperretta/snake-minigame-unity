using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
To run this script before any other:
Edit > Project Settings > Script Execution Order > Drag & drop the script before Default Time
**/

public class GameAssets : MonoBehaviour
{
    // To access class' public fields from outside
    // using a STATIC reference
    public static GameAssets instance;
    private void Awake() 
    {
        instance = this;
    }
    // Fields:
    public Sprite snakeHeadSprite;
    public Sprite snakebodySprite;
    public Sprite foodSprite;
    // NOTE: useful to create game objects (w/ sprite renderer component) dynamically
}


