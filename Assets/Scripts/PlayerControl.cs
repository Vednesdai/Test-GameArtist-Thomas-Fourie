using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float OldXPos;
    private GameManager _gamemanager;
    private UiManager _UiManager;
    
    private Animator animator = null;
    [SerializeField] private GameObject _playerModel;
    private void Awake()
    {
        animator = _playerModel.GetComponent<Animator>();
    }

    void Start()
    {
        _gamemanager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        _UiManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiManager>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OldXPos = Input.mousePosition.x;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
#endif
        {
            float _Delta;
            _Delta = Input.mousePosition.x - OldXPos;
            Vector3 tmp = this.transform.position;
            tmp.x += _Delta * _gamemanager._Sensitivity * Time.fixedDeltaTime;
            this.transform.position = tmp;
            OldXPos = Input.mousePosition.x;
        }
        if (this.transform.position.x > 2)
        {
            Vector3 Pos = this.transform.position;
            Pos.x = 2.0f;
            this.transform.position = Pos;
        }
        else if (this.transform.position.x < -2.0f)
        {
            Vector3 Pos = this.transform.position;
            Pos.x = -2.0f;
            this.transform.position = Pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            this._gamemanager._Sensitivity = 0.0f;
            this._gamemanager.Speed = 0.0f;
            _gamemanager.PlayerAnimation.Play("Crash");
            _gamemanager.StopTimer();
        }

        if (other.tag == "Jump")
        {
            animator.SetTrigger("Jump");
        }
    }
}