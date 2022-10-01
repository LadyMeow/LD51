using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeValue = 10;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            // or timeValue += 10;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        } 
        /* nur ohne Milliseconds benutzen!
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }
        */

        float seconds = Mathf.FloorToInt(timeToDisplay);
        float milliseconds = timeToDisplay % 1 * 1000;

        timeText.text = string.Format("{0:00}:{1:000}", seconds, milliseconds);
    }
}
