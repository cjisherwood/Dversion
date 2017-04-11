using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

    public Player playerScript;

    private bool[,] origin;
    private Transform player;

    // Use this for initialization
    void Start ()
    {
        playerScript = gameObject.GetComponent<Player>();
        player = gameObject.GetComponent<Transform>();

        origin = new bool[playerScript.counter, 5];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    
	}
}
