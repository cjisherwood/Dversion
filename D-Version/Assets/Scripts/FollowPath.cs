using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

    public int cloneNum;
    private bool[,] origin;
    private Player player;
	private ulong counter;
	private float speed;
    private string input;
    private GameObject[] clones;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Checking clone num.");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
		speed = 0.05f;//FIX
        clones = GameObject.FindGameObjectsWithTag("Clone");

		origin = player.GetComponent<Player>().originForClone;
        cloneNum = player.GetComponent<Player>().numOfClones; //player.GetComponent<Player>().CalcNextCloneNum();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
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

    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            //Reset clone to level start
            gameObject.GetComponent<Transform>().position = new Vector3(0, 3, 0);
            counter = 0;
        }

        //If you hit the number keys, destroy old clones and remap.
        if (Input.GetKeyDown("1"))
        {
            Numbers(1);
        }
        if (Input.GetKeyDown("2"))
        {
            Numbers(2);
        }
        if (Input.GetKeyDown("3"))
        {
            Numbers(3);
        }
        if (Input.GetKeyDown("4"))
        {
            Numbers(4);
        }
        if (Input.GetKeyDown("5"))
        {
            Numbers(5);
        }
        if (Input.GetKeyDown("6"))
        {
            Numbers(6);
        }
        if (Input.GetKeyDown("7"))
        {
            Numbers(7);
        }
        if (Input.GetKeyDown("8"))
        {
            Numbers(8);
        }
        if (Input.GetKeyDown("9"))
        {
            Numbers(9);
        }
    }

    private void Numbers(int num)
    {
        if (cloneNum == num)
        {
            foreach (GameObject clone in clones)
            {
                //Reset clone to level start
                clone.GetComponent<Transform>().position = new Vector3(0, 3, 0);
                counter = 0;
            }
            player.ResetLevel();
            GameObject.Destroy(gameObject, 0);
            player.DestroyClone();
        }
    }
}
