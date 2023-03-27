using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPosition
{
    private Vector2Int position;

    public Vector2Int RandomizePosition(Collider2D gridArea)
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the grid boundaries
        float x = Random.Range(0, bounds.max.x);
        float y = Random.Range(0, bounds.max.y);

        // NOTE: it's better to round the value
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        // Give it the actual position
        position = new Vector2Int((int)x, (int)y);
        Debug.Log("DEBUG: randomized position: " + position);

        return position;
    }
}
