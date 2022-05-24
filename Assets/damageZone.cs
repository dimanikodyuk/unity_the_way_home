using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone : MonoBehaviour
{
    private bool _isDamaged = false;
    IEnumerator TakeDamage()
    {
        _isDamaged = true;
        yield return new WaitForSeconds(0.8f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.8f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.8f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.8f);
        CharacterController.currPlayerLive--;
        yield return new WaitForSeconds(0.8f);
        CharacterController.currPlayerLive--;
    }

    void CheckDamage()
    {
        if (_isDamaged == true)
        {
            StartCoroutine(TakeDamage());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isDamaged = true;
            CheckDamage();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isDamaged = false;
            CheckDamage();
        }
    }
}
