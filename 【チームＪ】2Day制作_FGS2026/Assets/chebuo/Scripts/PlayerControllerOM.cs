using UnityEngine;

public class PlayerControllerOM : MonoBehaviour
{
    [SerializeField]GameObject attackCol;
    Vector3 attackColDefaultPos;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    
    void Awake()
    {
        rb=this.GetComponent<Rigidbody2D>();
        spriteRenderer=this.GetComponent<SpriteRenderer>();
        attackColDefaultPos=attackCol.transform.localPosition;
    }

    public void Move(Vector2 inputValue,float speed)
    {
        rb.linearVelocity = new Vector2(inputValue.x * speed,rb.linearVelocity.y);

        if (inputValue.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputValue.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float x = spriteRenderer.flipX ? -attackColDefaultPos.x : attackColDefaultPos.x;
        float y = spriteRenderer.flipY ? -attackColDefaultPos.y : attackColDefaultPos.y;
        attackCol.transform.localPosition = new Vector3( x,y,attackColDefaultPos.z);
    }

    public void Jump(float jumpForce,bool isReverse)
    {
        if(!isReverse)rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        else rb.AddForce(Vector2.down*jumpForce,ForceMode2D.Impulse);
    }

    public void NormalAttack(Vector2 attackSize)
    {
        rb.linearVelocityX=0;
        //Attack(duration,attackSize);
    }

    public void ChangeGravity()
    {
        rb.gravityScale*=-1;
        spriteRenderer.flipY = !spriteRenderer.flipY;

        float x = spriteRenderer.flipX ? -attackColDefaultPos.x : attackColDefaultPos.x;
        float y = spriteRenderer.flipY ? -attackColDefaultPos.y : attackColDefaultPos.y;
        attackCol.transform.localPosition = new Vector3(x,y,attackColDefaultPos.z);
    }

    public void Attack(Vector2 attackSize)
    {
        Debug.Log("attacking");
        Collider2D[] hitCol=Physics2D.OverlapBoxAll(attackCol.transform.position,attackSize,0);
        foreach(Collider2D col in hitCol){
            if(col.gameObject==this.gameObject)continue;
            var reversible=col.GetComponent<IReversible>();
            if(reversible==null)Debug.Log("nullpo");
            Debug.Log(col.name);
            reversible.OnReversed();
        }
    }
    #if UNITY_EDITOR
    //攻撃判定可視化用

    PlayerManagerOM pm;
    [Header("攻撃判定オブジェクト")][SerializeField]private SpriteRenderer sr;
    [Header("表示非表示")][SerializeField]private bool isEnable; 


    void Start()=>pm=this.GetComponent<PlayerManagerOM>();
    void Update()
    {
        sr.enabled=isEnable;
        sr.gameObject.transform.localScale=new Vector3(pm.attackSize.x,pm.attackSize.y,1);
    }

    #endif
}
