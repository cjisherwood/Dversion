using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject exitMenu;
    public GameObject optionsMenu;
    public new GameObject camera;

    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    public Toggle fullScreenMode;

    public Camera cameraToOptions;
    Animator animator;

    void Start()
    {
        animator = camera.GetComponent<Animator>();

        exitMenu.SetActive(false);
        optionsMenu.SetActive(false);
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
        CameraZoom("ZoomIn");

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
        CameraZoom("ZoomOut");

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

    public void CameraZoom(string direction)
    {
        animator.SetTrigger(direction);
    }
}
        