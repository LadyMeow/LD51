using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    private static bool _init = true;

    public Transform MouseLight;

    public Canvas CanvasMain;
    public Canvas CanvasHighscores;
    public Canvas CanvasOptions;

    public GameObject ButtonKeyCircle;
    public GameObject ButtonKeyTriangle;
    public GameObject ButtonKeySquare;
    public GameObject ButtonRestart;
    public Slider SliderVolume;

    // Could be in the Options Script but we need the check for the Escape Key here
    private int _listenForKeyWithType = 0;

    private void Awake()
    {
        Application.targetFrameRate = 165;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Just in case
        if (MouseLight == null) MouseLight = GetComponentInChildren<Light2D>().transform;

        if (_init)
        {
            _init = false;

            CanvasHighscores.gameObject.SetActive(false);
            CanvasOptions.gameObject.SetActive(false);

            CanvasMain.gameObject.SetActive(true);

            GameManager.KeyCircle = (KeyCode)PlayerPrefs.GetInt("KeyCircle", (int)KeyCode.A);
            GameManager.KeyTriangle = (KeyCode)PlayerPrefs.GetInt("KeyTriangle", (int)KeyCode.S);
            GameManager.KeySquare = (KeyCode)PlayerPrefs.GetInt("KeySquare", (int)KeyCode.D);
            GameManager.KeyRestart = (KeyCode)PlayerPrefs.GetInt("KeyRestart", (int)KeyCode.R);
        }
        else
        {
            CanvasMain.gameObject.SetActive(false);
            CanvasOptions.gameObject.SetActive(false);

            CanvasHighscores.gameObject.SetActive(true);
        }

        // Rest after Run
        Cursor.visible = true;

        UpdateOptionsControls();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouseLight();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Block Escape if Control Remapping is active
            if (_listenForKeyWithType > 0)
            {
                _listenForKeyWithType = 0;
            }
            else if (CanvasHighscores.isActiveAndEnabled || CanvasOptions.isActiveAndEnabled)
            {
                ButtonBackClick();
            }
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
    /// 4 ... Restart
    /// </summary>
    /// <param name="type"></param>
    public void StartChangeButton(int type)
    {
        _listenForKeyWithType = type;
    }

    public void UpdateOptionsControls()
    {
        ButtonKeyCircle.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.KeyCircle.ToString();
        ButtonKeyTriangle.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.KeyTriangle.ToString();
        ButtonKeySquare.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.KeySquare.ToString();
        ButtonRestart.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.KeyRestart.ToString();

        SliderVolume.value = AudioManager.Volume;
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
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
                    PlayerPrefs.SetInt("KeyCircle", (int)e.keyCode);
                    break;

                case 2:
                    GameManager.KeyTriangle = e.keyCode; 
                    PlayerPrefs.SetInt("KeyTriangle", (int)e.keyCode);
                    break;

                case 3:
                    GameManager.KeySquare = e.keyCode;
                    PlayerPrefs.SetInt("KeySquare", (int)e.keyCode);
                    break;

                case 4:
                    GameManager.KeyRestart = e.keyCode;
                    PlayerPrefs.SetInt("KeyRestart", (int)e.keyCode);
                    break;

                default:
                    break;
            }

            EventSystem.current.SetSelectedGameObject(null);

            UpdateOptionsControls();

            _listenForKeyWithType = 0;
        }

    }
}
