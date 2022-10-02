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

    private int _listenForKeyWithType = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Just in case
        if (MouseLight == null) MouseLight = GetComponentInChildren<Light2D>().transform;

        CanvasHighscores.gameObject.SetActive(false);
        CanvasOptions.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouseLight();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_listenForKeyWithType > 0) _listenForKeyWithType = 0;
            else if (CanvasHighscores.isActiveAndEnabled || CanvasOptions.isActiveAndEnabled) ButtonBackClick();
        }
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
        CanvasMain.gameObject.SetActive(false);
        CanvasOptions.gameObject.SetActive(false);

        CanvasHighscores.gameObject.SetActive(true);
    }

    public void ButtonOptionsClick()
    {
        CanvasMain.gameObject.SetActive(false);
        CanvasHighscores.gameObject.SetActive(false);

        CanvasOptions.gameObject.SetActive(true);
    }

    public void ButtonQuitClick()
    {
        Application.Quit();
    }

    public void ButtonBackClick()
    {
        CanvasHighscores.gameObject.SetActive(false);
        CanvasOptions.gameObject.SetActive(false);

        CanvasMain.gameObject.SetActive(true);
    }

    /// <summary>
    /// 1 ... Circle
    /// 2 ... Triangle
    /// 3 ... Square
    /// </summary>
    /// <param name="type"></param>
    public void StartChangeButton(int type)
    {
        _listenForKeyWithType = type;
    }

    public void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey && _listenForKeyWithType > 0)
        {
            switch (_listenForKeyWithType)
            {
                case 1:
                    GameManager.KeyCircle = e.keyCode;
                    break;

                case 2:
                    GameManager.KeySquare = e.keyCode;
                    break;

                case 3:
                    GameManager.KeyTriangle = e.keyCode;
                    break;

                default:
                    break;
            }

            Debug.Log("Key set for " + _listenForKeyWithType + " to: " + e.keyCode);

            _listenForKeyWithType = 0;
        }

    }
}
