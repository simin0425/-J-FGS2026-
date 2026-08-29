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

        if (distance < moveSpeed)
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
    // TODO:linerVerocityからの角度計算、適用(角度計算とか置くユーティリティクラスほしい)
}
