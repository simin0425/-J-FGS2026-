using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerOM : MonoBehaviour
{
    InputAction move;
    InputAction jump;
    InputAction attack;
    InputAction changeGravity;

    [Header("体力")]public int HP=3;
    [Header("移動速度")]public float moveSpeed=5;
    [Header("ジャンプ力")]public float jumpForce=5;
    [Header("攻撃力")]public float attackForce=5;
    [Header("攻撃範囲")]public Vector2 attackSize=new Vector2(1,1);
    [Header("攻撃持続")]public float attackDuration=2;

    public bool isGround=false;
    public bool isSquat=false;
    public bool isReverseGravity=false;

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
        changeGravity=InputSystem.actions.FindAction("ChangeGravity");
        move.Enable();
        jump.Enable();
        attack.Enable();
        changeGravity.Enable();
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
                ActionLoop();
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
        if (attack.WasPressedThisFrame())
        {
            Debug.Log("attack");
            NormalAttack();
        }
        if (changeGravity.WasPressedThisFrame()&&isGround)
        {
            ChangeGravity();
            isReverseGravity=!isReverseGravity;
        }
    }
    private void ActionLoop()
    {
        if (attack.WasPressedThisFrame())
        {
            Debug.Log("attack");
            NormalAttack();
        }
        if (changeGravity.WasPressedThisFrame()&&isGround)
        {
            ChangeGravity();
            isReverseGravity=!isReverseGravity;
        }
    }

    private void NormalAttack()
    {
        playerController.NormalAttack(attackSize,attackDuration);
    }

    private void ChangeGravity()
    {
        playerController.ChangeGravity();
    }

    private void Move()
    {
        var inputValue=move.ReadValue<Vector2>();
        if(inputValue==new Vector2(0,0))ChangeState(PlayerState.idle);//動いてないときidleへ
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

    private void CheckGround()//接地判定検知
    {
        if(!isReverseGravity)
        {
            var isHit=Physics2D.Raycast(
                transform.position,
                Vector2.down,
                groundDistance,
                groundLayer
            );
            
            isGround=isHit;
        }
        else
        {
            var isHit=Physics2D.Raycast(
                transform.position,
                Vector2.up,
                groundDistance,
                groundLayer
            );
            
            isGround=isHit;
        }
    }

    private void CheckSquat()//下入力検知
    {
        var InputValue=move.ReadValue<Vector2>();
        if(InputValue.y<0)isSquat=true;
    }

    public void Damage(int damage)
    {
        HP-=damage;
    }
}
