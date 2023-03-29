using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    private TextMeshProUGUI speedValue;
    private float speed;

    private void Awake()
    {
        // Find the object in the project by its name
        speedValue = transform.Find("SpeedValue").GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        speed = SnakeControl.GetSpeed();
        if (speed == 9f) {

            speedValue.text = "II";
        } else if (speed == 11f) {

            speedValue.text = "III";
        } else if (speed == 13f) {

            speedValue.text = "IIII";
        } else if (speed == 15f) {

           speedValue.text = "IIIII";
        }
    }

}
