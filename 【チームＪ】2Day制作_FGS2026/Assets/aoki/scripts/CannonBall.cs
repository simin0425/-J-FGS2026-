using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CannonBall : MonoBehaviour, IReversible
{
    [Header("コンポーネント")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rigidbody;
    [Header("移動")]
    [SerializeField][Tooltip("x-が左向き")] private Vector3 moveDir;
    [SerializeField] private float moveSpeed;
    [SerializeField] private UnityEngine.Transform playerTransform;
    [SerializeField] private float moveStartRange =12f;
    [SerializeField]private bool shouldMove = false;

    [SerializeField] private AudioClip boomSe;
    [SerializeField] private AudioClip moveSe;

    private Vector3 startPos;
    


    protected bool isRevered = false;

    bool IReversible.isRevered { get; set; }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(startPos, this.transform.position) > 20f)
        {
            SoundManager.Instance.PlaySE(boomSe);
            Destroy(gameObject);
            return;
        }
        if (shouldMove)
        {
            rigidbody.linearVelocity = moveDir * moveSpeed * (isRevered ? -1f : 1f);
            return;
        }

        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);

        if (distance < moveStartRange)
        {
            shouldMove = true;
        }

        rigidbody.SetRotation(Vector2.SignedAngle(playerTransform.right, rigidbody.linearVelocity));
    }

    private void OnReversed()
    {
        isRevered = true;
    }

    void IReversible.OnReversed()
    {
        OnReversed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player
        //破壊可能オブジェクト
        PlayerManagerOM playerManager = collision.gameObject.GetComponent<PlayerManagerOM>();
        if (playerManager != null)
        {
            playerManager.Damage(1);
            // パーティクルとか。
            SoundManager.Instance.PlaySE(boomSe);
            Destroy(this.gameObject);
            return;
        }

        BreakableObject breakableObject = collision.gameObject.GetComponent<BreakableObject>();
        if (breakableObject != null)
        {
            breakableObject.Break();
            // パーティクルとか。
            SoundManager.Instance.PlaySE(boomSe);
            Destroy(this.gameObject);
            return;
        }

        SoundManager.Instance.PlaySE(boomSe);
        Destroy(this.gameObject);
    }

    void TilemapCollider2D()
    {
        SoundManager.Instance.PlaySE(boomSe);
        Destroy(this.gameObject);
    }
    
    
}
