using UnityEngine;

public class PlayerControllerOM : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Awake()
    {
        rb=this.GetComponent<Rigidbody2D>();
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

    public void NormalAttack(float attackForce)
    {
        
    }

    public void SquatAttack(float attackForce)
    {
        
    }
}
