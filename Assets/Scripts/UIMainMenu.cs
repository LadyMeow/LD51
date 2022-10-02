using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public Transform MouseLight;

    public Canvas CanvasMain;
    public Canvas CanvasHighscores;
    public Canvas CanvasOptions;

    // Start is called before the first frame update
    void Start()
    {
        // Just in case
        if (MouseLight == null) MouseLight = GetComponentInChildren<Light2D>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouseLight();
    }

    void MoveMouseLight()
    {
        // Camera and Mouse are in Screen Space and we need to convert the position to World Space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        MouseLight.position = new Vector3(mousePosition.x, mousePosition.y, MouseLight.position.z);
    }

    public void ButtonNewGameClick()
    {
        // Go to next scene in Build Order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Start Button here!");
    }

    public void ButtonHighscoresClick()
    {

    }

    public void ButtonOptionsClick()
    {

    }

    public void ButtonQuitClick()
    {
        Application.Quit();
    }

    public void ButtonHighscoreBackClick()
    {

    }
}
