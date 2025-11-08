using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject EndMenu;
    public GameObject StartMenu;
    public GameObject SettingsMenu;

    public GameObject _Sensitivity;
    public GameObject _SensitivityText;
    public GameObject _Speed;
    public GameObject _SpeedText;

    private void Start()
    {
        DisplayStartMenu();
    }

    public void InGameUI()
    {
        StartMenu.GetComponent<Animator>().Play("Intro_Disappear");
    }
    public void DisplayStartMenu()
    {
        StartMenu.SetActive(true);
    }

    public void ExitSettingsMenu()
    {
        Time.timeScale = 1;
        SettingsMenu.GetComponent<Animator>().Play("Settings_Disappear");
    }
    public void DisplayEndMenu()
    {
        EndMenu.SetActive(true);
    }

    public void DisplaySettingsMenu()
    {
        SettingsMenu.SetActive(true);
        SettingsMenu.GetComponent<Animator>().Play("Settings_Appear");
        Time.timeScale = 0;
    }
}
