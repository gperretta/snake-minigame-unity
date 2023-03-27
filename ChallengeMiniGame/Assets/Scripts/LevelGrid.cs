using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LevelGrid
{
    // Calling the FoodPosition.cs script (randomized positions for food spawning)
    private FoodPosition foodPosition;
    private GameObject foodObject;
    private Vector2Int foodGridPosition;
    private BoxCollider2D foodCollider;
    // Collider dimension(s)
    private float colliderWidth = 0.79f;
    private float colliderHeight = 0.89f;


    // Spawn food in random position(s)
    public void SpawnFood(Collider2D gridArea)
    {
        // To spawn the apple in random position(s)
        foodPosition = new FoodPosition();

        // Instantiate the apple object at runtime (w/ the sprite renderer component)
        foodObject = new GameObject("Food", typeof(SpriteRenderer));
        foodObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;

        // Add a tag [to check for collision]
        foodObject.tag = "Food";

        // Add Box Collider 2D component [necessary for collision]
        BoxCollider2D foodCollider = foodObject.AddComponent<BoxCollider2D>();
        foodCollider.size = new Vector2(colliderWidth, colliderHeight);
        // NOTE: isTrigger must be true for the collision event to be triggered
        foodCollider.isTrigger = true;

        // Get the random position (FoodPosition.cs script)
        foodGridPosition = foodPosition.RandomizePosition(gridArea);
        // and assign it to the game object
        foodObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        //Debug.Log("DEBUG: food position: " + applePosition.RandomizePosition(gridArea));
    }

}
