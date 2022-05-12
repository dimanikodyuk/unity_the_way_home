using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // Скорость передвижения
    [SerializeField] private float _jumpSpeed;   // Скорость/сила прыжка
    [SerializeField] Rigidbody2D rigid;        //
    [SerializeField] Animator _anim;           // Аниматор персонажа

    private Vector2 _moveDirection;            // Направление движения
    private bool _isFacingRight = true;        // Проверка правильно ли повёрнут персонаж
    private bool _inAir;                       // Находится ли персонаж в воздухе


    private bool _isGrounded;                  // Определение, каснулись ли мы земли
    private bool _isSolidObject;
    public Transform groundCheck;              // Позиция анализатора земли
    private float _checkRadius = 0.15f;        // Радиус определения земли
    public LayerMask whatIsGround;             // Какой слой является землей
    public LayerMask whatIsSolid;             // Какой слой является землей

    public int allowJump;                 // Количество доступных прыжков
    private int _airJumpCount;                 // Счетчик прыжков

    [SerializeField] GameObject _deathEffect;
    [SerializeField] GameObject _jumpEffect;


    public int i = 0;
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
    }

    public void OnJump(InputAction.CallbackContext context)
    {    
        if (!_inAir)
        {
            _inAir = true;
            if (i % 3 == 0)
            {
                Jump();
            }
            i++;
        }
    }

    private void FixedUpdate()
    {
        Jump();
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _checkRadius, whatIsGround);
        _isSolidObject = Physics2D.OverlapCircle(groundCheck.position, _checkRadius, whatIsSolid);
        if (_isGrounded || _isSolidObject)
        {
            _airJumpCount = 0;
        }
    }

    private void Start()
    {
        _airJumpCount = 0;
    }

    private void Update()
    {
        Move(_moveDirection);
        
        if (_isGrounded || _isSolidObject)
        {
            _airJumpCount = 0;
            i = 0;
        }


        if (transform.position.y < -7)
        {
            MenuController.DiedMenu();
        }
    }

    private void Move(Vector2 direction)
    {
        float scaleMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaleMoveSpeed;
        _anim.SetFloat("Speed", Mathf.Abs(direction.x));

        if (direction.x > 0 && !_isFacingRight)
            Flip();
        else if (direction.x < 0 && _isFacingRight)
            Flip();
    }



    private void Jump()
    {
        if (_inAir)
        {
            _inAir = false;
            if (allowJump <= 0)
                return;

            else if ((_isGrounded || _isSolidObject) && _airJumpCount == 0)
            {
                rigid.velocity += Vector2.up * _jumpSpeed;
                Instantiate(_jumpEffect, transform.position, Quaternion.identity);
            }

            else if (_airJumpCount < allowJump - 1)
            {
                rigid.velocity += Vector2.up * _jumpSpeed;
                Instantiate(_jumpEffect, transform.position, Quaternion.identity);
                _airJumpCount++;
            }    

        }
    }



    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
