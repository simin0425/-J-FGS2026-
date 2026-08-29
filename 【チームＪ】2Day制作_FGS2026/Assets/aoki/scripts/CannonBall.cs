using UnityEngine;

public class CannonBall : MonoBehaviour, IReversible
{
    [Header("コンポーネント")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rigidbody;
    [Header("移動")]
    [SerializeField][Tooltip("x-が左向き")] private Vector3 moveDir;
    [SerializeField] private float moveSpeed;

    protected bool isRevered = false;

    bool IReversible.isRevered { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
    }

    void FixedUpdate()
    {
        rigidbody.linearVelocity = moveDir * moveSpeed * (isRevered ? -1f : 1f);
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
