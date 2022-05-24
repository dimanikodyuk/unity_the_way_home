using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    private float _timeCounter = 0f;
    private bool _changeAxis = false;

    void Update()
    {
        if (_changeAxis == false)
        {
            MotionRight();
        }
        else
        {
            MotionLeft();
        }
    }


    private void MotionRight()
    {
        var normalizedTime = _timeCounter / _duration;
        if (_timeCounter < _duration && _changeAxis == false)
        {
            _target.position = Vector3.Lerp(_startPosition.position, _endPosition.position, normalizedTime);
            _timeCounter += Time.deltaTime;
        }
        else
        {
            _changeAxis = true;
            _timeCounter = 0;
        }
    }


    private void MotionLeft()
    {
        var normalizedTime = _timeCounter / _duration;
        if (_timeCounter < _duration && _changeAxis == true)
        {
            _target.position = Vector3.Lerp(_endPosition.position, _startPosition.position, normalizedTime);
            _timeCounter += Time.deltaTime;
        }
        else
        {
            _changeAxis = false;
            _timeCounter = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterController.currPlayerLive--;
        }
    }

}
