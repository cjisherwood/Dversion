using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Player : MonoBehaviour {

    //Added declaration of Serial Port
    SerialPort sp = new SerialPort("COM3", 9600);

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
        //Initiallized Serial Port
        sp.Open();
        sp.ReadTimeout = 1;

		player = gameObject.GetComponent<Transform>();

		limit = 3600;
        origin = new bool[limit, 5];
    }

    void FixedUpdate()
    {
        if (sp.IsOpen)
        {
            try
            {
                MovePlayerArduino(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            //Player controls
            if (Input.GetKey("w") || Input.GetKey("up"))
            {
                origin[counter, 0] = true;
                player.Translate(0, speed, 0);
                if (player.position.y - camera.position.y > 2)
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
        }

        counter++;

        if (counter + 1 > limit)
        {
            limit += 3600; //Add a minute
            //need to expand array to origin[limit, 5];
        }

    }

    private void MovePlayerArduino(int i)
    {
        //Player controls with NES controller
        switch (i)
        {
            case 0:
                origin[counter, 0] = true;
                player.Translate(0, speed, 0);
                if (player.position.y - camera.position.y > 2)
                {
                    camera.Translate(0, speed, 0);
                }
                break;
            case 1:
                origin[counter, 3] = true;
                player.Translate(speed, 0, 0);
                if (player.position.x - camera.position.x > 3.5)
                {
                    camera.Translate(speed, 0, 0);
                }
                break;
            case 2:
                origin[counter, 2] = true;
                player.Translate(0, -speed, 0);
                if (player.position.y - camera.position.y < -2)
                {
                    camera.Translate(0, -speed, 0);
                }
                break;
            case 3:
                origin[counter, 1] = true;
                player.Translate(-speed, 0, 0);
                if (player.position.x - camera.position.x < -3.5)
                {
                    camera.Translate(-speed, 0, 0);
                }
                break;
            default:
                Debug.Log("Default case!");
                break;
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

    public void ResetLevel()
    {
        //Reset player to level start
        player.position = Vector3.zero;
        counter = 0;
        limit = 3600;
        originForClone = origin;
        origin = new bool[limit, 5];
        camera.position = Vector3.back;
    }

    //NEEDS FIXED
    public int CalcNextCloneNum()
    {
        GameObject[] clones;
        int nextClone = 0;
        clones = GameObject.FindGameObjectsWithTag("Clone");

        //If there is no clones yet, our next clone is the first.
        if (clones.Length == 1)
        {
            nextClone = 1;
            return nextClone;
        }
        else //If there is a clone
        {
            foreach (GameObject clone in clones)
            {
                //Check clones in order from the first to the last until we find a missing clone,
                //set that as our next clone.
                if (clone.transform.position.z == -1)
                {
                    nextClone = clone.GetComponent<FollowPath>().cloneNum;
                    return nextClone;
                }
                nextClone++;
            }
        }
        return nextClone;
    }

    public void CreateClone()
    {
        ResetLevel();

        if (numOfClones < cloneLimit)
        {
            //Add clone
            Instantiate(clone, (Vector3.zero), Quaternion.identity);
            numOfClones++;
        }
        else if (CalcNextCloneNum() <= cloneLimit)
        {
            GameObject[] clones;

            clones = GameObject.FindGameObjectsWithTag("Clone");

            int nextClone = CalcNextCloneNum();

            foreach (GameObject clone in clones)
            {
                if (clone.GetComponent<FollowPath>().cloneNum == nextClone)
                {
                    clone.transform.Translate(0, 0, 1);
                    clone.GetComponent<FollowPath>().origin = originForClone;
                }
            }
            
        }
        else
        {
            Debug.Log("Clone limit reached. Clone not created.");
        }
    }
}
