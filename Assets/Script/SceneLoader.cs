using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Play()
    {
        Invoke("LoadFirstScene", 3f);
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Exit();
    }
}
