using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    // Player parameters
    [SerializeField] private float _moveSpeed;              // Скорость передвижения
    [SerializeField] private float _jumpSpeed;              // Скорость/сила прыжка
    [SerializeField] private Rigidbody2D rigid;             //
    [SerializeField] private Animator _anim;                // Аниматор персонажа
    // Effects
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _jumpEffect;
    // WallCheker
    [SerializeField] private Transform _wallCheker;
    // Thought
    [SerializeField] private GameObject _thoughtCanvas;
    [SerializeField] private TMP_Text _thoughtText;
    [SerializeField] private string[] _textThought;
    [SerializeField] private SFXType _jumpSFX;
    private bool _textActive = false;

    private Vector2 _moveDirection;            // Направление движения
    private bool _isFacingRight = true;        // Проверка правильно ли повёрнут персонаж
    private bool _inAir;                       // Находится ли персонаж в воздухе

    private bool _isGrounded;                  // Определение, каснулись ли мы земли
    private bool _isSolidObject;
    private bool _isWallObject;
    public Transform groundCheck;              // Позиция анализатора земли
    private float _checkRadius = 0.10f;        // Радиус определения земли
    
    public LayerMask whatIsGround;             // Какой слой является землей
    public LayerMask whatIsSolid;              // Какой слой является твердым
    public LayerMask whatIsWall;               // Какой слой является стеной

    public int allowJump;                      // Количество доступных прыжков
    private int _airJumpCount;                 // Счетчик прыжков

    public static int currPlayerLive;
    private int playerLive = 0;
    public static int currPlayerScore;
    private int playerScore = 0;

    public static Action onDied;
    public static Action onPaused;

    public static Action<int> onChangeLive;
    public static Action<int> onChangeScore;

    public static Action<SFXType> onJump;

    private bool _isDead = false;

    private int i = 0;

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

    public void MenuSetting(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            onPaused?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Jump();
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _checkRadius, whatIsGround);
        _isSolidObject = Physics2D.OverlapCircle(groundCheck.position, _checkRadius, whatIsSolid);
        _isWallObject = Physics2D.OverlapCircle(_wallCheker.position, _checkRadius, whatIsWall);

        if (_isWallObject && !_isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -0.3f);
            _anim.SetBool("isWall", true);
            ThoughtText();
        }
        else
        {
            _thoughtCanvas.SetActive(false);
            _textActive = false;
        }

        if (_isGrounded || _isSolidObject)
        {
            _airJumpCount = 0;
            _anim.SetBool("isJumped", false);
        }
    }

    private void Start()
    {
        _isDead = false;
        _airJumpCount = 0;

        var dataRaw = PlayerPrefs.GetString("SaveData");
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);
        currPlayerLive = gameStorageData.Health;
        currPlayerScore = gameStorageData.ScorePoint;

        playerLive = currPlayerLive;
        playerScore = currPlayerScore;
    }

    public void Dead()
    {
        _isDead = true;
    }

    private void Update()
    {
        Move(_moveDirection);
        
        if (_isGrounded || _isSolidObject)
        {
            _anim.SetBool("isJumped", false);
            _anim.SetBool("isWall", false);
            _airJumpCount = 0;
           // i = 0;
        }

        if (playerLive != currPlayerLive)
        {
            ChangeLive(currPlayerLive);
            playerLive = currPlayerLive;
        }
        
        if (playerLive <= 0)
        {
            _anim.Play("player_desappearing");
            if (_isDead)
            {
                onDied?.Invoke();
            }
        }

        if (playerScore != currPlayerScore)
        {
            ChangeScore(currPlayerScore);
            playerScore = currPlayerScore;
        }

        if (gameObject.transform.position.y < -6)
        {
            onDied?.Invoke();
        }

    }

    
    private void ChangeLive(int live)
    {
        onChangeLive?.Invoke(live);
    }
    
    private void ChangeScore(int scorePoint)
    {
        onChangeScore?.Invoke(scorePoint);
    }
    
    private void ThoughtText()
    {
        if(!_textActive)
        {
            _textActive = true;
            _thoughtCanvas.SetActive(true);
            int textIndex = UnityEngine.Random.Range(0, _textThought.Length);
            _thoughtText.text = _textThought[textIndex].ToString();
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
                onJump?.Invoke(_jumpSFX);
                rigid.velocity += Vector2.up * _jumpSpeed;
                Instantiate(_jumpEffect, transform.position, Quaternion.identity);
                _anim.SetBool("isJumped", true);
            }

            else if (_airJumpCount < allowJump - 1)
            {
               
                onJump?.Invoke(_jumpSFX);
                rigid.velocity += Vector2.up * _jumpSpeed;
                Instantiate(_jumpEffect, transform.position, Quaternion.identity);
                _airJumpCount++;
                _anim.SetBool("isJumped", true);
            }    

        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        _thoughtCanvas.transform.localScale = theScale;   
    }


}
