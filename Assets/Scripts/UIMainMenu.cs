using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIMainMenu : MonoBehaviour
{
    public Transform MouseLight;

    // Start is called before the first frame update
    void Start()
    {
        if (MouseLight == null) MouseLight = GetComponentInChildren<Light2D>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouseLight();
    }

    void MoveMouseLight()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        MouseLight.position = new Vector3(mousePosition.x, mousePosition.y, MouseLight.position.z);
    }

    public void ButtonStartClick()
    {
        Debug.Log("Start Button here!");
    }
}
