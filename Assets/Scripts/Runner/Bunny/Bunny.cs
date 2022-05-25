using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bunny : MonoBehaviour
{
    [SerializeField] private Animator _bunnyAnim;
    [SerializeField] private float _jumpForce;   
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private Transform _groundCheck;              
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private GameObject[] _livesImages;
    [SerializeField] private SFXType _bunnySFX;
    
    private float _checkRadius = 0.15f;        
    private bool _isGrounded = false;

    public static int countLives = 3;
    private int _tmpLives;

    void Start()
    {
        _bunnyAnim.SetBool("run", true);
        _bunnyAnim.SetFloat("speed", 1.0f);
        _tmpLives = countLives;
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
            _tmpLives = countLives;
        }
           
        if (countLives <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void ChangeLive(int lives)
    {
        for (int i = 0; i < _livesImages.Length; i++)
        {
            if (i > lives - 1)
            {
                _livesImages[i].SetActive(false);
            }
        }
    }

    private void Jump()
    {
       if (_isGrounded )
        {
            AudioManager.PlaySFX(_bunnySFX);
            _rb2D.AddForce(Vector2.up * _jumpForce);
            Instantiate(_jumpEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            Jump();
        }
    }



}
