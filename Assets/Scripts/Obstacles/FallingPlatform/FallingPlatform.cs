using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    [SerializeField] private Animator _anim;

    private float _timeCounter = 0f;
    private bool _changeAxis = false;

    void Update()
    {
        if (!_changeAxis)
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
        _anim.SetBool("on", true);
        _anim.SetFloat("speed", 1.0f);
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
        _anim.SetBool("on", true);
        _anim.SetFloat("speed", -1.0f);
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
}
