using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bunny : MonoBehaviour
{
    [SerializeField] private Animator _bunnyAnim;
    [SerializeField] private float _jumpForce;   
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private Transform _groundCheck;              
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private TMP_Text _bunnyText;
    
    private float _checkRadius = 0.15f;        
    private bool _isGrounded = false;

    public static int countLives = 3;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _bunnyAnim.SetBool("run", true);
        _bunnyAnim.SetFloat("speed", 1.0f);
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void Update()
    {
        _bunnyText.text = countLives.ToString();
    }

    private void Jump()
    {
       if (_isGrounded )
        {
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
