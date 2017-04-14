using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

    private bool[,] origin;
    private Transform player;
	private ulong counter;
	private float speed;

    // Use this for initialization
    void Start ()
    {
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		speed = 0.1f;//FIX

		origin = player.GetComponent<Player>().originForClone;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		Debug.Log (origin [counter, 0]);

		if (origin[counter, 0])
		{           
			gameObject.GetComponent<Transform>().Translate(0, speed, 0);
		}
		if (origin[counter, 1])
		{
			gameObject.GetComponent<Transform>().Translate(-speed, 0, 0);
		}
		if (origin[counter, 2])
		{
			gameObject.GetComponent<Transform>().Translate(0, -speed, 0);
		}
		if (origin[counter, 3])
		{
			gameObject.GetComponent<Transform>().Translate(speed, 0, 0);
		}

		counter++;

		if (Input.GetKeyDown ("r")) 
		{
			//Reset clone to level start
			gameObject.GetComponent<Transform>().position = new Vector3 (0, 3, 0);
			counter = 0;
		}	
	}
}
