using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadScreen(string sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void LoadOptions()
    {

    }

    public void LoadExit()
    {

    }
}
