using UnityEngine;

public class PlayerControllerOM : MonoBehaviour
{
    [SerializeField]GameObject attackCol;
    Rigidbody2D rb;
    
    void Awake()
    {
        rb=this.GetComponent<Rigidbody2D>();
        attackCol.SetActive(false);
    }

    public void Move(Vector2 inputValue,float speed)
    {
        rb.linearVelocity = new Vector2(inputValue.x * speed, rb.linearVelocity.y);
    }

    public void Jump(float jumpForce,bool isReverse)
    {
        if(!isReverse)rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        else rb.AddForce(Vector2.down*jumpForce,ForceMode2D.Impulse);
    }

    public void NormalAttack(float attackForce,float duration)
    {
        //Attack(duration,attack);
    }

    public void SquatAttack(float attackForce)
    {
        
    }

    private void Attack(float duration,Vector2 attackSize)
    {
        Collider2D[] hitCol=Physics2D.OverlapBoxAll(attackCol.transform.position,attackSize,0);
    }

    private void GetColliders()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //var reversable=col.transform.GetComponent<IReversable>();
        //if(reversable==null)return;

        //反転処理いろいろ
    }
}
