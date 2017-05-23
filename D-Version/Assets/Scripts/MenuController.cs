using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject exitMenu;
    public GameObject optionsMenu;

    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    public Toggle fullScreenMode;

    void Start()
    {
        exitMenu.SetActive(false);
        optionsMenu.SetActive(false);

        if (Screen.fullScreen)
        {
            fullScreenMode.isOn = true;
        }
        else if (!Screen.fullScreen)
        {
            fullScreenMode.isOn = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsButton.IsInteractable())
                LoadOptions();
            else
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
        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;

        optionsMenu.SetActive(true);
    }
    public void FullScreen()
    {
        fullScreenMode.isOn = !fullScreenMode.isOn;
    }

    public void BackButton()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;

        optionsMenu.SetActive(false);
    }
#endregion Options Methods

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
}
