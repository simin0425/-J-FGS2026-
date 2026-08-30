using System;
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float FallHeight = -11.0f;
    [SerializeField] float FallHeightTop = 22.0f;
    
    private PlayerManagerOM playerManager;
    private Rigidbody2D rb;

    private void Start()
    {
        playerManager = player.gameObject.GetComponent<PlayerManagerOM>();
        rb = player.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player.position.y < FallHeight || player.position.y > FallHeightTop)
        { 
            rb.linearVelocity = Vector3.zero;
            player.transform.position = playerManager.respawnPoint;
            playerManager.Damage(1);
        }
    }
}
