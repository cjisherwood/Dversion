using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    #region Variables
    public GameObject exitMenu;     //Panel in which all of the exit menu items are present
    public GameObject optionsMenu;  //Panel in which all of the exit menu items are present
    public new GameObject camera;   //Getting access to main camera for options animation purposes

    //Declaration of all three buttons in which the main menu consists
    public Button playButton, optionsButton, exitButton;

    public Text difficulty, resolution; //Variable used to access the text component of the difficulty and resolution buttons

    public Toggle fullScreenMode;  //Declaring the toggle used to change screen size

    public bool easy, medium, hard; //Boolean values used to decide the game's difficulty

    Animator animator;   //Animator used to animate the zoom in motion for the options menu

    //Variables used only on in-game. 
    PauseMenu pauseMenu;        //Gets access to the script PauseMenu
    public GameObject exitGameMenu;    //Gets access to the exit game options when clicked on "DESKTOP"
    #endregion

    #region Start and Update
    void Start()
    {
        //Setting up animator from camera
        animator = camera.GetComponent<Animator>(); 

        //Hiding options menu and exit menu at start of scene
        exitMenu.SetActive(false);      
        optionsMenu.SetActive(false);

        //Difficulty settings are by default easy. 
        //Boolean values can be used to set difficulty by other scripts
        easy = true;
        medium = hard = false;

        //Checking if game is in full screen mode, if so, toggle in settings
        //will be checked, else otherwise
        if (Screen.fullScreen)
            fullScreenMode.isOn = true;
        else
            fullScreenMode.isOn = false;

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            //Finding the game objets with the specified type
            pauseMenu = GameObject.Find("UICtrl").GetComponent<PauseMenu>();

            //Disables the exit game menu options
            exitGameMenu.SetActive(false);
        }
        
        
    }

    public void Update()
    {
        //When pressing the escape key, if the difficulty menu is active,
        //the key will act as the back button inside options.
        //If key pressed in the main menu, it will act as the full screen toggle inside options
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (difficulty.isActiveAndEnabled)
                BackButton();
            else if (playButton.IsInteractable() && Screen.fullScreen)
            {
                Screen.fullScreen = false;
                fullScreenMode.isOn = false;
            }
        }
    }
    #endregion

    #region Play Button
    //Loads a scene by is name. Scene must be in built settings in order for it to be called
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //If "Play" button is pressed in the game, it will bring the player right back to the existing gameplay.
    public void OptionsPlayButton()
    {
        pauseMenu.LaunchPauseMenu("Hide");

        Cursor.visible = false;
        pauseMenu.isPauseOpen = false;

        //Does this next line work? check it at meeting
        StartCoroutine(GameObject.Find("UICtrl").GetComponent<PauseMenu>().unpauseTimer.ToString());
    }
    #endregion

    #region Options Methods
    //Loads the options menu by:
    // *animating the camera towards the menu,
    // *disabling play, options, and exit buttons,
    // *and making the options menu visible
    public void LoadOptions()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            CameraZoom("InGameZoomIn");
        }
        else
        { 
            CameraZoom("ZoomIn");
        }

        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        optionsMenu.SetActive(true);
    }

    //When full screen toggle is pressed, it checks if its checked, 
    //if so it sets full screen to true and false other wise
    public void FullScreen()
    {
        if (fullScreenMode.isOn)
        {
            Screen.fullScreen = true;
            Debug.Log("Should enter full screen in application");
        }
        else
        {
            Screen.fullScreen = false;
            Debug.Log("Should exit full screen in application");
        }

    }

    //When back button is pressed:
    // *animation of zoom out is played,
    // *play, options, and exit butttons become active again,
    // *and it hides the options menu
    public void BackButton()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            CameraZoom("InGameZoomOut");
        }
        else
        {
            CameraZoom("ZoomOut");
        }

        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

        optionsMenu.SetActive(false);
    }
    #endregion Options Methods

    #region Inside Options Difficulty
    //The following methods only set boolean values 
    //assigned to difficulty to either easy, medium, or hard
    public void EasyButton()
    {
        difficulty.text = "Easy";

        easy = true;
        medium = hard = false;
    }

    //See method EasyButton() at the beginning or region for description in this method
    public void MediumButton()
    {
        difficulty.text = "Medium";

        medium = true;
        easy = hard = false;
    }

    //See method EasyButton() at the beginning or region for description in this method
    public void HardButton()
    {
        difficulty.text = "Hard";

        hard = true;
        easy = medium = false;
    }
    #endregion

    #region Inside Options Resolution
    //The following methods in this region will alter the application's 
    //aspect ratio to the methods respective ratio whether in full screen or not.
    public void ThreeToTwoRes()
    {
        resolution.text = "3:2";
        Screen.SetResolution(1080, 720, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FourToThreeRes()
    {
        resolution.text = "4:3";
        Screen.SetResolution(1024, 768, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FiveToThreeRes()
    {
        resolution.text = "5:3";
        Screen.SetResolution(1280, 768, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FiveToFourRes()
    {
        resolution.text = "5:4";
        Screen.SetResolution(960, 768, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void EightToFiveRes()
    {
        resolution.text = "8:5";
        Screen.SetResolution(1280, 800, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void SixteenToNineRes()
    {
        resolution.text = "16:9";
        Screen.SetResolution(1280, 720, fullScreenMode.isOn);
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void ThirtytwoToFifteenRes()
    {
        resolution.text = "32:15";
        Screen.SetResolution(1280, 600, fullScreenMode.isOn);
    }
    #endregion

    #region Exit Methods
    //When the exit button is pressed:
    // *play, options, and exit buttons will be disabled
    // *and the exit menu will be active.
    public void LoadExit()
    {
        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        exitMenu.SetActive(true);
    }

    //Is "HOME" is pressed, player will be directed to main menu
    public void ExitHomeButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    //By pressing "DESKTOP" exit menu disables and exit game menu enables
    public void ExitDesktopButton()
    {
        exitMenu.SetActive(false);
        exitGameMenu.SetActive(true);
    }

    //Pressing yes inside the exit menu will exit the application
    public void ExitYesButton()
    {
        Debug.Log("Should quit if in application mode. Will not quit in Unity editor.");

        Application.Quit();
    }

    //Pressing no in exit menu will:
    // *set play, options, and exit buttons to be active
    // *and set the exit menu to not active
    public void ExitNoButton()
    {
        
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

        exitMenu.SetActive(false);

        if (GameObject.FindGameObjectWithTag("Player")) 
        {
            exitGameMenu.SetActive(false);
        }
    }
    #endregion Exit Methods

    #region Camera Animation
    //Method that take a string as a parameter where the string is the trigger name.
    //The trigger with that name gets activated and the animation is played
    public void CameraZoom(string direction)
    {
        animator.SetTrigger(direction);
    }
    #endregion
}
