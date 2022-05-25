using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameControll : MonoBehaviour
{
    // PLAYER
    [SerializeField] private int _livesPlayer;
    [SerializeField] private int _maxLivesPlayer;
    [SerializeField] private GameObject _player;
    // SETTING MENU
    [SerializeField] private GameObject _menuSetting;
    [SerializeField] private SFXType _sfxClick;
    [SerializeField] private MusicType _musicMenu;
    [SerializeField] private MusicType _musicGame;
    [SerializeField] private MusicType _dieMenu;
    [SerializeField] private TMP_Text _soundValue;
    [SerializeField] private TMP_Text _sfxValue;

    

    private bool isPaused = false;
    public static Action onChangeVolume;

    public static Action<int> onChangeLive;
    public static Action<int> onChangeScore;


    public void Start()
    {
        PlayMusic(_musicMenu);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_menuSetting);

        MenuController.onStartNewGame += StartNewGameHandler;
        MenuController.onRestartGame += RestartGameHandler;
        MenuController.onLoadGame += LoadGameHandler;
        MenuController.onMainMenu += MainMenuHandler;
        MenuController.onSettingNewGame += SettingHandler;
        MenuController.onQuitNewGame += QuitHandler;
        
        CharacterController.onPaused += PauseMenu;
        CharacterController.onJump += PlayShortSFX;
        CharacterController.onDied += OnDied;

        CheckpointControll.onGameSave += PrepateGameStorageData;
       
        MenuSettings.onBack += BackSetting;
        MenuSettings.onPressed += PlayShortSFX;
        MenuSettings.onChangeMusicVolume += SoundVolumeSetting;
        MenuSettings.onChangeSFXVolume += SFXVolumeSetting;

        FruitsControll.onCollectedFruits += PlayShortSFX;
    }

    public void Update()
    {
       
    }

    // --- START GAME MENU --- //
    private void StartNewGameHandler()
    {
        PlayShortSFX(_sfxClick);
        int levelNum = 2;
        int score = 0;
        int lives = _livesPlayer;
        Vector3 pos = new Vector3(-43.57f, -3.12f, 1);
        StartCoroutine(LoadLevelCoroutine(levelNum, score, lives, pos));
        var gameStorageData = GetGameStorageData(score, lives, pos, levelNum);
        var gameStorageRaw = JsonUtility.ToJson(gameStorageData, true);
        SaveToPrefs(gameStorageRaw);
    }

    private void RestartGameHandler()
    {
        PlayShortSFX(_sfxClick);
        LoadGameStorageData();
    }

    private void LoadGameHandler()
    {      
        PlayShortSFX(_sfxClick);
        LoadGameStorageData();
    }
    


    private void MainMenuHandler()
    {      
        PlayShortSFX(_sfxClick);
        SceneManager.LoadScene("MainMenu");
    }

    private void SettingHandler()
    {
        PlayShortSFX(_sfxClick);
        _menuSetting.SetActive(true);
    }

    private void QuitHandler()
    {
        PlayShortSFX(_sfxClick);
        Application.Quit();
    }
    // --- END GAME MENU --- //

    // --- START SETTING MENU --- //
    private void PauseMenu()
    {
        if (!isPaused)
        {
            _menuSetting.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            _menuSetting.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    private void OnDied()
    {
        SceneManager.LoadScene(1);
    }

    private void SoundVolumeSetting(float musicVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        _soundValue.text = (Math.Round(musicVolume, 2) * 100).ToString();
        ApplySetting();
    }

    private void SFXVolumeSetting(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        _sfxValue.text = (Math.Round(sfxVolume, 2) * 100).ToString();
        ApplySetting();
    }

    private void BackSetting()
    {
        _menuSetting.SetActive(false);
        Time.timeScale = 1;
    }

    private void ApplySetting()
    {
        onChangeVolume?.Invoke();
    }

    // --- END SETTING MENU --- //


    // --- START AUDIO --- //
    public static void PlayShortSFX(SFXType name)
    {
        AudioManager.PlaySFX(name);
    }
  
    public static void PlayMusic(MusicType name)
    {
        AudioManager.PlayMusic(name);
    } 
    
    // --- END AUDIO --- //

    private void PrepateGameStorageData(int scorePoint, int health, Vector3 position, int levelNumber)
    {
        var gameStorageData = GetGameStorageData(scorePoint, health, position, levelNumber);
        var gameStorageRaw = JsonUtility.ToJson(gameStorageData, true);
        SaveToPrefs(gameStorageRaw);
    }

    IEnumerator LoadLevelCoroutine(int levelNum, int scorePoint, int healthCount, Vector3 playerPoss)
    {
        SceneManager.LoadScene(levelNum);
        PlayMusic(_musicGame);
        yield return new WaitForSeconds(0.25f);

        if (levelNum != 0 && GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(_player, playerPoss, Quaternion.identity);
        }
    }


    private void LoadGameStorageData()
    {
        var dataRaw = PlayerPrefs.GetString("SaveData");
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);
        StartCoroutine(LoadLevelCoroutine(gameStorageData.LevelNumber, gameStorageData.ScorePoint, gameStorageData.Health, gameStorageData.Position));
    }

    public static GameStorageData GetGameStorageData(int scorePoint, int health, Vector3 position, int levelNumver)
    {
        return new GameStorageData()
        {
            ScorePoint = scorePoint,
            Health = health,
            Position = position,
            LevelNumber = levelNumver
        };
    }

    private void SaveToPrefs(string data)
    {
            PlayerPrefs.SetString("SaveData", data);      
    }

}


public class GameStorageData
{
    public int ScorePoint;
    public int Health;
    public Vector3 Position;
    public int LevelNumber;
}