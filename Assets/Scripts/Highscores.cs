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
        UpdateLocalHighscores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLocalHighscores()
    {
        // Retrieve Highscore from local cache
        List<int> localHighscoreValues = new List<int>();

        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore", -1));
        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore2", -1));
        localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore3", -1));

        List<string> localHighscoreNames = new List<string>();

        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName", ""));
        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName2", ""));
        localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName3", ""));

        // Clear all local híghscores
        LocalHighscoreLabels.ForEach(l => l.text = "");

        // Check just in case
        if (LocalHighscoreLabels != null)
        {
            for (int i = 0; i < localHighscoreValues.Count; i++)
            {
                if (LocalHighscoreLabels.Count > i && localHighscoreValues[i] != -1 && localHighscoreNames[i] != "")
                {
                    int nameLength = localHighscoreNames[i].Length;
                    int valueLength = localHighscoreValues[i].ToString().Length;

                    // Minimum of one space
                    LocalHighscoreLabels[i].text = localHighscoreNames[i] + new string(' ', Mathf.Max(1, 40 - nameLength - valueLength)) + localHighscoreValues[i];
                }
            }
        }
    }

    public static void StoreLocalHighscore(int localHighscore)
    {
        if (localHighscore > PlayerPrefs.GetInt("Highscore", -1)) PlayerPrefs.SetInt("Highscore", localHighscore);
        else if (localHighscore > PlayerPrefs.GetInt("Highscore1", -1)) PlayerPrefs.SetInt("Highscore1", localHighscore);
        else if (localHighscore > PlayerPrefs.GetInt("Highscore2", -1)) PlayerPrefs.SetInt("Highscore2", localHighscore);
    }
}
