using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    
    public float FallingSpeed = 5f;

    private void Awake()
    {
        Application.targetFrameRate = 165;
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log("CLICKED " + hit.collider.name);
                SpawnManager.wasteObjects.Remove(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);

                score += hit.collider.gameObject.GetComponent<Waste>().value;
            }
        }

        foreach (var waste in SpawnManager.wasteObjects)
        {
            waste.transform.position = new Vector3(waste.transform.position.x, waste.transform.position.y - (FallingSpeed * Time.deltaTime), waste.transform.position.z);
        }
    }
}