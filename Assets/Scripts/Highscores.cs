using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    const string privateCode = "zc4hDLkjnkqJQu2jgliz_AJUTNhmKnNU6hdBhyZaw80A";  //Key to Upload New Info
    const string publicCode = "63399bad8f40bc0fe88fdb6c";   //Key to download
    const string webURL = "http://dreamlo.com/lb/"; //  Website the keys are for

    public List<NameValueLabels> LocalHighscoreLabels;

    public GameObject NewHighscoreArea;
    public TextMeshProUGUI NewHighScoreValueLabel;

    public GameObject GlobalLeaderboard;
    public GameObject GlobalLeaderboardContent;
    public GameObject HighscorePrefab;

    private List<int> _localHighscoreValues = new List<int>();
    private List<string> _localHighscoreNames = new List<string>();

    private List<PlayerScore> _scoreList;

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
        DownloadAndUpdateGlobalHighScoresAsync();

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

    public void UpdateGlobalHighscores()
    {

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
            StartCoroutine(DatabaseUpload(input.text, GameManager.Score));

            NewHighscoreArea.SetActive(false);
        }
    }

    IEnumerator DatabaseUpload(string userame, int score) //Called when sending new score to Website
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(userame) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadAndUpdateGlobalHighScoresAsync();
        }
        else
        {
            print("Error uploading" + www.error);
        }
    }

    public void DownloadAndUpdateGlobalHighScoresAsync()
    {
        StartCoroutine(DatabaseDownload());
    }

    IEnumerator DatabaseDownload()
    {
        //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list  - "/pipe/0/10" //Gets top 10
        UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/"); 
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            OrganizeInfo(www.downloadHandler.text);
            UpdateGlobalHighscores();
        }
        else
        {
            print("Error uploading" + www.error);
        }
    }

    void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
    {
        string[] entries = rawData.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _scoreList.Clear();
        for (int i = 0; i < entries.Length; i++) //For each entry in the string array
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            _scoreList.Add(new PlayerScore(username, score));
            print(_scoreList[i].Name + ": " + _scoreList[i].Score);
        }
    }

    public struct PlayerScore //Creates place to store the variables for the name and score of each player
    {
        public string Name;
        public int Score;

        public PlayerScore(string _username, int _score)
        {
            Name = _username;
            Score = _score;
        }
    }
}
