﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeControll : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetHeightPos;
    [SerializeField] private float _targetSidePos;
    [SerializeField] private bool _useMotionTarget = false;
    [SerializeField] private Animator _beeAnim;
    [SerializeField] private GameObject _beeBullet;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _timeBtwShots;
    [SerializeField] private float _startTimeBtwShots;
    

    void Start()
    {
        
    }

  
    void Update()
    {
        CheckTarget();
        CheckAttack();
        
        if (_useMotionTarget)
            return;
    }

    private void Motion(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void CheckTarget()
    {
        if (!_useMotionTarget )
        {
            return;
        }
        var direction = new Vector3(_target.position.x - transform.position.x, _target.position.y + _targetHeightPos - transform.position.y, 0);
        direction = direction.normalized;
        direction = direction.normalized;
        Motion(direction);
    }


    private void CheckAttack()
    {
        if ((transform.position.y - _target.position.y) <= _targetHeightPos && Mathf.Abs(transform.position.x - _target.position.x) < _targetSidePos)
        {
            if (_timeBtwShots <= 0)
            {
                _beeAnim.SetBool("itsAttack", true);
                Instantiate(_beeBullet, _shootPoint.position, transform.rotation);
                _timeBtwShots = _startTimeBtwShots;

            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
                _beeAnim.SetBool("itsAttack", false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _useMotionTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _useMotionTarget = false;
        }
    }
}