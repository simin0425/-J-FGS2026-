using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerOM : MonoBehaviour
{
    InputAction move;
    InputAction jump;
    InputAction attack;

    [Header("移動速度")]public float moveSpeed=5;
    [Header("ジャンプ力")]public float jumpForce=5;
    [Header("攻撃力")]public float attackForce=5;
    [Header("攻撃持続")]public float attackDuration=2;

    public bool isGround=false;
    public bool isSquat=false;
    public bool isReverseGravity=false;//仮実装,Reverse専用の機能実装次第削除予定

    [SerializeField]float groundDistance=0.5f;
    [SerializeField]LayerMask groundLayer;

    public PlayerState currentState=PlayerState.idle;

    PlayerControllerOM playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        move=InputSystem.actions.FindAction("Move");
        jump=InputSystem.actions.FindAction("Jump");
        attack=InputSystem.actions.FindAction("Attack");
        move.Enable();
        jump.Enable();
        attack.Enable();
        playerController=this.GetComponent<PlayerControllerOM>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        CheckSquat();
        switch (currentState)
        {
            case PlayerState.idle:
                IdleLoop();
                break;
            case PlayerState.move:
                Move();
                break;
            case PlayerState.squat:
                SquatAttack();
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
            if(!isSquat)ChangeState(PlayerState.move);
            else ChangeState(PlayerState.squat);
        }
        if (attack.WasPressedThisFrame())
        {
            NormalAttack();
        }
    }

    private void NormalAttack()
    {
        playerController.NormalAttack(attackForce,attackDuration);
    }

    private void SquatAttack()
    {
        if(attack.WasPressedThisFrame()&&isSquat)playerController.SquatAttack(attackForce);
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
            playerController.Jump(jumpForce,isReverseGravity);
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

    private void CheckSquat()
    {
        var InputValue=move.ReadValue<Vector2>();
        if(InputValue.y<0)isSquat=true;
    }
}
