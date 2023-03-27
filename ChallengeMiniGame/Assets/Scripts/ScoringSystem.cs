using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        // Find the object in the project by its name
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    private void Update()
    {
        scoreText.text = GameHandler.GetScore().ToString();
    }
}
