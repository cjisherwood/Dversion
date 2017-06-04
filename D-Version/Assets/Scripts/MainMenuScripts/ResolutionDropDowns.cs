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
    
    void Start ()
    {
        subButtonsRes = transform.Find("SubButtonsRes").GetComponent<RectTransform>();
        scaleRes = subButtonsRes.localScale;
        scaleRes.y = 0;

        isOpen = false;
	}
	
	void Update ()
    {
        scaleRes.y = Mathf.Lerp(scaleRes.y, isOpen ? 1 : 0, Time.deltaTime * 12);
        subButtonsRes.localScale = scaleRes;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;
    }
}
