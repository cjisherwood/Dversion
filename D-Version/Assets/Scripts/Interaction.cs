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

	// Use this for initialization
	void Start ()
    {
        me = gameObject.tag;
        activated = false;
        startingPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Interact(player.gameObject);

    }

    public GameObject PickUp()
    {
        gameObject.transform.Translate(0, 0, -100);
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
            }
        }

        if (actor.tag.Equals("Clone"))
        {
            if (actor.GetComponent<FollowPath>().item != null)
            {
                actor.GetComponent<FollowPath>().item = null;
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
                                Debug.Log(activated);
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
            buddy1.SetActive(!buddy1.activeSelf);
        }

        if (buddy2 != null)
        {
            buddy2.SetActive(!buddy2.activeSelf);
        }
    }
}
