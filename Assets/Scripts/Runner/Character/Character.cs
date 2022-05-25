using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator _charAnim;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private GameObject[] _liveImages;

    [SerializeField] private SFXType _playerSFX;

    private float _checkRadius = 0.15f;
    private bool _isGrounded = false;

    public static int countLives = 3;
    private int _tmpLives;

    private int _pressCount = 0;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (_pressCount % 3 == 0)
        {        
            Jump();
        }
        _pressCount++;   
    }

    // Start is called before the first frame update
    void Start()
    {
        _charAnim.SetFloat("Speed", 1.0f);
        ChangeLive(countLives);
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void Update()
    {
        if (_tmpLives != countLives)
        {
            ChangeLive(countLives);
        }

        if (countLives <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void ChangeLive(int livesCount)
    {
        for (int i = 0; i < _liveImages.Length; i++)
        {
            if (i > livesCount - 1)
            {
                _liveImages[i].SetActive(false);
            }
        }
    }


    private void Jump()
    {
        if (_isGrounded)
        {
            AudioManager.PlaySFX(_playerSFX);
            _rb2D.AddForce(Vector2.up * _jumpForce);
            Instantiate(_jumpEffect, transform.position, Quaternion.identity);
        }
    }

}
