using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckpointControll : MonoBehaviour
{
    [SerializeField] Animator _checkpointAnimator;
    public static Action<int, int, Vector3, int> onGameSave;

    private Vector3 _currPosition;
    private int test = 2;

    private void Start()
    {
        _currPosition = transform.position;    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_checkpointAnimator.GetBool("checkpointActivate"))
        {
            _checkpointAnimator.SetBool("checkpointActivate", true);
            SaveData(CharacterController.currPlayerScore, CharacterController.currPlayerLive, _currPosition, test);
        }
    }

    private void SaveData(int scorePoint, int health, Vector3 position, int levelNumber)
    {
        onGameSave?.Invoke(scorePoint, health, position, levelNumber);
    }
}
