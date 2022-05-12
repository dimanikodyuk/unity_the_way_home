using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControll : MonoBehaviour
{
    [SerializeField] private int _livesPlayer;
    [SerializeField] private int _maxLivesPlayer;

    public static int currentLives;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = _livesPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealt(currentLives, _maxLivesPlayer);
    }

    private void CheckHealt(int curLives, int _maxLives)
    {
        if (curLives <= 0)
        {
            MenuController.DiedMenu();
        }
        else if (curLives > _maxLives)
        {
            currentLives = _maxLives;
        }
    }
}
