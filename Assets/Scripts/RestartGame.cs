using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
       else if (Time.timeScale == 1)
            Time.timeScale = 0;
    }

}
