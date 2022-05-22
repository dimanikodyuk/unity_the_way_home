using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private string _playerTag;
    [SerializeField] private float _speedMove;

    private void Awake()
    {
        StartCoroutine(Test());
    }


    IEnumerator Test()
    {
        yield return new WaitForSeconds(0.25f);
        if (GameObject.FindGameObjectWithTag(_playerTag) != null)
        {
            _playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
        }
        else
        {
            _playerTransform = this.gameObject.transform;
        }
        
        transform.position = new Vector3()
        {
            x = _playerTransform.position.x,
            y = _playerTransform.position.y + 2,
            z = _playerTransform.position.z - 10
        };
    }

    private void Update()
    {
        if (_playerTransform)
        {
            Vector3 target = new Vector3()
            {
                x = _playerTransform.position.x,
                y = _playerTransform.position.y + 2,
                z = _playerTransform.position.z - 10
            };

            Vector3 pos = Vector3.Lerp(transform.position, target, _speedMove * Time.deltaTime);

            transform.position = pos;
        }
    }
}
