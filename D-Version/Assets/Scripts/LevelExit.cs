using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private bool levelComplete;

	// Use this for initialization
	void Start ()
    {
        levelComplete = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (levelComplete /*&& animation of elevator has finished*/)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelComplete = true;
        }
    }
}
