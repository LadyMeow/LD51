using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> wasteObjectTypes;
    private List<Vector3> randomPositions;
    public float SpawnSpeed = 1f;

    public static List<GameObject> wasteObjects;

    // Start is called before the first frame update
    void Start()
    {
        randomPositions = new List<Vector3>(3);
        wasteObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.timeValue > 0)
        {
            if (SpawnSpeed > 0)
            {
                SpawnSpeed -= Time.deltaTime;
            }
            else if (SpawnSpeed <= 0)
            {
                wasteObjects.Add(Instantiate(wasteObjectTypes[0], CreateRandomPosition(), Quaternion.identity));
                SpawnSpeed = 1;
            }
        }
    }

    // instantiate

    // create random Postionsa
    Vector3 CreateRandomPosition()
    {
        List<int> xPositions = new List<int> { -10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10 };

        int x = xPositions[Random.Range(0, 10)];

        while (randomPositions.Any(p => p.x == x))
        {
            x = xPositions[Random.Range(0, 10)];
        }

        float y = 5.5f;
        int z = 0;

        Vector3 pos = new Vector3(x, y, z);

        if (randomPositions.Count == 3)
        {
            randomPositions.RemoveAt(0);
        }
        randomPositions.Add(pos);

        return pos;
    }

}
