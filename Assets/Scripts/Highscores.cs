using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    public List<TextMeshProUGUI> LocalHighscoreLabels;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Highscore", 9000);
        PlayerPrefs.SetString("HighscoreName", "Busen");

        // Retrieve Highscore from local cache
        List<int> localHighscoreValues = new List<int>();

        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore", -1));
        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore2", -1));
        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore3", -1));

        List<string> localHighscoreNames = new List<string>();

        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName", ""));
        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName2", ""));
        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName3", ""));

        UpdateLocalHighscores(localHighscoreValues, localHighscoreNames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLocalHighscores(List<int> highscoreValues, List<string> highscoreNames)
    {
        // Clear all local híghscores
        LocalHighscoreLabels.ForEach(l => l.text = "");

        // Check just in case
        if (LocalHighscoreLabels != null)
        {
            for (int i = 0; i < highscoreValues.Count; i++)
            {
                if (LocalHighscoreLabels.Count > i && highscoreValues[i] != -1 && highscoreNames[i] != "")
                {
                    int nameLength = highscoreNames[i].Length;
                    int valueLength = highscoreValues[i].ToString().Length;

                    // Minimum of one space
                    LocalHighscoreLabels[i].text = highscoreNames[i] + new string(' ', Mathf.Max(1, 40 - nameLength - valueLength)) + highscoreValues[i];
                }
            }
        }
    }

    public static void StoreLocalHighscore(int localHighscore)
    {

    }
}
