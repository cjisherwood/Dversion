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

    void Start()
    {
        img[0] = GetComponentInChildren<Image>();
        activeClone[0] = false;
    }

    void Update()
    {
        if (activeClone[0])
        {
            img[0].fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }

    public void OnClickStuff()
    {
        activeClone[0] = true;
    }
}