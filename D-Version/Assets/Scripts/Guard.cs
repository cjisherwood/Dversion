using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour {

    public Vector3 startPoint;
    public float speed;
    private Vector2 velocity = Vector2.zero;
    private GameObject player;
    private Rigidbody2D rb;
    private GameObject[] clones;
    public static float chaseRange = 5.0f;
    public Transform[] path;

    private Vector3 oldPosition;
    private Vector3 newPosition;
    private Vector3 midPosition;

    Animator anim;

    void Start () 
	{
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        oldPosition= startPoint;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Check the distance of all clones and the player, pick the closest one.
        GameObject closestClone = player;
        clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject clone in clones)
        {
            if (Vector3.Distance(transform.position, clone.transform.position) < Vector3.Distance(GetComponent<Transform>().position, closestClone.transform.position))
            {
                closestClone = clone;
            }
        }

        //Checking if closest clone/player is close to guard. If so, finding direction from guard to the closest clone/player, normalizing and setting speed.
        if (Vector3.Distance(transform.position, closestClone.transform.position) < chaseRange)
        {
            velocity = closestClone.transform.position - transform.position;
            velocity.Normalize();
            velocity = velocity * speed;

            //Moving guard towards closest clone/player in a straight line, reset velocity to zero for next frame.
            rb.MovePosition(rb.position + velocity);
            velocity = Vector2.zero;
        }
        else //If not, returning to next part of patrol path.
        { 
            //Retrace steps
            //etc
        }
        if (Input.GetKey("r"))
        {
            rb.MovePosition(startPoint);
        }
    }

      private void Update()
    {
        oldPosition = midPosition;
        midPosition = newPosition;
        newPosition = transform.position;
        //animator information
        float input_x  = newPosition.x - oldPosition.x;
        float input_y = newPosition.y - oldPosition.y;

        if(input_x > .090)
            input_x = 1;
        else if (input_x < -.090)
            input_x = -1;


        if (input_y > .09)
            input_y = 1;
        else if (input_y < -.09)
            input_y = -1;

        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

        anim.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            anim.SetFloat("x", input_x);
            anim.SetFloat("y", input_y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().CreateClone();
        }
        else if(collision.gameObject.tag == "Clone")
        {
            collision.gameObject.transform.Translate(new Vector3(1000, 1000, -200));
        }
    }
}
