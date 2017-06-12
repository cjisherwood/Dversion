using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform player;
    public Transform[] clones;
    private string me;
    private bool activated;
    public GameObject buddy1;
    public GameObject buddy2;
    public Vector3 startingPoint;

    UIController UIText;
    //MenuController 

	// Use this for initialization
	void Start ()
    {
        me = gameObject.tag;
        activated = false;
        startingPoint = transform.position;

        UIText = GameObject.Find("UICtrl").GetComponent<UIController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Interact(player.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && gameObject.GetComponent<Collider2D>().enabled == true)
        {
            UIText.interactionE.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            UIText.interactionE.enabled = false;
        }
    }

    public GameObject PickUp()
    {
        gameObject.transform.Translate(0, 0, -100);
        gameObject.GetComponent<Collider2D>().enabled = false;

        return gameObject;
    }

    public void PutDown(GameObject actor)
    {
        if (actor.tag.Equals("Player"))
        {
            if (actor.GetComponent<Player>().item != null)
            {
                actor.GetComponent<Player>().item = null;
                gameObject.transform.position = actor.transform.position;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }

        if (actor.tag.Equals("Clone"))
        {
            if (actor.GetComponent<FollowPath>().item != null)
            {
                actor.GetComponent<FollowPath>().item = null;
                gameObject.transform.position = actor.transform.position;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void Interact(GameObject actor)
    {
        if (actor.tag.Equals("Player"))
        {
            if (Input.GetKeyDown("e") || Input.GetKeyDown("enter"))
            {
                if (player.GetComponent<Player>().item == null)
                {
                    if (Vector3.Distance(actor.transform.position, gameObject.transform.position) < 0.5)
                    {
                        switch (me)
                        {
                            case "Key":
                                player.GetComponent<Player>().item = PickUp();
                                break;
                            case "Switch": //Make work when item is held
                                activated = !activated;
                                Activate();
                                break;
                            default:
                                Debug.Log("Please attach the proper tag to this object.");
                                break;
                        }
                    }
                }
            }
        }


        else
        {
            if (actor.GetComponent<FollowPath>().item == null)
            {
                if (Vector3.Distance(actor.transform.position, gameObject.transform.position) < 0.5)
                {
                    switch (me)
                    {
                        case "Key":
                            actor.GetComponent<FollowPath>().item = PickUp();
                            break;
                        case "Switch":
                            activated = !activated;
                            Activate();
                            break;
                        default:
                            Debug.Log("Please attach the proper tag to this object.");
                            break;
                    }
                }
            }
        }
    }

    public void Activate()
    {
        if (buddy1 != null)
        {
            if (buddy1.GetComponent<Door>().wasActive == false)
            {
                player.GetComponent<Player>().allObjectsToReset.Add(buddy1);
                player.GetComponent<Player>().allObjectsToReset[player.GetComponent<Player>().allObjectsToReset.Count - 1].GetComponent<Door>().wasActive = false;
            }
            buddy1.SetActive(!buddy1.activeSelf);
        }

        if (buddy2 != null)
        {
            if (buddy1.GetComponent<Door>().wasActive == false)
            {
                player.GetComponent<Player>().allObjectsToReset.Add(buddy2);
                player.GetComponent<Player>().allObjectsToReset[player.GetComponent<Player>().allObjectsToReset.Count - 1].GetComponent<Door>().wasActive = false;
            }
            buddy2.SetActive(!buddy2.activeSelf);
        }
    }
}
