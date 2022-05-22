using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistingObject : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Animator _objectAnimator;
    [SerializeField] private int demage;

    private void Update()
    {
        _objectAnimator.SetBool("on", true);
        float scaleMoveSpeed = _rotateSpeed * Time.deltaTime;
        transform.Rotate(0, 0, 10 * scaleMoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterController.currPlayerLive -= demage;
        }
    }
}
