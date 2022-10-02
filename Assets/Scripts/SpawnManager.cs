using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> wasteObjectTypes;
    private List<Vector3> randomPositions;

    private int XWasteRatio = 3;
    private int GoodWasteSize = 10;

    public static List<GameObject> wasteObjects;


    public int width = 100;
    public int length = 0; // z
    public int height = 100;
    public int step = 2;
    public int randomFactor = 3;

    // Start is called before the first frame update
    void Start()
    {
        randomPositions = new List<Vector3>(3);
        wasteObjects = new List<GameObject>();

        // wasteObjects.Add(Instantiate(wasteObjectTypes[3], CreateRandomPosition(), Quaternion.identity));

        for (int h = 0; h < length / step; h++)
        {
            for (int i = 0; i < width / step; i++)
            {
                int canSpawn = Random.Range(0, randomFactor);
                if (canSpawn == 0)
                {
                    wasteObjects.Add(Instantiate(wasteObjectTypes[0], new Vector3(((i * step) + transform.position.x) - (width / 2), transform.position.y + (Random.Range(-(height / 2), (height / 2))), ((h * step) + transform.position.z) - (length / 2)), Quaternion.identity));
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // instantiate

    // create random Positions
    // Vector3 CreateRandomPosition()
    // {
    //     return new Vector3(0, 0, 0);
    // }

}
