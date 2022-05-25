using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _restartGame;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _quitGame;
    [SerializeField] private Button _loadGame;
    [SerializeField] private Button _settingGame;

  
    public static Action onStartNewGame;
    public static Action onRestartGame;
    public static Action onLoadGame;
    public static Action onMainMenu;
    public static Action onSettingNewGame;
    public static Action onQuitNewGame;

    public static Action onNextLevel;

    private void Start()
    {
        _newGame.onClick.AddListener(NewGameHandler);
        _restartGame.onClick.AddListener(RestartGameHandler);
        _mainMenu.onClick.AddListener(MainMenuHandler);
        _quitGame.onClick.AddListener(QuitGameHandler);
        _loadGame.onClick.AddListener(LoadGameHandler);
        _settingGame.onClick.AddListener(SettingsHandler);
    }

    public void NewGameHandler()
    {
        onStartNewGame?.Invoke();
    }

    public void RestartGameHandler()
    {
        onRestartGame?.Invoke();
    }

    public void MainMenuHandler()
    {
        onMainMenu?.Invoke();
    }

    public void QuitGameHandler()
    {
        onQuitNewGame?.Invoke();
    }

    public void LoadGameHandler()
    {
        onLoadGame?.Invoke();
    }

    public void SettingsHandler()
    {
        onSettingNewGame?.Invoke();
    }
}
