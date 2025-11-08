using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector3          PlayerPos;
    public GameObject       Player;
    public float            _Sensitivity;
    public float            Speed;
    public GameObject[]     PaternList;
    public List<GameObject> InstantiatedPatern;
    public int              PaternRank;
    public Animator         PlayerAnimation;

    private float _speedMultiplier = 2;
    private UiManager _UiManager;
    private bool _play;
    private float _timer = 0;
    [SerializeField] private TextMeshProUGUI _TravellingText;
    void Start()
    {
        _UiManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiManager>();
        _TravellingText.gameObject.SetActive(false);
        Setup();
    }
    private void Update()
    {
        if (_play)
        {
            _timer += Time.deltaTime;
            float speed = PlayerPrefs.GetFloat("Speed");
            float distance = (_timer / 100f) * speed;

            _TravellingText.text = $"{distance:F2} Km";
        }
    }

    void Setup()
    {
        _timer = 0;
        InstantiatedPatern = new List<GameObject>();
        InstantiatedPatern.Clear();
        PaternRank = 0;
        Player.transform.position = PlayerPos;
        for (int i = 0; i < this.PaternList.Length; i++)
        {
            GameObject Obj;
            Obj = Instantiate(this.PaternList[i], new Vector3(0, 0, 40 * i), Quaternion.identity);
            InstantiatedPatern.Add(Obj);
        }
        PlayerPrefs.SetFloat("Speed", Speed);
        _UiManager._Speed.GetComponent<Slider>().value = Speed;

        PlayerPrefs.SetFloat("Sensitivity", _Sensitivity);
        _UiManager._Sensitivity.GetComponent<Slider>().value = _Sensitivity * 10;

        _UiManager._SpeedText.GetComponent<Text>().text = "Speed : " + _UiManager._Speed.GetComponent<Slider>().value.ToString();
        _UiManager._SensitivityText.GetComponent<Text>().text = "Sensivity : " + _UiManager._Sensitivity.GetComponent<Slider>().value.ToString();
        
        this.Speed = 0.0f;
        this._Sensitivity = 0.0f;
    }

    public void ReturnToHome()
    {
        PaternRank = 0;
        PlayerAnimation.Play("Idle_Intro");
        _TravellingText.gameObject.SetActive(false);
        Player.transform.position = PlayerPos;
        Speed = PlayerPrefs.GetFloat("Speed", this.Speed);
        _Sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        
        foreach (var t in InstantiatedPatern)
        {
            Destroy(t);
        }
        Setup();
        _UiManager.EndMenu.GetComponent<Animator>().Play("GameOver_Disappear");
        _UiManager.DisplayStartMenu();
        _UiManager.StartMenu.GetComponent<Animator>().Play("Intro_Appear");
    }
    public void TransitionToGame()
    {
        _UiManager.InGameUI();
        _TravellingText.gameObject.SetActive(true);
        PlayerAnimation.Play("Intro");
    }
    public void StartGame()
    {
        _timer = 0;
        this.Speed = PlayerPrefs.GetFloat("Speed", this.Speed) * _speedMultiplier;
        this._Sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        PaternRank = 0;
        Player.transform.position = PlayerPos;

        for (int i = 0; i < this.PaternList.Length; i++)
        {
            this.InstantiatedPatern[i].transform.position = new Vector3(0, 0, 40 * i);
        }
        //_UiManager.HideAll();
        _play = true;
        _UiManager.EndMenu.SetActive(false);
        PlayerAnimation.SetFloat("RunSpeed", Speed / 3);
        PlayerAnimation.Play("Idle_Run");

    }

    GameObject GetGameobjectchild(GameObject _obj, string _tag)
    {
        GameObject tmpobj = new GameObject();
        for (int i = 0; i < _obj.transform.childCount; i++)
        {
            Transform child = _obj.transform.GetChild(i);
            if (child.tag == _tag)
            {
                tmpobj = child.gameObject;
            }
        }
        return tmpobj;
    }

    public void ChangeSpeed()
    {
        PlayerPrefs.SetFloat("Speed", _UiManager._Speed.GetComponent<Slider>().value);
        _UiManager._SpeedText.GetComponent<Text>().text = "Speed : " + _UiManager._Speed.GetComponent<Slider>().value.ToString();
    }

    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", _UiManager._Sensitivity.GetComponent<Slider>().value * 0.1f);
        _UiManager._SensitivityText.GetComponent<Text>().text = "Sensivity : " + _UiManager._Sensitivity.GetComponent<Slider>().value.ToString();
    }

    public void StopTimer()
    {
        _play = false;
    }
}
