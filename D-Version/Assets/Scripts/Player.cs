using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour, ICloneable {

    //Player attributes:
    private Transform player; //Holds our transform component.
    private Vector2 velocity; //The direction the player is moving.
    private const float speed = 0.07f; //How quickly the player moves around the level.
    private Rigidbody2D rb; //The rigidbody for controlling movement.
    public Vector3 startingPoint; //Holds the position we first started in a level.
    public GameObject item; //The object the player is presently holding, if none, item is null.
    public Transform mCamera; //For camera translation.

    //Clone attributes:
    private bool[,] origin; //Used to track the players actions and later copy them to clones.
    public bool[,] originForClone; //Used to hold old interaction we send to the clones while we track new interaction.
    private ulong counter; //Counts the current frame number since scene loading/clone creation/level reset.
    private ulong limit; //The present limit to how many frames we will keep track of player movement information.
                         //Increases dynamically as the level drags on to prevent overuse of system memory.
    public int numOfClones; //The number of clones presently in the level.
    public int cloneLimit; //Limitation on how many clones there may be in the level. This may be set on a per-level basis.
    public GameObject clone; //For Instantiation of future clones.

    //Other
    Animator anim;
    public List<GameObject> allObjectsToReset; //Used to reset position, rotation, active status, animations and any other
                                            //attributes attached to these objects to their defaults.

    void Start () 
	{
        //Initialization for player
        player = gameObject.GetComponent<Transform>();
        startingPoint = player.position;
        rb = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        item = null;

        //Initialization for clone tracking.
        limit = 3600;   //Limit is set to 3600, exactly a minute's worth of frames at 60 fps.
        origin = new bool[limit, 6];    //Origin is set to have 6 columns, one for each button input kept track of. (W, A, S, D, E, Q)

        allObjectsToReset = new List<GameObject>();
        foreach(GameObject reset in GameObject.FindGameObjectsWithTag("Key"))
        {
            allObjectsToReset.Add(reset);
        }
        foreach (GameObject reset in GameObject.FindGameObjectsWithTag("Guard"))
        {
            allObjectsToReset.Add(reset);
        }
        foreach (GameObject reset in GameObject.FindGameObjectsWithTag("Door"))
        {
            allObjectsToReset.Add(reset);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("w") || Input.GetKey("up"))    //Move up
        {
            //Record action, then do it.
            origin[counter, 0] = true;
            velocity += Vector2.up * speed;
            
            //Controls the camera.
            if(player.position.y - mCamera.position.y > 2)
            {
                mCamera.Translate(0, speed, 0);
            }
        }
        if (Input.GetKey("a") || Input.GetKey("left"))  //Move left
        {
            origin[counter, 1] = true;
            velocity += Vector2.left * speed;
            if (player.position.x - mCamera.position.x < -3.5)
            {
                mCamera.Translate(-speed, 0, 0);
            }
        }
        if (Input.GetKey("s") || Input.GetKey("down"))  //Move down
        {
            origin[counter, 2] = true;
            velocity += Vector2.down * speed;
            if (player.position.y - mCamera.position.y < -2)
            {
                mCamera.Translate(0, -speed, 0);
            }
        }
        if (Input.GetKey("d") || Input.GetKey("right"))     //Move right
        {
            origin[counter, 3] = true;
            velocity += Vector2.right * speed;
            if (player.position.x - mCamera.position.x > 3.5)
            {
                mCamera.Translate(speed, 0, 0);
            }
        }
        rb.MovePosition(rb.position + velocity);    //Movement happens after wasd actions recorded.
        velocity = Vector2.zero;    //then we reset our velocity for next frame.

        if (Input.GetKeyDown("e") || Input.GetKey("enter"))     //Interact
        {
            //Doing stuff in interaction script...
            origin[counter, 4] = true;
        }
        if (Input.GetKey("q"))  //Drop
        {
            origin[counter, 5] = true;
            GameObject.FindGameObjectWithTag("Key").GetComponent<Interaction>().PutDown(gameObject);
        }

        counter++; //Frame over. Next frame...

        if (counter >= limit)
        {
            limit += 3600; //Add a minute
            bool[,] newOrigin = (bool[,])origin.Clone();
            origin = new bool[limit, 6];
            Array.Copy(newOrigin, origin, (((long)limit - 3600) * 6));
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("r")) //Reset level and create clone.
        {
            CreateClone();
        }

        //Animator information
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", input_x);
            anim.SetFloat("y", input_y);
        }

    }

    //Returns counter.
    public ulong GetCounter()
	{
		return counter;
	}

    //Resets all gameobjects to the default position and state in the scene.
    //This includes animations, active state, position, rotation, locked/unlocked, activated/deactived...
    //Created because reloading the scene would break the clones. D;
    public void ResetLevel()
    {
        //Resetting all objects to default state.
        foreach (GameObject reset in allObjectsToReset)
        {
            switch(reset.tag)
            {
                case "Key":
                    reset.transform.position = reset.GetComponent<Interaction>().startingPoint;
                    break;
                case "Door":
                    if (reset.GetComponent<Door>().wasActive)
                    {
                        reset.SetActive(true);
                        Debug.Log("Was active");
                    }
                    else
                        reset.SetActive(false);
                    break;
                case "Switch":
                    //reset switch to default on/off state.
                    break;
                case "Guard":
                    reset.transform.position = reset.GetComponent<Guard>().startingPoint;
                    break;
                default:
                    Debug.Log("Error: Could not reset: ");
                    Debug.Log(reset);
                    break;
            }
        }

        //Resetting player and camera to level start.
        player.position = startingPoint;
        mCamera.position = player.position + Vector3.back;

        //Resetting clone information, getting ready for clone creation.
        counter = 0;
        limit = 3600;
        originForClone = origin;
        origin = new bool[limit, 6];
    }

    //Calculates which clone will be the next replaced/created clone.
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
                if (clone.transform.position.z == -100)
                {
                    nextClone = clone.GetComponent<FollowPath>().cloneNum;
                    return nextClone;
                }
                nextClone++;
            }
        }
        return nextClone;
    }

    //Creates a clone with the smallest clone id
    //not already taken by another clone.
    //Will not create a clone if the limit is reached.
    public void CreateClone()
    {
        ResetLevel(); //Start by resetting the level.

        int nextClone = CalcNextCloneNum();

        if (nextClone < numOfClones || numOfClones == 5) //If there is a destroyed clone
        {
            GameObject[] clones;
            clones = GameObject.FindGameObjectsWithTag("Clone");

            foreach (GameObject clone in clones)
            {
                if (clone.GetComponent<FollowPath>().cloneNum == nextClone) //then if this is the designated next clone, bring him back with new data.
                {
                    clone.transform.Translate(0, 0, 100);
                    clone.GetComponent<FollowPath>().origin = originForClone;
                }
            }
        }
        else if (numOfClones < cloneLimit) //If there isn't a destroyed clone and we haven't reached the clone limit.
        {
            //Add clone
            Instantiate(clone, (Vector3.zero), Quaternion.identity);
            numOfClones++;
        }
        else
        {
            Debug.Log("Clone limit reached. Clone not created.");
        }
    }

    //Used to create a copy of the old origin information.
    //NOT TO BE CONFUSED WITH THE GAMEOBJECT "CLONE"
    public object Clone()
    {
        bool[,] newOrigin = new bool[limit, 6];
        newOrigin = origin;
        return newOrigin;
    }
}
