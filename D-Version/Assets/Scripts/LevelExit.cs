using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //private bool levelComplete;
    private Collider2D exitCol;
    Animator elevatorAnim;
    private SpriteRenderer elevator;
    MenuController sounds;

	// Use this for initialization
	void Start ()
    {
        //levelComplete = false;
        elevatorAnim = gameObject.GetComponent<Animator>();
        exitCol = GetComponentInChildren<Collider2D>();
        elevator = gameObject.GetComponent<SpriteRenderer>();
        sounds = GameObject.Find("PauseFromMainMenu").GetComponent<MenuController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
  
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sounds.elevatorSound.Play();
            ElevatorAnim("Open");
            StartCoroutine(StartTimer());
        }
    }

    public void ElevatorAnim(string state)
    {
        elevatorAnim.SetTrigger(state);
    }

    public IEnumerator StartTimer()
    {
        for (int i = 0; i <= 5; i++)
        {
            Debug.Log("Starting timer at " + i);
            yield return new WaitForSecondsRealtime(1);

            if (i == 3)
            {
                ElevatorAnim("Close");
                //levelComplete = true;
                elevator.sortingLayerName = "Default";
                elevator.sortingOrder = 10;
            }
            if (i == 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
