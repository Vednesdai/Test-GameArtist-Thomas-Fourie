using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLevel : MonoBehaviour
{
    private Vector3 Pos;
    private GameManager _gamemanager;

    void Start()
    {
        _gamemanager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }
	
	void Update ()
    {
        Pos = this.transform.position;
        Pos.z = Pos.z - _gamemanager.Speed * Time.deltaTime;
        this.transform.position = Pos;
    }
}