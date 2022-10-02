using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> wasteObjectTypes;
    private List<Vector3> randomPositions;

    private int XWasteFactor = 5;
    private int GoodWasteSize = 20;

    public static List<GameObject> wasteObjects;


    // Start is called before the first frame update
    void Start()
    {
        randomPositions = new List<Vector3>(3);
        wasteObjects = new List<GameObject>();

        List<GameObject> tempWasteList = new List<GameObject>();

        for (int i = 0; i < GoodWasteSize; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                if (j == 3)
                {
                    for (int f = 0; f < XWasteFactor; f++)
                    {
                        tempWasteList.Add(Instantiate(wasteObjectTypes[j]));
                    }
                }
                else
                {
                    tempWasteList.Add(Instantiate(wasteObjectTypes[j]));
                }
            }
        }

        float Xmin = -9f;
        float Xmax = 9f;
        float Ymin = 5.5f;
        float Ymax = 6.5f;
        int count = tempWasteList.Count;


        for (int i = 0; i < count; i++)
        {
            bool posSet = false;
            GameObject temp = tempWasteList[Random.Range(0, tempWasteList.Count)];

            while (!posSet)
            {
                Vector3 pos = new Vector3(Random.Range(Xmin, Xmax), Random.Range(Ymin, Ymax) + (i * GameManager.FallingSpeed / 10), 0);

                bool notOccupied = wasteObjects.TakeLast(10).ToList().TrueForAll(w => Vector3.Distance(w.transform.position, pos) > 1);

                if (notOccupied)
                {
                    temp.transform.SetPositionAndRotation(pos, Quaternion.identity);
                    posSet = true;
                    wasteObjects.Add(temp);
                    tempWasteList.Remove(temp);
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // create random Positions
    // Vector3 CreateRandomPosition()
    // {
    //     return new Vector3(0, 0, 0);
    // }

}
