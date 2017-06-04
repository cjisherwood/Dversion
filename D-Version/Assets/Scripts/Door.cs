using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator open;
    private bool doorOpened;

	// Use this for initialization
	void Start ()
    {
        open = GetComponent<Animator>();
        doorOpened = false;
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (collision.gameObject.GetComponent<Player>().item.tag == "Key")
    //        {
    //            OpenDoor(collision.gameObject);
    //        } 
    //    }
    //    if (collision.gameObject.tag == "Clone")
    //    {
    //        if (collision.gameObject.GetComponent<FollowPath>().item.tag == "Key")
    //        {
    //            OpenDoor(collision.gameObject);
    //        }
    //    }
    //}

    //public void OpenDoor(GameObject aaron)
    //{
    //    open.SetTrigger("Open");
    //    if (aaron.tag == "Player")
    //    {
    //        aaron.GetComponent<Player>().item = aaron;
    //    }
    //    //START HERE
    //}
}
