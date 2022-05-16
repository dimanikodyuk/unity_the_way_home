using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleControll : MonoBehaviour
{

    [SerializeField] private Animator _animTurtle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _animTurtle.GetBool("Spike") == true)
        {
            //GameControll.currentLives--;
        }
        else if (collision.CompareTag("Player"))
        {
            _animTurtle.SetBool("Spike", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animTurtle.SetBool("Spike", false);
        }
    }
}
