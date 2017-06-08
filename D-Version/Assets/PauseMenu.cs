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
                    MenuCameraZoom.StartCoroutine(StartTimer(3));
                    Time.timeScale = 1;
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

        Debug.Log("Real Time is " + Time.realtimeSinceStartup +
            "Current Time is " + Time.time);
    }

    public void LaunchPauseMenu(string launch)
    {
        pauseMenuUI.SetTrigger(launch);
        
    }

    IEnumerator StartTimer(int value)
    {
        float pauseEndTime = Time.realtimeSinceStartup + value;

        Debug.Log("PausedTime Starts at " + pauseEndTime);

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            Debug.Log("PausedTime is " + pauseEndTime);

            if (Time.realtimeSinceStartup > Time.time + 1 && Time.realtimeSinceStartup <= Time.time + 2)
                // Display 3
                unpauseTimer.text = "3";

            else if (Time.realtimeSinceStartup > Time.time + 2 && Time.realtimeSinceStartup <= Time.time + 3)
                // Display 2
                unpauseTimer.text = "2";

            else if (Time.realtimeSinceStartup > Time.time + 3 && Time.realtimeSinceStartup <= Time.time + 4)
                // Display 1
                unpauseTimer.text = "1";

            else
                unpauseTimer.text = "";

            yield return null;
        }

        Debug.Log("PausedTime ends at " + pauseEndTime);
    }
}
