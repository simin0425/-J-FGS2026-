using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerOM : MonoBehaviour
{
    InputAction move;
    InputAction jump;

    public float moveSpeed=5;
    public float jumpForce=5;

    public bool isGround=false;
    [SerializeField]float groundDistance=0.5f;
    [SerializeField]LayerMask groundLayer;

    public PlayerState currentState=PlayerState.idle;

    PlayerControllerOM playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        move=InputSystem.actions.FindAction("Move");
        jump=InputSystem.actions.FindAction("Jump");
        move.Enable();
        jump.Enable();
        playerController=this.GetComponent<PlayerControllerOM>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        switch (currentState)
        {
            case PlayerState.idle:
                IdleLoop();
                break;
            case PlayerState.move:
                Move();
                break;
            default:
                break;
        }
        Jump();

    }

    private void IdleLoop()
    {
        if (move.WasPressedThisFrame())
        {
            ChangeState(PlayerState.move);
        }
    }

    private void NormalAttack()
    {
        
    }

    private void SquatAttack()
    {
        
    }

    private void Move()
    {
        var inputValue=move.ReadValue<Vector2>();
        playerController.Move(inputValue,moveSpeed);
    }
    private void Jump()
    {
        if (jump.WasPressedThisFrame()&&isGround)
        {
            playerController.Jump(jumpForce);
        }
    }

    public void ChangeState(PlayerState state)
    {
        if(currentState==state)return;
        currentState=state;
    }

    private void CheckGround()
    {
        var isHit=Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundDistance,
            groundLayer
        );
        isGround=isHit;
    }
}
