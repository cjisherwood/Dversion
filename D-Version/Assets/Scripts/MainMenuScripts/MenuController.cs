using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject exitMenu;     //Panel in which all of the exit menu items are present
    public GameObject optionsMenu;  //Panel in which all of the exit menu items are present
    public new GameObject camera;   //Getting access to main camera for options animation purposes
    
    //Declaration of all three buttons in which the main menu consists
    public Button playButton, optionsButton, exitButton;

    public Text difficulty, resolution;        //Variable used to access the text component of the difficulty and resolution buttons

    public Toggle fullScreenMode;  //Declaring the toggle used to change screen size

    public bool easy, medium, hard; //Boolean values used to decide the game's difficulty

    Animator animator;              //Animator used to animate the zoom in motion for the options menu

    void Start()
    {
        animator = camera.GetComponent<Animator>();

        exitMenu.SetActive(false);
        optionsMenu.SetActive(false);

        easy = true;
        medium = hard = false;

        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (difficulty.isActiveAndEnabled)
                BackButton();
        }
    }

    public void LoadScene(string sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    #region Options Methods
    public void LoadOptions()
    {
        CameraZoom("ZoomIn");

        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        optionsMenu.SetActive(true);
    }
    public void FullScreen()
    {
        if (fullScreenMode.isOn)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;

    }

    public void BackButton()
    {
        CameraZoom("ZoomOut");

        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

        optionsMenu.SetActive(false);
    }
    #endregion Options Methods

    #region Inside Options Difficulty
    public void EasyButton()
    {
        difficulty.text = "Easy";

        easy = true;
        medium = hard = false;
    }

    public void MediumButton()
    {
        difficulty.text = "Medium";

        medium = true;
        easy = hard = false;
    }

    public void HardButton()
    {
        difficulty.text = "Hard";

        hard = true;
        easy = medium = false;
    }
    #endregion

    #region Inside Options Resolution
    public void ThreeToTwoRes()
    {
        resolution.text = "3:2";
        Screen.SetResolution(1080, 720, fullScreenMode.isOn);
    }

    public void FourToThreeRes()
    {
        resolution.text = "4:3";
        Screen.SetResolution(1024, 768, fullScreenMode.isOn);
    }

    public void FiveToThreeRes()
    {
        resolution.text = "5:3";
        Screen.SetResolution(1280, 768, fullScreenMode.isOn);
    }

    public void FiveToFourRes()
    {
        resolution.text = "5:4";
        Screen.SetResolution(960, 768, fullScreenMode.isOn);
    }

    public void EightToFiveRes()
    {
        resolution.text = "8:5";
        Screen.SetResolution(1280, 800, fullScreenMode.isOn);
    }

    public void SixteenToNineRes()
    {
        resolution.text = "16:9";
        Screen.SetResolution(1280, 720, fullScreenMode.isOn);
    }

    public void ThirtytwoToFifteenRes()
    {
        resolution.text = "32:15";
        Screen.SetResolution(1280, 600, fullScreenMode.isOn);
    }
    #endregion

    #region Exit Methods
    public void LoadExit()
    {
        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        exitMenu.SetActive(true);
    }

    public void ExitYesButton()
    {
        Debug.Log("Should quit if in application mode. Will not quit in Unity editor.");

        Application.Quit();
    }

    public void ExitNoButton()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;
    
        exitMenu.SetActive(false);
    }
    #endregion Exit Methods

    #region Camera Animation
    public void CameraZoom(string direction)
    {
        animator.SetTrigger(direction);
    }
    #endregion
}
