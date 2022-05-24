using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampButton : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    [SerializeField] private GameObject _currCamera;
    [SerializeField] private GameObject _doorCamera;

    private Animator _anim;
    private float _timeCounter = 0f;

    public static bool _doorOpen = false;
    public static bool _doorClose = false;

    private void UnMotion()
    {
        Debug.Log("DOOR OPEN");
        var normalizedTime = _timeCounter / _duration;
        if (_timeCounter < _duration)
        {
            _target.position = Vector3.Lerp(_startPosition.position, _endPosition.position, normalizedTime);
            _timeCounter += Time.deltaTime;
        }
        else
        {
            _timeCounter = 0;
            _doorOpen = false;
            _doorClose = false;
        }
    }

    private void Motion()
    {
        Debug.Log("DOOR CLOSES");
        var normalizedTime = _timeCounter / _duration;
        if (_timeCounter < _duration)
        {
            _target.position = Vector3.Lerp(_endPosition.position, _startPosition.position, normalizedTime);
            _timeCounter += Time.deltaTime;
        }
        else
        {
            _timeCounter = 0;
            _doorOpen = false;
            _doorClose = false;

            _currCamera.SetActive(true);
            _doorCamera.SetActive(false);
        }
    }

    private void OpenDoor()
    {
        if (!_doorOpen)
        {
            _doorClose = false;
            _doorOpen = true;
        }
    }

    private void CloseDoor()
    {
        if (_doorOpen)
        {
            _doorOpen = false;
            _doorClose = true;
        }
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_doorOpen)
        {
            _currCamera.SetActive(false);
            _doorCamera.SetActive(true);
            Motion();
        }
        else if (_doorClose)
        {
            UnMotion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            _anim.SetBool("on", true);
            _anim.SetBool("off", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            _anim.SetBool("on", false);
            _anim.SetBool("off", true);
        }
    }
}
