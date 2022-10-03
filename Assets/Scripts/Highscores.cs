using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    public List<NameValueLabels> LocalHighscoreLabels;

    public GameObject NewHighscoreArea;
    public TextMeshProUGUI NewHighScoreValueLabel;

    private List<int> _localHighscoreValues = new List<int>();
    private List<string> _localHighscoreNames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpdateLocalHighscores();

        if (GameManager.Score > 0 && GameManager.Score > _localHighscoreValues.FirstOrDefault())
        {
            NewHighScoreValueLabel.text = GameManager.Score.ToString();
            NewHighscoreArea.SetActive(true);
        }
        else
        {
            NewHighscoreArea.SetActive(false);
        }
    }

    public void UpdateLocalHighscores()
    {
        // Retrieve Highscore from local cache
        _localHighscoreValues.Clear();
        _localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore", -1));
        _localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore1", -1));
        _localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore2", -1));

        _localHighscoreNames.Clear();
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName", ""));
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName1", ""));
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName2", ""));


        // Check just in case
        if (LocalHighscoreLabels != null)
        {
            for (int i = 0; i < LocalHighscoreLabels.Count; i++)
            {
                if (_localHighscoreValues[i] != -1 && _localHighscoreNames[i] != "")
                {
                    LocalHighscoreLabels[i].UpdateLables(_localHighscoreNames[i], _localHighscoreValues[i].ToString());
                }
                else
                {
                    // Clear Highscore
                    LocalHighscoreLabels[i].UpdateLables("", "");
                }
            }
        }
    }

    public void StoreLocalHighscore(int localHighscore, string name)
    {
        if (localHighscore > _localHighscoreValues[0])
        {
            // Move old highscores
            PlayerPrefs.SetInt("Highscore2", _localHighscoreValues[1]);
            PlayerPrefs.SetString("HighscoreName2", _localHighscoreNames[1]);

            PlayerPrefs.SetInt("Highscore1", _localHighscoreValues[0]);
            PlayerPrefs.SetString("HighscoreName1", _localHighscoreNames[0]);

            // Set new highscore
            PlayerPrefs.SetInt("Highscore", localHighscore);
            PlayerPrefs.SetString("HighscoreName", name);
        }
        else if (localHighscore > _localHighscoreValues[1])
        {
            // Move old highscores
            PlayerPrefs.SetInt("Highscore2", _localHighscoreValues[1]);
            PlayerPrefs.SetString("HighscoreName2", _localHighscoreNames[1]);

            // Set new highscore
            PlayerPrefs.SetInt("Highscore1", localHighscore);
            PlayerPrefs.SetString("HighscoreName1", name);
        }
        else if (localHighscore > _localHighscoreValues[2])
        {
            // Set new highscore
            PlayerPrefs.SetInt("Highscore2", localHighscore);
            PlayerPrefs.SetString("HighscoreName2", name);
        }
    }

    public void NewHighscoreSubmitClick()
    {
        TMP_InputField input = NewHighscoreArea.GetComponentInChildren<TMP_InputField>();

        if (input != null)
        {
            StoreLocalHighscore(GameManager.Score, input.text);
            GameManager.Score = 0;
            UpdateLocalHighscores();

            NewHighscoreArea.SetActive(false);
        }
    }
}
