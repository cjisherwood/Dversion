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

    MenuController MenuCameraZoom;    

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
                if (!MenuCameraZoom.playButton.IsInteractable())
                {
                    MenuCameraZoom.BackButton();
                }
                else
                {
                    StartCoroutine(StartTimer(1));
                    LaunchPauseMenu("Hide");

                    Cursor.visible = false;
                    isPauseOpen = false;
                }
            }
            else
            {
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

    /*IEnumerator StartTimer(int value)
    {
        float pauseEndTime = Time.realtimeSinceStartup + value;
        
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            if (Time.realtimeSinceStartup > 1 && Time.realtimeSinceStartup <= 2)
                // Display 3
                unpauseTimer.text = "3";

            else if (Time.realtimeSinceStartup > 2 && Time.realtimeSinceStartup <= 3)
                // Display 2
                unpauseTimer.text = "2";

            else if (Time.realtimeSinceStartup > 3 && Time.realtimeSinceStartup <= 4)
                // Display 1
                unpauseTimer.text = "1";

            else
                unpauseTimer.text = "";

            yield return null;
        }
    }*/

    IEnumerator StartTimer(int value)
    {
        for (int i = 2; i >= 0; i--)
        {
            unpauseTimer.text = (i + 1).ToString();
            yield return new WaitForSecondsRealtime(value);

            if (i == 0)
            {
                unpauseTimer.text = "";
                Time.timeScale = 1;
            }
        }
    }
}
