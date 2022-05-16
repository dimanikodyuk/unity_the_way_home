using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator _charAnim;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private TMP_Text _charText;

    private float _checkRadius = 0.15f;
    private bool _isGrounded = false;

    public static int countLives = 3;
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
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    private void Update()
    {
        _charText.text = countLives.ToString();
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb2D.AddForce(Vector2.up * _jumpForce);
            Instantiate(_jumpEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            countLives--;
        }
    }

}
