using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float FallingSpeed = 5f;

    public static KeyCode KeyCircle = KeyCode.A;
    public static KeyCode KeyTriangle = KeyCode.S;
    public static KeyCode KeySquare = KeyCode.D;

    public Texture2D cursor;
    public Texture2D cursorGlow;

    public TextMeshProUGUI ScoreLabel;

    private int _score = 0;
    private float _glowTime = 0f;
    private float _maxGlowTime = 0.08f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);
        UpdateScoreLabel();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // if color selection ?
            List<GameObject> itemsInReach = new List<GameObject>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            foreach (GameObject item in SpawnManager.wasteObjects)
            {
                if (Vector3.Distance(mousePosition, item.transform.position) < 0.7)
                {
                    itemsInReach.Add(item);
                }
            }

            if (itemsInReach != null)
            {
                foreach (GameObject reachedItem in itemsInReach)
                {
                    Cursor.SetCursor(cursorGlow, new Vector3(92, 92, 0), CursorMode.ForceSoftware);

                    _glowTime = _maxGlowTime;

                    _score += reachedItem.GetComponent<Waste>().value;

                    UpdateScoreLabel();

                    SpawnManager.wasteObjects.Remove(reachedItem);

                    Destroy(reachedItem);
                }
            }
        }

        if (_glowTime > 0) _glowTime -= Time.deltaTime;
        else if (_glowTime <= 0) Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);

        // MOVEMENT
        foreach (var waste in SpawnManager.wasteObjects)
        {
            waste.transform.position = new Vector3(waste.transform.position.x, waste.transform.position.y - (FallingSpeed * Time.deltaTime), waste.transform.position.z);
        }
    }

    private void UpdateScoreLabel()
    {
        if (ScoreLabel != null) ScoreLabel.text = "Score: " + _score;
    }
}