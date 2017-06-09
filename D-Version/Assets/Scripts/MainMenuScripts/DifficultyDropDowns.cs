using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyDropDowns : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform subButtonsDif;
    private Vector3 scaleDif;

    public bool isOpenDif;
    
    void Start ()
    {
        subButtonsDif = transform.Find("SubButtons").GetComponent<RectTransform>();
        scaleDif = subButtonsDif.localScale;
        scaleDif.y = 0;

        isOpenDif = false;
    }
	
	void Update ()
    {
        scaleDif.y = Mathf.Lerp(scaleDif.y, isOpenDif ? 1 : 0, Time.deltaTime * 12);
        subButtonsDif.localScale = scaleDif;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOpenDif = true;

        Debug.Log("Entering difficulty and value is " + isOpenDif);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOpenDif = false;

        Debug.Log("Exiting difficulty and value is " + isOpenDif);
    }
}
