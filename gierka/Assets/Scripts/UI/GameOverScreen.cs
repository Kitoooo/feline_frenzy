using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen instance { get; private set; }
    [SerializeField]
    protected GameObject screen;
    void Awake()
    {
        instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {

    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Show()
    {
        screen.SetActive(true);
    }

    public void Hide()
    {
        screen.SetActive(false);
    }
}
