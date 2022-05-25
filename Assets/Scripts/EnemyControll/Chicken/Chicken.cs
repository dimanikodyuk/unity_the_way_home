using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private SFXType _sfxRino;
    [SerializeField] private Animator _anim;
    [SerializeField] private string _playerTag;

    private Transform _target;
    private bool _isFacingRight = true;
    private bool _isDead = false;

    private void Awake()
    {
        StartCoroutine(ChickenTarget());
    }

    IEnumerator ChickenTarget()
    {
        yield return new WaitForSeconds(2.0f);
        if (GameObject.FindGameObjectWithTag(_playerTag) != null)
        {
            _target = GameObject.FindGameObjectWithTag(_playerTag).transform;
        }
    }

    public void SFXChiken()
    {
        AudioManager.PlaySFX(_sfxRino);
    }

    void Update()
    {
        if (_isDead)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Vector3 movement = new Vector3(Random.Range(1, 3), Random.Range(-6, -10), -1);
            transform.position = transform.position + movement * Time.deltaTime;
        }

        if (_target != null)
        {
            CheckTarget();
        }
        if (_anim.GetBool("attack"))
            return;
    }

    private void Motion(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;

        if (direction.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    public void Dead()
    {
        _isDead = true;
    }


    private void CheckTarget()
    {
        if (!_anim.GetBool("attack"))
            return;

        var direction = _target.position - transform.position;
        direction = direction.normalized;
        Motion(direction);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _anim.SetBool("idle", false);
            _anim.SetBool("attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _anim.SetBool("attack", false);
            _anim.SetBool("idle", true);
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
