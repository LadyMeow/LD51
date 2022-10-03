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

    public static Dictionary<WasteTypes, Color> WasteColors = new Dictionary<WasteTypes, Color>() { { WasteTypes.CIRCLE, new Color(0.071f, 0.651f, 0.988f, 1) },
                                                                                                    { WasteTypes.TRIANGLE, new Color(0.325f, 1, 0.294f, 1) },
                                                                                                    { WasteTypes.SQUARE, new Color(1, 0, 0.8f, 1) }};

    private Dictionary<WasteTypes, Color> WasteColorsLowGloom = new Dictionary<WasteTypes, Color>();

    public enum WasteTypes
    {
        NONE,
        CIRCLE,
        TRIANGLE,
        SQUARE,
        XWASTE
    }

    public SpriteRenderer CursorSprite;
    public TextMeshProUGUI ScoreLabel;

    private TimeManager _timer;
    private WasteTypes _activeType;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0; // Reset Score

        _timer = GetComponent<TimeManager>();

        Color.RGBToHSV(WasteColors[WasteTypes.CIRCLE], out float h, out float s, out float v);
        WasteColorsLowGloom[WasteTypes.CIRCLE] = Color.HSVToRGB(h, s, v * 0.75f);

        Color.RGBToHSV(WasteColors[WasteTypes.TRIANGLE], out h, out s, out v);
        WasteColorsLowGloom[WasteTypes.TRIANGLE] = Color.HSVToRGB(h, s, v * 0.75f);

        Color.RGBToHSV(WasteColors[WasteTypes.SQUARE], out h, out s, out v);
        WasteColorsLowGloom[WasteTypes.SQUARE] = Color.HSVToRGB(h, s, v * 0.75f);

        UpdateScoreLabel();
    }

    // Update is called once per frame
    private void Update()
    {
        // Run finished
        if (_timer.MaxRunTimeInSeconds <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        // Move Cursor Sprite to 
        // Camera and Mouse are in Screen Space and we need to convert the position to World Space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        CursorSprite.transform.position = new Vector3(mousePosition.x, mousePosition.y, CursorSprite.transform.position.z);


        if (Input.GetKeyDown(KeyCircle)) _activeType = WasteTypes.CIRCLE;
        else if (Input.GetKeyDown(KeyTriangle)) _activeType = WasteTypes.TRIANGLE;
        else if (Input.GetKeyDown(KeySquare)) _activeType = WasteTypes.SQUARE;

        if ((_activeType == WasteTypes.CIRCLE && Input.GetKey(KeyCircle)) ||
            (_activeType == WasteTypes.TRIANGLE && Input.GetKey(KeyTriangle)) ||
            (_activeType == WasteTypes.SQUARE && Input.GetKey(KeySquare)))
        {
            // Update Cursor
            CursorSprite.color = WasteColors[_activeType];

            List<GameObject> itemsInReach = new List<GameObject>();

            foreach (GameObject item in SpawnManager.wasteObjects)
            {
                if (Vector3.Distance(mousePosition, item.transform.position) < 0.7)
                {
                    if (item.GetComponent<Waste>().type == _activeType || item.GetComponent<Waste>().type == WasteTypes.XWASTE)
                    {
                        itemsInReach.Add(item);
                    }
                }
            }

            if (itemsInReach != null)
            {
                foreach (GameObject reachedItem in itemsInReach)
                {
                    Score += reachedItem.GetComponent<Waste>().value;

                    UpdateScoreLabel();

                    SpawnManager.wasteObjects.Remove(reachedItem);

                    Destroy(reachedItem);
                }
            }
        }
        else
        {
            // Reset
            CursorSprite.color = new Color(0.8f, 0.8f, 0.8f);
            _activeType = WasteTypes.NONE;
        }

 
        // MOVE all Waste objects
        foreach (var waste in SpawnManager.wasteObjects)
        {
            waste.transform.position = new Vector3(waste.transform.position.x, waste.transform.position.y - (FallingSpeed * Time.deltaTime), waste.transform.position.z);
        }
    }

    private void UpdateActiveWasteIcon()
    {

    }

    private void UpdateScoreLabel()
    {
        if (ScoreLabel != null) ScoreLabel.text = "Score: " + Score;
    }
}