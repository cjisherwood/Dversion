using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDowns : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform subButtons;
    public bool isOpen;
    private Vector3 scale;

    void Start ()
    {
        subButtons = transform.Find("SubButtons").GetComponent<RectTransform>();
        scale = subButtons.localScale;
        scale.y = 0;

        isOpen = false;
    }
	

	void Update ()
    {
        //Vector3 scale = subButtons.localScale;
        scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
        subButtons.localScale = scale;
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
