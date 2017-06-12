using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator open;
    private bool doorOpened;
    public bool wasActive;

    UIController UIText;

	// Use this for initialization
	void Start ()
    {
        open = GetComponent<Animator>();
<<<<<<< HEAD
        doorOpened = false;
        wasActive = gameObject.active;

        UIText = GameObject.Find("UICtrl").GetComponent<UIController>();
=======
        doorOpened = !gameObject.activeSelf;
        wasActive = gameObject.activeSelf;
>>>>>>> 8e37a154b0b2c11965b4536f55a68aa719e25739
	}

    private void Update()
    {
        if (open.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            doorOpened = true;
        }
        if (open.GetCurrentAnimatorStateInfo(0).IsName("DoorIdle") && doorOpened)
        {
            Destroy(gameObject);
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
                }
            }
            else
            {
                UIText.lockedDoor.enabled = true;
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
        open.SetTrigger("Open");
        if (aaron.tag == "Player")
        {
            aaron.GetComponent<Player>().item = aaron;
        }
        //START HERE
    }
}
