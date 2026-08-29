using UnityEngine;

public class SpikeTrampoline : MonoBehaviour, IReversible
{
    [Header("Spike Components")]
    [SerializeField] private Sprite spikeSprite;
    [SerializeField] private Vector2 spikeColOffset;
    [SerializeField] private Vector2 spikeColSize;

    [Header("Trampoline Components")]
    [SerializeField] private Sprite trampolineSprite;
    [SerializeField] private Vector2 trampolineColOffset;
    [SerializeField] private Vector2 trampolineColSize;
    [SerializeField] private float trampolinePower = 10.0f;

    bool IReversible.isRevered { get; set; }
    bool isReversed = false; // false : とげ / true : トランポリン

    void IReversible.OnReversed()
    {
        Debug.Log("SpikeTrampoline_Reversed");
        isReversed = !isReversed;
        UpdateState();
    }

    void UpdateState()
    {
        if (isReversed)
        {
            this.GetComponent<SpriteRenderer>().sprite = trampolineSprite;
            var col = this.GetComponent<BoxCollider2D>();
            col.offset = trampolineColOffset;
            col.size = trampolineColSize;

            Vector3 pos = this.transform.position;
            pos.y -= trampolineColSize.y;
            this.transform.position = pos;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spikeSprite;
            var col = this.GetComponent<BoxCollider2D>();
            Vector2 offset = spikeColOffset;
            offset.y -= trampolineColSize.y;
            col.offset = offset;
            col.size = spikeColSize;

            Vector3 pos = this.transform.position;
            pos.y += trampolineColSize.y;
            this.transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isReversed)
            {
                // トランポリン
                collision.GetComponent<Rigidbody2D>().linearVelocityY = trampolinePower;
                Debug.Log("Trampoline : hit");
            }
            else
            {
                // とげ
                Debug.Log("Spike : hit");
                collision.GetComponent<PlayerManagerOM>().Damage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // 2D BoxColliderの描画例
        var box2d = GetComponent<BoxCollider2D>();
        if (box2d != null && box2d.enabled)
        {
            Gizmos.color = Color.green;
            Vector3 center = transform.TransformPoint(box2d.offset);
            Vector3 size = new Vector3(box2d.size.x * transform.lossyScale.x, box2d.size.y * transform.lossyScale.y, 0f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
