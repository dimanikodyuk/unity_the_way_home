using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _startTimeBtwShoots;
    [SerializeField] private GameObject _deadEffect;

    private float _timeBtwShoots;
    private bool _isAttack = false;
    private bool _isDead = false;

    private void Update()
    {
        if (_isAttack)
        {
            if (_timeBtwShoots <= 0)
            {
                Shoot();
            }
            else
            {
                _timeBtwShoots -= _timeBtwShoots;
            }
        }

        if (_isDead)
        {
            _isAttack = false;
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Vector3 movement = new Vector3(Random.Range(1, 3), Random.Range(-6, -10), -1);
            transform.position = transform.position + movement * Time.deltaTime;
        }
    }

    public void Dead()
    {
        _isDead = true;
    }

    private void Shoot()
    {
        Instantiate(_bullet, _shootPoint.position, transform.rotation);
        _timeBtwShoots = _startTimeBtwShoots;
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(1f);
        Shoot();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Shoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

}
