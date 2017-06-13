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

    public AudioSource btnClickSound;
    public AudioSource booSound;
    public AudioSource backgroundSound;
    public AudioSource elevatorSound;
    public AudioSource doorLocked;
    public AudioSource doorUnlocked;
    public AudioSource tickSound;

    public Slider musicSlider;
    public Toggle musicToggle;

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
        }

        if (GameObject.Find("ExitGameMenu"))
        {
            //Disables the exit game menu options
            exitGameMenu.SetActive(false);
        }

        musicToggle.isOn = true;
        musicSlider.value = 1;
    }

    public void Update()
    {
        //When pressing the escape key, if the difficulty menu is active,
        //the key will act as the back button inside options.
        //If key pressed in the main menu, it will act as the full screen toggle inside options
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (difficulty.isActiveAndEnabled)
            {
                btnClickSound.Play();
                BackButton();
            }
            else if (playButton.IsInteractable() && Screen.fullScreen)
            {
                btnClickSound.Play();
                Screen.fullScreen = false;
                fullScreenMode.isOn = false;
            }
        }

        backgroundSound.volume = musicSlider.value;
    }
    #endregion

    #region Play Button
    //Loads a scene by is name. Scene must be in built settings in order for it to be called
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        btnClickSound.Play();
    }

    //If "Play" button is pressed in the game, it will bring the player right back to the existing gameplay.
    public void OptionsPlayButton()
    {
        pauseMenu.LaunchPauseMenu("Hide");

        Cursor.visible = false;
        pauseMenu.isPauseOpen = false;
        btnClickSound.Play();

        //Need to add a way to StartCoroutine and set a 3 second timer after pressing play.
        StartCoroutine(pauseMenu.StartTimer(1));
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

        btnClickSound.Play();
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
        btnClickSound.Play();
    }

    //When back button is pressed:
    // *animation of zoom out is played,
    // *play, options, and exit butttons become active again,
    // *and it hides the options menu
    public void BackButton()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (exitMenu.activeSelf)
            {
                exitMenu.SetActive(false);
            }
            else
            {
                optionsMenu.SetActive(false);
                CameraZoom("InGameZoomOut");
            }
        }
        else
        {
            optionsMenu.SetActive(false);
            CameraZoom("ZoomOut");
        }

        btnClickSound.Play();
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

    }

    public void MusicControl()
    {
        if (musicToggle.isOn)
        {
            backgroundSound.mute = true;
            musicToggle.isOn = false;
        }
        else
        {
            backgroundSound.mute = false;
            musicToggle.isOn = true;
        }
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
        btnClickSound.Play();
    }

    //See method EasyButton() at the beginning or region for description in this method
    public void MediumButton()
    {
        difficulty.text = "Medium";

        medium = true;
        easy = hard = false;
        btnClickSound.Play();
    }

    //See method EasyButton() at the beginning or region for description in this method
    public void HardButton()
    {
        difficulty.text = "Hard";

        hard = true;
        easy = medium = false;
        btnClickSound.Play();
    }
    #endregion

    #region Inside Options Resolution
    //The following methods in this region will alter the application's 
    //aspect ratio to the methods respective ratio whether in full screen or not.
    public void ThreeToTwoRes()
    {
        resolution.text = "3:2";
        Screen.SetResolution(1080, 720, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FourToThreeRes()
    {
        resolution.text = "4:3";
        Screen.SetResolution(1024, 768, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FiveToThreeRes()
    {
        resolution.text = "5:3";
        Screen.SetResolution(1280, 768, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void FiveToFourRes()
    {
        resolution.text = "5:4";
        Screen.SetResolution(960, 768, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void EightToFiveRes()
    {
        resolution.text = "8:5";
        Screen.SetResolution(1280, 800, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void SixteenToNineRes()
    {
        resolution.text = "16:9";
        Screen.SetResolution(1280, 720, fullScreenMode.isOn);
        btnClickSound.Play();
    }

    //Refer to method ThreeToTwoRes() at the beggining of region for a description on this method
    public void ThirtytwoToFifteenRes()
    {
        resolution.text = "32:15";
        Screen.SetResolution(1280, 600, fullScreenMode.isOn);
        btnClickSound.Play();
    }
    #endregion

    #region Exit Methods
    //When the exit button is pressed:
    // *play, options, and exit buttons will be disabled
    // *and the exit menu will be active.
    public void LoadExit()
    {
        btnClickSound.Play();
        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        exitMenu.SetActive(true);
    }

    //Is "HOME" is pressed, player will be directed to main menu
    public void ExitHomeButton()
    {
        btnClickSound.Play();
        SceneManager.LoadScene("Main_Menu");
    }

    //By pressing "DESKTOP" exit menu disables and exit game menu enables
    public void ExitDesktopButton()
    {
        booSound.Play();
        exitMenu.SetActive(false);
        exitGameMenu.SetActive(true);
    }

    //Pressing yes inside the exit menu will exit the application
    public void ExitYesButton()
    {
        //Add play boo sound
   
        Debug.Log("Should quit if in application mode. Will not quit in Unity editor.");
        Application.Quit();
    }

    //Pressing no in exit menu will:
    // *set play, options, and exit buttons to be active
    // *and set the exit menu to not active
    public void ExitNoButton()
    {
        btnClickSound.Play();
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

        exitMenu.SetActive(false);

        if (GameObject.Find("ExitGameMenu"))
            exitGameMenu.SetActive(false);
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
