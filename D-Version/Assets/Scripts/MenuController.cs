using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ExitMenu;
    public GameObject OptionsMenu;
    public GameObject testerCube;

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
        PlayButton.interactable = false;
        ExitButton.interactable = false;
        OptionsButton.interactable = false;

        testerCube.transform.Translate(Vector3.up * Time.deltaTime);

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
        PlayButton.interactable = false;
        ExitButton.interactable = false;
        OptionsButton.interactable = false;

        ExitMenu.SetActive(true);
    }

    public void ExitYesButton()
    {
        Debug.Log("Should quit if in application mode. Will not quit in Unity editor.");

        Application.Quit();
    }

    public void ExitNoButton()
    {
        PlayButton.interactable = true;
        ExitButton.interactable = true;
        OptionsButton.interactable = true;

        ExitMenu.SetActive(false);
    }
    #endregion Exit Methods
}
