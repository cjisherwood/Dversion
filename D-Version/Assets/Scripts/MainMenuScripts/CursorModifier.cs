using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorModifier : MonoBehaviour
{
    #region Variables
    //Textures used to modify the cursor's appearance 
    //according to which button is the cursor on
    public Texture2D defaultCursor;
    public Texture2D playBtn;
    public Texture2D optionsBtn;
    public Texture2D backOptionsBtn;
    public Texture2D exitBtn;
   
    private CursorMode curMode; //Variable that allows the cursor to be modified
    private Vector2 hotSpot;    //Variable that is used to identify where the texture should be placed at
    #endregion

    #region Start and Update Methods
    void Start ()
    {
        curMode = CursorMode.Auto;
        hotSpot = Vector2.zero;
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);
    }

	void Update ()
    {
		if (Input.GetButtonUp("Fire1"))
        {
            Cursor.SetCursor(defaultCursor, hotSpot, curMode);
        }
	}
    #endregion

    #region Cursor Renderer
    void OnMouseEnter()
    {
        if (gameObject.tag == "PlayButton")
        {
            Cursor.SetCursor(playBtn, hotSpot, curMode);
        }
        if (gameObject.tag == "OptionButton")
        {
            Cursor.SetCursor(optionsBtn, hotSpot, curMode);
        }
        if (gameObject.tag == "BackOptionsButton")
        {
            Cursor.SetCursor(backOptionsBtn, hotSpot, curMode);
        }
        if (gameObject.tag == "ExitButton")
        {
            Cursor.SetCursor(exitBtn, hotSpot, curMode);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);
    }
    #endregion
}
