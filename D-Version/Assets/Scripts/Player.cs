using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform player;

    void Start () 
	{
        player = gameObject.GetComponent<Transform>();

    }
	
	void FixedUpdate () 
	{
		//Player controls:

		if (Input.GetKey ("w") || Input.GetKey ("up")) 
		{
            player.Translate (0, (float) 0.05, 0);
		}
		if (Input.GetKey ("s") || Input.GetKey ("down")) 
		{
            player.Translate (0, (float) -0.05, 0);
		}
		if (Input.GetKey ("d") || Input.GetKey ("right")) 
		{
            player.Translate ((float) 0.05, 0, 0);
		}
		if (Input.GetKey ("a") || Input.GetKey ("left")) 
		{
            player.Translate ((float) -0.05, 0, 0);
		}

        //Placeholder for resetting level and adding clone.
        if (Input.GetKeyDown ("r")) 
		{
            //Reset player to level start
            player.position = new Vector3 (0, 3, 0);

            //Add clone

		}
	}
}
