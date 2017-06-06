using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject inGamePause;

	void Start ()
    {
        Cursor.visible = false;

        inGamePause.SetActive(false);
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
	}
}
