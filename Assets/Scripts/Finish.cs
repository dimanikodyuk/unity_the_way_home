using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Animator _finishAnimator;
    public static Action<int, int, Vector3, int> onGameSave;

    private Vector3 _currPosition;
    private int test = 2;

    private void Start()
    {
        _currPosition = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_finishAnimator.GetBool("finishActivate"))
        {
            _finishAnimator.SetBool("finishActivate", true);
            SaveData(test, test, _currPosition, test);
        }
    }

    private void SaveData(int scorePoint, int fruits, Vector3 position, int levelNumber)
    {
        onGameSave?.Invoke(scorePoint, fruits, position, levelNumber);
    }
}
