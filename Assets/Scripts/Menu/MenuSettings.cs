using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private Slider _sound;
    [SerializeField] private Slider _sfx;
    [SerializeField] private Button _back;
    [SerializeField] private TMP_Text _soundText;
    [SerializeField] private TMP_Text _sfxText;
    [SerializeField] private SFXType _buttonSFX;

    public static Action onBack;
    public static Action<SFXType> onPressed;
    public static Action<float> onChangeMusicVolume;
    public static Action<float> onChangeSFXVolume;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _sound.value = PlayerPrefs.GetFloat("MusicVolume");
        _sfx.value = PlayerPrefs.GetFloat("SFXVolume");
        _soundText.text = (Math.Round(_sound.value, 2) * 100).ToString();
        _sfxText.text = (Math.Round(_sfx.value, 2) * 100).ToString();

        _back.onClick.AddListener(BackButton);
        _sound.onValueChanged.AddListener(SoundChange);
        _sfx.onValueChanged.AddListener(SFXChange);
    }

    private void SoundChange(float musicVolume)
    {
        onChangeMusicVolume?.Invoke(musicVolume);
    }
    
    private void SFXChange(float sfxVolume)
    {
        onChangeSFXVolume?.Invoke(sfxVolume);
    }
    
    private void BackButton()
    {
        onBack?.Invoke();
        onPressed?.Invoke(_buttonSFX);
    }


}
