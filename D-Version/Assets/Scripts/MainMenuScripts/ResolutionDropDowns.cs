using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResolutionDropDowns : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform subButtonsRes;
    private Vector3 scaleRes;

    public bool isOpen;

    DifficultyDropDowns swooshSounds;
    
    void Start ()
    {
        subButtonsRes = transform.Find("SubButtonsRes").GetComponent<RectTransform>();
        scaleRes = subButtonsRes.localScale;
        scaleRes.y = 0;

        swooshSounds = GameObject.Find("DifficultySettings").GetComponent<DifficultyDropDowns>();

        isOpen = false;
	}
	
	void Update ()
    {
        scaleRes.y = Mathf.Lerp(scaleRes.y, isOpen ? 1 : 0, Time.deltaTime * 12);
        subButtonsRes.localScale = scaleRes;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        swooshSounds.swooshIn.Play();
        isOpen = true;

        Debug.Log("Entering resolution and value is " + isOpen);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        swooshSounds.swooshOut.Play();
        isOpen = false;

        Debug.Log("Exiting Resolution and value is " + isOpen);
    }
}
