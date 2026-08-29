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
        rb.linearVelocity=inputValue*speed;
    }

    public void Jump(float jumpForce)
    {
        rb.AddForce(Vector2.up*jumpForce);
    }
}
