using System;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] Vector2 offset;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        SetRespawnPoint(other);
    }

    private void SetRespawnPoint(Collider2D collider2D )
    {
        collider2D.gameObject.GetComponent<PlayerManagerOM>().respawnPoint = transform.position + (Vector3)offset;
    }
}
