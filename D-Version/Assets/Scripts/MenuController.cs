using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ExitMenu;
    public GameObject OptionsMenu;

    public Button PlayButton;
    public Button OptionsButton;
    public Button ExitButton;

    void Start()
    {
        ExitMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void LoadScene(string sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    #region Options Methods
    public void LoadOptions()
    {
        PlayButton.enabled = false;
        ExitButton.enabled = false;
        OptionsButton.enabled = false;

        OptionsMenu.SetActive(true);
    }
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    #endregion Options Methods

    #region Exit Methods
    public void LoadExit()
    {
        PlayButton.enabled = false;
        ExitButton.enabled = false;
        OptionsButton.enabled = false;

        ExitMenu.SetActive(true);
    }

    public void ExitYesButton()
    {
        Debug.Log("Should quit if in application mode. Will not quit in Unity editor.");

        Application.Quit();
    }

    public void ExitNoButton()
    {
        PlayButton.enabled = true;
        ExitButton.enabled = true;
        OptionsButton.enabled = true;

        ExitMenu.SetActive(false);
    }
    #endregion Exit Methods
}
