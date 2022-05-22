using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : MonoBehaviour
{
    [SerializeField] private SFXType _birdFall;
    [SerializeField] private SFXType _birdTrigger;
    [SerializeField] private int _damagePoint;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlaySFX(_birdFall);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.PlaySFX(_birdTrigger);
            CharacterController.currPlayerLive = CharacterController.currPlayerLive - _damagePoint;
        }
        else if (collision.gameObject.tag == "Ground")
        {
            AudioManager.PlaySFX(_birdTrigger);
            Destroy(gameObject);
        }

    }
}
