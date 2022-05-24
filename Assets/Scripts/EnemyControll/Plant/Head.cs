using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Animator _anim;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _anim.SetBool("attack", false);
            _anim.SetBool("idle", false);
            _anim.SetBool("hit", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _anim.SetBool("idle", true);
            _anim.SetBool("hit", false);
            _anim.SetBool("attack", false);
        }
    }
}
