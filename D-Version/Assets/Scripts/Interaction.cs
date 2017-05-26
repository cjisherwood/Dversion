using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform player;
    public Transform[] clones;
    private string me;
    private bool activated;
    public GameObject buddy;

	// Use this for initialization
	void Start ()
    {
        me = gameObject.tag;
        activated = false;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Interact(player.gameObject);

    }

    public GameObject PickUp()
    {
        gameObject.transform.Translate(0, 0, -1);
        return gameObject;
    }

    public void PutDown(GameObject actor)
    {
        if (actor.tag.Equals("Player"))
        {
            if (!actor.GetComponent<Player>().item.tag.Equals("Player"))
            {
                actor.GetComponent<Player>().item = player.gameObject;
                gameObject.transform.position = actor.transform.position;
            }
        }

        if (actor.tag.Equals("Clone"))
        {
            if (!actor.GetComponent<FollowPath>().item.tag.Equals("Clone"))
            {
                actor.GetComponent<FollowPath>().item = actor.gameObject;
                gameObject.transform.position = actor.transform.position;
            }
        }
    }

    public void Interact(GameObject actor)
    {
        if (actor.tag.Equals("Player"))
        {
            if (Input.GetKeyDown("e") || Input.GetKeyDown("enter"))
            {
                if (player.GetComponent<Player>().item.tag.Equals("Player"))
                {
                    if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 0.3)
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
            if (actor.GetComponent<FollowPath>().item.tag.Equals("Clone"))
            {
                if (Vector3.Distance(actor.transform.position, gameObject.transform.position) < 0.3)
                {
                    switch (me)
                    {
                        case "Key":
                            actor.GetComponent<FollowPath>().item = PickUp();
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

    public void Activate()
    {
        buddy.SetActive(!activated);
    }
}
