using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Image[] img;
    public bool[] activeClone;
    public float waitTime = 30.0f;

    public Player playerScript;
    public int cloneNumber;

    void Start()
    {
        cloneNumber = playerScript.numOfClones;
    }

    void Update()
    {
        if (cloneNumber == 1)
            img[0].fillAmount += 1.0f;
        if (cloneNumber == 2)
            img[1].fillAmount += 1.0f;
        if (cloneNumber == 3)
            img[2].fillAmount += 1.0f;
        if (cloneNumber == 4)
            img[3].fillAmount += 1.0f;
        if (cloneNumber == 5)
            img[4].fillAmount += 1.0f;
        if (cloneNumber == 6)
            img[5].fillAmount += 1.0f;
        if (cloneNumber == 7)
            img[6].fillAmount += 1.0f;
        if (cloneNumber == 8)
            img[7].fillAmount += 1.0f;
        if (cloneNumber == 9)
            img[8].fillAmount += 1.0f;
    }

    public void OnClickStuff()
    {
        activeClone[0] = true;
    }
}