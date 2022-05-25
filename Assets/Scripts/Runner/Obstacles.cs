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
        if (collision.gameObject.tag == "NextLine")
        {
            obstaclesGenerator.GenerateNextObstacles();
        }
        
        if (collision.gameObject.tag == "FinishLine")
        {
            Destroy(gameObject);
        }

        //if (collision.gameObject.tag == "Player")
        //{
        //    Destroy(gameObject);
        //    Character.countLives--;
        //}

        //if (collision.gameObject.tag == "Bunny")
        //{
        //    Bunny.countLives--;
        //}
    }
}
