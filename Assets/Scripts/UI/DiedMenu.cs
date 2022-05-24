using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedMenu : MonoBehaviour
{
    [SerializeField] private MusicType _music;
    public static Action onStopAllSounds;
    // Start is called before the first frame update
    void Start()
    {
        StopMusic();
        AudioManager.PlayMusic(_music);    
    }

    private void StopMusic()
    {
        onStopAllSounds?.Invoke();
    }

}
