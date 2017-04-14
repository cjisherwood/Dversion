using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    
    public float speed;
    public ulong counter;
	public GameObject clone;

    private Transform player;
    private ulong limit;
    private bool[,] origin;
    public bool[,] originForClone;

    void Start () 
	{
		player = gameObject.GetComponent<Transform>();

		limit = 3600;
		speed = 0.1f;
        origin = new bool[limit, 5];
    }
	
	void FixedUpdate () 
	{
        //Player controls
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
			origin[counter, 0] = true;               
			player.Translate(0, speed, 0);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            origin[counter, 1] = true;
			player.Translate(-speed, 0, 0);
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            origin[counter, 2] = true;
			player.Translate(0, -speed, 0);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            origin[counter, 3] = true;
			player.Translate(speed, 0, 0);
        }

        counter++;

        if (counter + 1 > limit)
        {
            limit += 3600; //Add a minute
            //need to expand array to origin[limit, 5];
        }

        //Placeholder for resetting level and adding clone.
        if (Input.GetKeyDown ("r")) 
		{
            originForClone = origin;
            origin = new bool[limit, 5];

            //Add clone
			Instantiate (clone, (Vector3.up * 3), Quaternion.identity);
            
			//Reset player to level start
			player.position = new Vector3 (0, 3, 0);
			counter = 0;
			limit = 3600;
        }
	}

	public ulong GetCounter()
	{
		return counter;
	}
}
