using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGameEvent : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UiManager _UiManager;

    public void LaunchGame()
    {
        _gameManager.StartGame();
    }
    
    public void OpenGameOver()
    {
        _UiManager.DisplayEndMenu();
        _UiManager.EndMenu.GetComponent<Animator>().Play("GameOver_Appear");
    }
}
