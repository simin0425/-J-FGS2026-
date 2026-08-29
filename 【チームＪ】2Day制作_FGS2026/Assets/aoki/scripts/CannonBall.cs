using UnityEngine;
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
    private bool shouldMove = false;


    protected bool isRevered = false;

    bool IReversible.isRevered { get; set; }


    void FixedUpdate()
    {
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
        PlayerManagerOM playerManager = GetComponent<PlayerManagerOM>();
        if (playerManager != null) {
            playerManager.Damage(1);
            // パーティクルとか。
            Destroy(this.gameObject);
            return;
        }
        BreakableObject breakableObject = GetComponent<BreakableObject>();
        if (breakableObject != null)
        {
            breakableObject.Break();
            // パーティクルとか。
            Destroy(this.gameObject);
            return;
        }

    }
    // TODO:linerVerocityからの角度計算、適用(角度計算とか置くユーティリティクラスほしい)
}
