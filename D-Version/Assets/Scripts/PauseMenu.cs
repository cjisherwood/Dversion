using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    Animator pauseMenuUI;  
    public bool isPauseOpen;

    public Text unpauseTimer;

    [SerializeField] private MenuController MenuCameraZoom;    

    void Start ()
    {
        Cursor.visible = false;

        pauseMenuUI = GameObject.Find("PauseFromMainMenu").GetComponent<Animator>();
        isPauseOpen = false;

        MenuCameraZoom = GameObject.Find("PauseMenuController").GetComponent<MenuController>();

        unpauseTimer.text = "";
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPauseOpen)
            {
                MenuCameraZoom.btnClickSound.Play();
                MenuCameraZoom.OptionsPlayButton();
               
                StartCoroutine(StartTimer(1));
                LaunchPauseMenu("Hide");

                Cursor.visible = false;
                isPauseOpen = false;
            }
            else
            {
                MenuCameraZoom.btnClickSound.Play();
                Time.timeScale = 0;
                LaunchPauseMenu("Launch");

                Cursor.visible = true;
                isPauseOpen = true;
            }
        }
    }

    public void LaunchPauseMenu(string launch)
    {
        pauseMenuUI.SetTrigger(launch);
    }

    public IEnumerator StartTimer(int value)
    {
        for (int i = 2; i >= 0; i--)
        {
            unpauseTimer.text = (i + 1).ToString();
            MenuCameraZoom.tickSound.Play();
            yield return new WaitForSecondsRealtime(value);

            if (i == 0)
            {
                unpauseTimer.text = "";
                Time.timeScale = 1;
            }
        }
    }
}
