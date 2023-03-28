using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    //private Text scoreText;
    private TextMeshProUGUI scoreNum;

    private void Awake()
    {
        // Find the object in the project by its name
        //scoreText = transform.Find("ScoreText").GetComponent<Text>();
        scoreNum = transform.Find("ScoreNum").GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        //scoreText.text = GameHandler.GetScore().ToString();
        scoreNum.text = GameHandler.GetScore().ToString();
    }
}
