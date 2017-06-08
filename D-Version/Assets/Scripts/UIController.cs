using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Image[] activeImg;
    public Image[] InactiveImg;

    Player cloneNumScript;
    public int cloneNumber;

    void Start()
    {
        Time.timeScale = 1;
        cloneNumScript = GameObject.Find("Player").GetComponent<Player> ();
        
        for (int i = cloneNumScript.cloneLimit; i < 9; i++)
        {
            InactiveImg[i].enabled = false;
        }
    }

    void Update()
    {
        cloneNumber = cloneNumScript.CalcNextCloneNum();
        //Debug.Log("Clone Number is " + cloneNumber);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (cloneNumber == 1)
                activeImg[0].fillAmount += 1.0f;
            if (cloneNumber == 2)
                activeImg[1].fillAmount += 1.0f;
            if (cloneNumber == 3)
                activeImg[2].fillAmount += 1.0f;
            if (cloneNumber == 4)
                activeImg[3].fillAmount += 1.0f;
            if (cloneNumber == 5)
                activeImg[4].fillAmount += 1.0f;
            if (cloneNumber == 6)
                activeImg[5].fillAmount += 1.0f;
            if (cloneNumber == 7)
                activeImg[6].fillAmount += 1.0f;
            if (cloneNumber == 8)
                activeImg[7].fillAmount += 1.0f;
            if (cloneNumber == 9)
                activeImg[8].fillAmount += 1.0f;
        }
    }
}