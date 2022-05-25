using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{

    [SerializeField] private Button _nextLevel;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private MusicType _congratulationMusic;

    public static Action onAllSoundStop;
    public static Action onNextLevel;
    public static Action onMenu;

    //public static Action 

    void Start()
    {
        StopMusic();
        AudioManager.PlayMusic(_congratulationMusic);
        _nextLevel.onClick.AddListener(NextLevel);
        _mainMenu.onClick.AddListener(MainMenu);

        var dataRaw = PlayerPrefs.GetString("SaveData");
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);
        _score.text = gameStorageData.ScorePoint.ToString();
    }

    private void NextLevel()
    {
        onNextLevel?.Invoke();
    }

    private void MainMenu()
    {
        onMenu?.Invoke();
    }

    private void StopMusic()
    {
        onAllSoundStop?.Invoke();
    }
}
