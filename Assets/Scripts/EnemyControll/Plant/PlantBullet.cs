using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _whatIsSolid;

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance, _whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                CharacterController.currPlayerLive--;
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * _speedBullet * Time.deltaTime);

    }
}
