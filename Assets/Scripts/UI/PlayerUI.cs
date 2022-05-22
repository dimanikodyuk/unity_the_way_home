using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _liveImage;
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        var dataRaw = PlayerPrefs.GetString("SaveData");
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);

        CharacterController.onChangeLive += ChangeLive;
        CharacterController.onChangeScore += ChangeScore;

        ChangeLive(gameStorageData.Health);
        ChangeScore(gameStorageData.ScorePoint);
         
    }

    private void OnDestroy()
    {
        CharacterController.onChangeLive -= ChangeLive;
        CharacterController.onChangeScore -= ChangeScore;
    }

    private void ChangeLive(int liveCount)
    {
        for (int i = 0; i < _liveImage.Length; i++)
        {
            if (i > liveCount-1)
            {
                _liveImage[i].SetActive(false);
            }
        }
    }

    private void ChangeScore(int scoreCount)
    {
        _scoreText.text = scoreCount.ToString();
    }

}
