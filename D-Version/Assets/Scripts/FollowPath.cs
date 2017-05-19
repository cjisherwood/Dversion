using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
    GameObject[] clones;
    public int cloneNum;
    public bool[,] origin;
    private Player player;
    public ulong counter;
    private float speed;
    private string input;

    public GameObject item;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        speed = 0.05f;//FIX
        
        item = gameObject;

        origin = player.GetComponent<Player>().originForClone;
        cloneNum = player.GetComponent<Player>().CalcNextCloneNum();
    }
    
    // Update is called once per frame
    void FixedUpdate()
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
        if (origin[counter, 4])
        {
            GameObject.FindGameObjectWithTag("Key").GetComponent<Interaction>().Interact(gameObject);
        }
        if (origin[counter, 5])
        {
            GameObject.FindGameObjectWithTag("Key").GetComponent<Interaction>().PutDown(gameObject);
        }
        counter++;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            //Reset clone to level start
            if (gameObject.transform.position.z == -1)
                gameObject.GetComponent<Transform>().position = Vector3.back;

            else
                gameObject.GetComponent<Transform>().position = Vector3.zero;

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
        clones = GameObject.FindGameObjectsWithTag("Clone");
        
        if (cloneNum == num)
        {
            foreach (GameObject clone in clones)
            {
                //Reset clone to level start
                if (clone.transform.position.z == -1)
                    clone.GetComponent<Transform>().position = Vector3.back;

                else
                    clone.GetComponent<Transform>().position = Vector3.zero;

                Debug.Log(clone.GetComponent<FollowPath>().cloneNum);
                clone.GetComponent<FollowPath>().counter = 0;
            }
            player.ResetLevel();
            Debug.Log("Bye bye!");
            DestroyClone();
        }
    }
    
    private void DestroyClone()
    {
        gameObject.GetComponent<Transform>().Translate(0, 0, -1);
    }
}