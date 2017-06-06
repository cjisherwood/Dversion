using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject inGamePause;

    //Animator pauseMenuUI;   //Uncomment for animation
    //public bool isPauseOpen;       //Uncomment for animation

    void Start ()
    {
        Cursor.visible = false;

        inGamePause.SetActive(false);

        //pauseMenuUI = GameObject.Find("PauseMenuUIAnim").GetComponent<Animator>();
        //isPauseOpen = false;        //Uncomment for animation
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (inGamePause.activeSelf)
            {
                inGamePause.SetActive(false);
                //Time.timeScale = 1;

                Cursor.visible = false;
            }
            else
            {
                inGamePause.SetActive(true);
                //Time.timeScale = 0;

                Cursor.visible = true;
            }
        }

        //Uncomment for animation
        //if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        //{
        //    LaunchPauseMenu(isPauseOpen ? "Hide" : "Launch");

        //    isPauseOpen = !isPauseOpen;
        //}
    }

    //Uncomment for animation
    //public void LaunchPauseMenu(string launch)
    //{
    //    pauseMenuUI.SetTrigger(launch);
    //}
}
