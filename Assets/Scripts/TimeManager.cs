using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static float MaxRunTimeInSeconds = 10;

    public TextMeshProUGUI TimeText;
    public Slider ProgressBar;

    public bool ActiveRun = false;
    private float _currentRunTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.maxValue = MaxRunTimeInSeconds;
        _currentRunTime = MaxRunTimeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveRun)
        {
            if (_currentRunTime > 0)
            {
                _currentRunTime -= Time.deltaTime;
                ProgressBar.value = ProgressBar.maxValue - _currentRunTime;
            }
            else
            {
                ActiveRun = false;
            }
        }
    }

    public void StartRun()
    {
        ActiveRun = true;
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

        //timeText.text = string.Format("{0:00}:{1:000}", seconds, milliseconds);
    }
}
