﻿using System;
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
    public int cloneLimit;

    public bool coolingDown;
    public float waitTime;

    public Text interactionE;
    public Text lockedDoor;

    void Start()
    {
        Time.timeScale = 1;
        cloneNumScript = GameObject.Find("Player").GetComponent<Player> ();
        cloneLimit = cloneNumScript.cloneLimit;
        
        for (int i = cloneLimit; i < 9; i++)
        {
            InactiveImg[i].enabled = false;
        }

        coolingDown = true;
        waitTime = 0.5f;

        Debug.Log("menus before " + (interactionE.isActiveAndEnabled ? "True" : "False"));
        interactionE.enabled = false;
        lockedDoor.enabled = false;
        Debug.Log("menus after " + (interactionE.isActiveAndEnabled ? "True" : "False"));
    }

    void Update()
    {
        cloneNumber = cloneNumScript.CalcNextCloneNum();

        CloneNumUI(cloneNumber);
    }

    public void CloneNumUI(int value)
    {
        if (cloneNumber < cloneLimit)
        {
            if (coolingDown)
            {
                activeImg[cloneNumber].fillAmount += 1.0f / waitTime * Time.deltaTime;

                if (activeImg[cloneNumber].fillAmount == 1)
                    coolingDown = false;
            }
            else
            {
                activeImg[cloneNumber].fillAmount -= 1.0f / waitTime * Time.deltaTime;

                if (activeImg[cloneNumber].fillAmount == 0)
                    coolingDown = true;
            }
        }

        for (int i = (cloneNumber - 1); i >= 0; i--)
        {
            activeImg[i].fillAmount = 1;
        }
    }
}