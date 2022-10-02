using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    public List<TextMeshProUGUI> LocalHighscoreLabels;

    public GameObject NewHighscoreArea;

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

        if (GameManager.Score > 0 && !_localHighscoreValues.TrueForAll(s => GameManager.Score < s))
        {
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
        _localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore2", -1));
        _localHighscoreValues.Add(PlayerPrefs.GetInt("Highscore3", -1));

        _localHighscoreNames.Clear();
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName", ""));
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName2", ""));
        _localHighscoreNames.Add(PlayerPrefs.GetString("HighscoreName3", ""));

        // Clear all local híghscores
        LocalHighscoreLabels.ForEach(l => l.text = "");

        // Check just in case
        if (LocalHighscoreLabels != null)
        {
            for (int i = 0; i < _localHighscoreValues.Count; i++)
            {
                if (LocalHighscoreLabels.Count > i && _localHighscoreValues[i] != -1 && _localHighscoreNames[i] != "")
                {
                    int nameLength = _localHighscoreNames[i].Length;
                    int valueLength = _localHighscoreValues[i].ToString().Length;

                    // Minimum of one space
                    LocalHighscoreLabels[i].text = _localHighscoreNames[i] + new string(' ', Mathf.Max(1, 40 - nameLength - valueLength)) + _localHighscoreValues[i];
                }
            }
        }
    }

    public void StoreLocalHighscore(int localHighscore, string name)
    {
        if (localHighscore > _localHighscoreValues[0])
        {
            PlayerPrefs.SetInt("Highscore", localHighscore);
            PlayerPrefs.SetString("HighscoreName", name);
        }
        else if (localHighscore > _localHighscoreValues[1])
        {
            PlayerPrefs.SetInt("Highscore1", localHighscore);
            PlayerPrefs.SetString("HighscoreName1", name);
        }
        else if (localHighscore > _localHighscoreValues[2])
        {
            PlayerPrefs.SetInt("Highscore2", localHighscore);
            PlayerPrefs.SetString("HighscoreNam2", name);
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
