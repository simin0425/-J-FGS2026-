using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerOM : MonoBehaviour
{
    InputAction move;
    InputAction jump;

    public float moveSpeed=5;
    public float jumpForce=5;

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
        if (jump.WasPressedThisFrame())
        {
            playerController.Jump(jumpForce);
        }
    }

    public void ChangeState(PlayerState state)
    {
        if(currentState==state)return;
        currentState=state;
    }
}
