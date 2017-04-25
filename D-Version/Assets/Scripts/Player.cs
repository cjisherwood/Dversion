using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform camera;

    public float speed;
    public ulong counter;
	public GameObject clone;

    private Transform player;
    public int numOfClones;
    public int cloneLimit;
    private ulong limit;
    private bool[,] origin;
    public bool[,] originForClone;

    void Start () 
	{
		player = gameObject.GetComponent<Transform>();

		limit = 3600;
        origin = new bool[limit, 5];
    }

    void FixedUpdate()
    {
        //Player controls
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            origin[counter, 0] = true;
            player.Translate(0, speed, 0);
            if(player.position.y - camera.position.y > 2)
            {
                camera.Translate(0, speed, 0);
            }
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            origin[counter, 1] = true;
            player.Translate(-speed, 0, 0);
            if (player.position.x - camera.position.x < -3.5)
            {
                camera.Translate(-speed, 0, 0);
            }
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            origin[counter, 2] = true;
            player.Translate(0, -speed, 0);
            if (player.position.y - camera.position.y < -2)
            {
                camera.Translate(0, -speed, 0);
            }
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            origin[counter, 3] = true;
            player.Translate(speed, 0, 0);
            if (player.position.x - camera.position.x > 3.5)
            {
                camera.Translate(speed, 0, 0);
            }
        }

        counter++;

        if (counter + 1 > limit)
        {
            limit += 3600; //Add a minute
            //need to expand array to origin[limit, 5];
        }

    }

    private void Update()
    {
        //Placeholder for resetting level and adding clone.
        if (Input.GetKeyDown("r"))
        {
            CreateClone();
        }
    }

    public ulong GetCounter()
	{
		return counter;
	}

    public void DestroyClone()
    {
        numOfClones--;
    }

    public void ResetLevel()
    {
        //Reset player to level start
        player.position = new Vector3(0, 3, 0);
        counter = 0;
        limit = 3600;
        originForClone = origin;
        origin = new bool[limit, 5];
    }

    public void CreateClone()
    {
        ResetLevel();

        if (numOfClones < cloneLimit)
        {
            //Add clone
            Instantiate(clone, (Vector3.up * 3), Quaternion.identity);
            numOfClones++;
        }
        else
        {
            Debug.Log("Clone limit reached. Clone not created.");
        }
    }

    public int CalcNextCloneNum()
    {
        GameObject[] clones;
        int nextClone = 10;

        clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach(GameObject clone in clones)
        {
            if (nextClone > clone.GetComponent<FollowPath>().cloneNum && (clone.GetComponent<FollowPath>().cloneNum + 1) != nextClone)
                nextClone = clone.GetComponent<FollowPath>().cloneNum + 1;
        }

        Debug.Log(nextClone);
        return nextClone;
    }
}
