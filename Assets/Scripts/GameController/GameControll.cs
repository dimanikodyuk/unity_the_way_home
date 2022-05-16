using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameControll : MonoBehaviour
{
    [SerializeField] private int _livesPlayer;
    [SerializeField] private int _maxLivesPlayer;

    [SerializeField] GameObject _player;

    private const string StorageDataFolder = "C:\\GIT\\unity_the_way_home\\SaveData";
    private const string StorageDataFile = "GameStorageData.json";

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        MenuController.onStartNewGame += StartNewGameHandler;
        MenuController.onRestartGame += RestartGameHandler;
        MenuController.onLoadGame += LoadGameHandler;
        MenuController.onMainMenu += MainMenuHandler;
        MenuController.onSettingNewGame += SettingHandler;
        MenuController.onQuitNewGame += QuitHandler;

        CheckpointControll.onGameSave += PrepateGameStorageData;
    }


    // --- START GAME MENU --- //
    private void StartNewGameHandler()
    {
        StartCoroutine(LoadLevelCoroutine(2, new Vector3(-7.32f, -2.57f, 1)));
    }

    private void RestartGameHandler()
    {
        StartNewGameHandler();
    }

    private void LoadGameHandler()
    {
        LoadGameStorageData();
    }

    private void MainMenuHandler()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SettingHandler()
    {
        
    }

    private void QuitHandler()
    {
        Application.Quit();
    }
    // --- END GAME MENU --- //


    private void PrepateGameStorageData(int scorePoint, int fruits, Vector3 position, int levelNumber)
    {
        var gameStorageData = GetGameStorageData(scorePoint, fruits, position, levelNumber);

        var gameStorageRaw = JsonUtility.ToJson(gameStorageData, true);
        SaveToFile(gameStorageRaw);

        //Debug.Log($"Game Storage data: {gameStorageRaw}");
    }

    

    IEnumerator LoadLevelCoroutine(int levelNum, Vector3 playerPoss)
    {
        SceneManager.LoadScene(levelNum);
        yield return new WaitForSeconds(0.25f);
        if(levelNum != 1)
        {
            Instantiate(_player, playerPoss, Quaternion.identity);
        }
        
    }

    private void LoadGameStorageData()
    {
        var dataRaw = LoadFromFile();
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);
        StartCoroutine(LoadLevelCoroutine(gameStorageData.LevelNumber, gameStorageData.Position));

        //Debug.Log($"[LoadGameStorageData] data: scorePoint: {gameStodateData.ScorePoint}, fruits: {gameStodateData.Fruits}" +
        //    $"Position:{gameStodateData.Position},LevelNumber: {gameStodateData.LevelNumber}");
    }

    private GameStorageData GetGameStorageData(int scorePoint, int fruits, Vector3 position, int levelNumver)
    {
        return new GameStorageData()
        {
            ScorePoint = scorePoint,
            Fruits = fruits,
            Position = position,
            LevelNumber = levelNumver
        };
    }

    private void SaveToFile(string data)
    {
        if (!System.IO.Directory.Exists(StorageDataFolder))
        {
            System.IO.Directory.CreateDirectory(StorageDataFolder);
        }

        var filePath = Path.Combine(StorageDataFolder, StorageDataFile);

        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            byte[] buffer = Encoding.Default.GetBytes(data);
            fileStream.Write(buffer, 0, buffer.Length);
            Debug.Log($"Data {data} was succesful saved, at path: {filePath}");
        }
    }

    private string LoadFromFile()
    {
        var filePath = Path.Combine(StorageDataFolder, StorageDataFile);

        if (!System.IO.File.Exists(filePath))
        {
            Debug.Log($"File at path {filePath} did not exists");
            return null;
        }

        var result = "";

        using (FileStream fileStream = System.IO.File.OpenRead(filePath))
        {
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            result = Encoding.Default.GetString(buffer);
            Debug.Log($"From file {filePath} was loaded data: {result}");
        }
        return result;
    }

}


public class GameStorageData
{
    public int ScorePoint;
    public int Fruits;
    public Vector3 Position;
    public int LevelNumber;
}