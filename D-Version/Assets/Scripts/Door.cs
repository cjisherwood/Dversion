using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator open;
    public bool doorOpened;
    public bool wasOpen;

    UIController UIText;
    MenuController menuSounds;

	// Use this for initialization
	void Start ()
    {
        open = GetComponent<Animator>();

        UIText = GameObject.Find("UICtrl").GetComponent<UIController>();
        menuSounds = GameObject.Find("PauseMenuController").GetComponent<MenuController>();

        wasOpen = doorOpened;

        if (doorOpened)
        {
            OpenDoor(GameObject.FindGameObjectWithTag("Player"));
        }
        if (!doorOpened)
        {
            CloseDoor(GameObject.FindGameObjectWithTag("Player"));
            OpenDoor(GameObject.FindGameObjectWithTag("Player"));
            CloseDoor(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private void Update()
    {
        if (open.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            doorOpened = true;
        }
        if(open.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
        {
            doorOpened = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().item != null)
            {
                if (collision.gameObject.GetComponent<Player>().item.tag == "Key")
                {
                    OpenDoor(collision.gameObject);
                    menuSounds.doorUnlocked.Play();
                }
            }
            else
            {
                UIText.lockedDoor.enabled = true;
                menuSounds.doorLocked.Play();
            }
        }
        if (collision.gameObject.tag == "Clone")
        {
            if(collision.gameObject.GetComponent<Player>().item != null)
            {
                if (collision.gameObject.GetComponent<FollowPath>().item.tag == "Key")
                {
                    OpenDoor(collision.gameObject);
                }
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        UIText.lockedDoor.enabled = false;
    }

    public void OpenDoor(GameObject aaron)
    {
        DoorAnim("Open");
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (aaron.tag == "Player")
        {
            aaron.GetComponent<Player>().item = null;
        }
    }

    public void CloseDoor(GameObject aaron)
    {
        Debug.Log("closing");
        DoorAnim("Close");
        gameObject.GetComponent<Collider2D>().enabled = true;
        if (aaron.tag == "Player")
        {
            aaron.GetComponent<Player>().item = null;
        }
    }

    public void DoorAnim(string state)
    {
        open.SetTrigger(state);
    }
}
