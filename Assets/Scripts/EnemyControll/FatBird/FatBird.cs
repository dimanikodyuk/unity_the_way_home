using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : MonoBehaviour
{
    [SerializeField] private SFXType _birdFall;
    [SerializeField] private SFXType _birdTrigger;
    [SerializeField] private int _damagePoint;

    public void BirdFall()
    {
        AudioManager.PlaySFX(_birdFall);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.StopSFX();
            AudioManager.PlaySFX(_birdTrigger);
            CharacterController.currPlayerLive = CharacterController.currPlayerLive - _damagePoint;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            AudioManager.StopSFX();
            AudioManager.PlaySFX(_birdTrigger);
            Destroy(gameObject);
        }

    }
}
