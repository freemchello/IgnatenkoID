using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _quitGameButton;

    public Slider _sl;


    private void Awake()
    {
        _quitGameButton.onClick.AddListener(() => { Application.Quit(); });
        _startGameButton.onClick.AddListener(StartGame);

        

        //_sl.onValueChanged.AddListener(value =>
        //{
        //    //Debug.Log(value);
        //});
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else if (Time.timeScale == 1)
            Time.timeScale = 0;
    }
    
}
