using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirePlatform : MonoBehaviour
{

    [SerializeField] private SFXType _fireSFX;
    [SerializeField] private SFXType _platformSFX;

    private Animator _fireAnimator;
    private bool _isFire = false;

    // Start is called before the first frame update
    void Start()
    {
        _fireAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    void CheckFire()
    {
        if (_isFire == true)
        {
            StartCoroutine(StartFire());
        }
        else
        {
            _fireAnimator.SetBool("fire_on", false);
            _fireAnimator.SetBool("fire_hit", false);
        }
    }

    IEnumerator StartFire()
    {    
        _isFire = true;
        
        _fireAnimator.SetBool("fire_hit", true);
        yield return new WaitForSeconds(0.9f);
        AudioManager.PlaySFX(_fireSFX);
        _fireAnimator.SetBool("fire_on", true);
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.5f);
        CharacterController.currPlayerLive--;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.PlaySFX(_platformSFX);
            _isFire = true;
            CheckFire();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isFire = false;
            StopAllCoroutines();
            CheckFire();
        }
    }


}
