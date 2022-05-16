using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _addSpeed;
    [SerializeField] public float _currSpeed;

    [SerializeField] private GameObject[] _objObstacles;

    private void Awake()
    {
        _currSpeed = _minSpeed;
        GenerateObstacles();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currSpeed < _maxSpeed)
        {
            _currSpeed += _addSpeed;
        }
    }

    public void GenerateNextObstacles()
    {
        float randomWait = Random.Range(0.3f, 1.4f);
        Invoke("GenerateObstacles", randomWait);
    }

    void GenerateObstacles()
    {
        int rand = Random.Range(0, _objObstacles.Length);
        GameObject ObstaclesIns = Instantiate(_objObstacles[rand], transform.position, transform.rotation);

        ObstaclesIns.GetComponent<Obstacles>().obstaclesGenerator = this;
    }
}