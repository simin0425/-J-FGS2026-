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

    public void NormalAttack(Vector2 attackSize,float duration)
    {
        Attack(duration,attackSize);
    }

    public void SquatAttack(float attackForce)
    {
        
    }

    private void Attack(float duration,Vector2 attackSize)
    {
        Debug.Log("attacking");
        Collider2D[] hitCol=Physics2D.OverlapBoxAll(attackCol.transform.position,attackSize,0);
        foreach(Collider2D col in hitCol){
            //if(col.name==this.gameObject.name)return;
            Debug.Log(col.name);
            var reversible=col.GetComponent<IReversible>();
            if(reversible==null)Debug.Log("nullpo");
            reversible.OnReversed();
        }
        //var reversible=hitCol.GetComponent<IReversible>();
        //reversible.OnReverse();
    }
}
