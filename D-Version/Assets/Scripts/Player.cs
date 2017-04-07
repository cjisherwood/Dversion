using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform player;
    public float speed;
    public uint counter;

    private uint limit;
    private bool[,] origin;
    private bool[,] originForClone;

    void Start () 
	{
        player = gameObject.GetComponent<Transform>();

        limit = 3600;
        origin = new bool[limit, 5];
    }
	
	void FixedUpdate () 
	{
        //Player controls
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            origin[counter, 1] = true;               
            player.Translate(0, speed, 0);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            origin[counter, 2] = true;
            player.Translate(-speed, 0, 0);
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            origin[counter, 3] = true;
            player.Translate(0, -speed, 0);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            origin[counter, 4] = true;
            player.Translate(speed, 0, 0);
        }

        counter++;

        if (counter > limit)
        {
            limit *= 2;
            //need to expand array to origin[limit, 5];
        }

        //Placeholder for resetting level and adding clone.
        if (Input.GetKeyDown ("r")) 
		{
            //Reset player to level start
            player.position = new Vector3 (0, 3, 0);
            counter = 0;

            originForClone = origin;
            origin = new bool[3600, 5];

            //Add clone

            

        }
	}

    void CopyArray()
    {

    }
}
