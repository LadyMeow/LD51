using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;

    public static float FallingSpeed = 5f;

    public static KeyCode KeySquare = KeyCode.S;
    public static KeyCode KeyTriangle = KeyCode.D;
    public static KeyCode KeyCircle = KeyCode.A;

    public Texture2D cursor;
    public Texture2D cursorGlow;
    private float glowTime = 0f;

    private void Awake()
    {
        Application.targetFrameRate = 165;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //    if (hit.collider != null)
        //    {
        //        Debug.Log("CLICKED " + hit.collider.name);
        //        SpawnManager.wasteObjects.Remove(hit.collider.gameObject);
        //        Destroy(hit.collider.gameObject);

        //        score += hit.collider.gameObject.GetComponent<Waste>().value;
        //        Debug.Log(score);
        //    }
        //}

        // COLLECT ON MOUSE BUTTON
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
                    glowTime = 0.08f;
                    score += reachedItem.GetComponent<Waste>().value;
                    Debug.Log(score);

                    SpawnManager.wasteObjects.Remove(reachedItem);

                    // add Animation ?
                    Destroy(reachedItem);
                }
            }
        }

        if (glowTime > 0) glowTime -= Time.deltaTime;
        else if (glowTime <= 0) Cursor.SetCursor(cursor, new Vector3(64, 64, 0), CursorMode.ForceSoftware);

        // MOVEMENT
        foreach (var waste in SpawnManager.wasteObjects)
        {
            waste.transform.position = new Vector3(waste.transform.position.x, waste.transform.position.y - (FallingSpeed * Time.deltaTime), waste.transform.position.z);
        }
    }
}