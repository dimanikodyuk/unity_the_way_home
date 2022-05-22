using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsControll : MonoBehaviour
{

    [SerializeField] private int _fruitPoint;
    [SerializeField] private SFXType _collectSFX;

    public static Action<SFXType> onCollectedFruits;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collected(_collectSFX);
            Destroy(gameObject);
            CharacterController.currPlayerScore += _fruitPoint;
        }

    }

    private void Collected(SFXType sfx)
    {
        onCollectedFruits?.Invoke(sfx);
    }
}
