using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public ObstaclesGenerator obstaclesGenerator;

    void Update()
    {
        transform.Translate(Vector2.left * obstaclesGenerator._currSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextLine"))
        {
            obstaclesGenerator.GenerateNextObstacles();
        }
        
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        //collision.gameObject.CompareTag("Bunny") ||
    }
}
