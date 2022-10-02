using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float FallingSpeed = 5f;
    public static int Score = 0;

    public static KeyCode KeyCircle = KeyCode.A;
    public static KeyCode KeyTriangle = KeyCode.S;
    public static KeyCode KeySquare = KeyCode.D;
    private WasteTypes activeType;
    public enum WasteTypes
    {
        CIRCLE,
        TRIANGLE,
        SQUARE,
        XWASTE
    }

    public Texture2D cursor;
    public Texture2D cursorGlow;

    public TextMeshProUGUI ScoreLabel;


    private float _glowTime = 0f;
    private float _maxGlowTime = 0.08f;

    private TimeManager timer;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0; // Reset Score

        timer = GetComponent<TimeManager>();

        Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);

        UpdateScoreLabel();
    }

    // Update is called once per frame
    private void Update()
    {
        // Run finished
        if (timer.MaxRunTimeInSeconds <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorGlow, new Vector3(92, 92, 0), CursorMode.ForceSoftware);

            // if color selection ?

            List<GameObject> itemsInReach = new List<GameObject>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            foreach (GameObject item in SpawnManager.wasteObjects)
            {
                if (Vector3.Distance(mousePosition, item.transform.position) < 0.7)
                {
                    if (item.GetComponent<Waste>().type == activeType || item.GetComponent<Waste>().type == WasteTypes.XWASTE)
                    {
                        itemsInReach.Add(item);
                    }
                }
            }

            if (itemsInReach != null)
            {
                foreach (GameObject reachedItem in itemsInReach)
                {
                    _glowTime = _maxGlowTime;

                    Score += reachedItem.GetComponent<Waste>().value;

                    UpdateScoreLabel();

                    SpawnManager.wasteObjects.Remove(reachedItem);

                    Destroy(reachedItem);
                }
            }
        }
        else
        {
            Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);
        }

        // KEY STATUS - ACTIVE TYPE
        if (Input.GetKeyDown(KeyCircle))
        {
            activeType = WasteTypes.CIRCLE;
        }
        if (Input.GetKeyDown(KeySquare))
        {
            activeType = WasteTypes.SQUARE;
        }
        if (Input.GetKeyDown(KeyTriangle))
        {
            activeType = WasteTypes.TRIANGLE;
        }

        // MOVEMENT
        foreach (var waste in SpawnManager.wasteObjects)
        {
            waste.transform.position = new Vector3(waste.transform.position.x, waste.transform.position.y - (FallingSpeed * Time.deltaTime), waste.transform.position.z);
        }
    }

    private void UpdateScoreLabel()
    {
        if (ScoreLabel != null) ScoreLabel.text = "Score: " + Score;
    }
}