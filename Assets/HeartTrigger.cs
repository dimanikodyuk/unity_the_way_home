using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (CharacterController.currPlayerLive < 3)
            {
                CharacterController.currPlayerLive++;
                Destroy(gameObject);
            }
        }
    }
}
