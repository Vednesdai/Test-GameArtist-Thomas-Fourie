using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    private GameManager _gamemanager;
    
	private void Start()
	{
        _gamemanager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Switch")
        {
            Vector3 Pos = new Vector3();
            if (_gamemanager.PaternRank == 0)
            {
                Pos = _gamemanager.InstantiatedPatern[_gamemanager.PaternList.Length - 1].transform.position;
                Pos.z = Pos.z + 40.0f;
                this.transform.parent.localPosition = Pos;
                _gamemanager.PaternRank++;
            }
            else
            {
                Pos = _gamemanager.InstantiatedPatern[_gamemanager.PaternRank - 1].transform.position;
                Pos.z = Pos.z + 40.0f;
                this.transform.parent.localPosition = Pos;

                if (_gamemanager.PaternRank < _gamemanager.PaternList.Length - 1)
                    _gamemanager.PaternRank++;
                else
                    _gamemanager.PaternRank = 0;
            }
        }
	}
}