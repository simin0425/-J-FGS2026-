using System;
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

    [HideInInspector] public Vector3 respawnPoint;

    public bool isGround=false;
    public bool isSquat=false;
    public bool isReverseGravity=false;

    [SerializeField]float groundDistance=0.5f;
    [SerializeField]LayerMask groundLayer;

    public PlayerState currentState=PlayerState.idle;
    public AttackState currentAttackState=AttackState.idle;

    Animator animator;
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
        animator=this.GetComponent<Animator>();
        playerController=this.GetComponent<PlayerControllerOM>();
    }

    private void Start()
    {
        respawnPoint=transform.position;
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
            case PlayerState.attack:
                NormalAttack();
            break;
            default:
                break;
        }
        Jump();
        
        Debug.Log(isGround);
    }

    private void IdleLoop()
    {
        animator.SetBool("isMove",false);
        if (move.IsPressed())
        {
            ChangeState(PlayerState.move);
        }
        if (attack.WasPressedThisFrame())
        {
            ChangeState(PlayerState.attack);
            ChangeAttackState(AttackState.hammer);
        }
        if (changeGravity.WasPressedThisFrame()&&isGround)
        {
            animator.SetBool("isMove",false);
            animator.SetBool("isAttack",true);
            ChangeAttackState(AttackState.gravity);
            isReverseGravity=!isReverseGravity;
        }
    }
    private void ActionLoop()
    {
        if (attack.WasPressedThisFrame())
        {
            ChangeState(PlayerState.attack);
        }
        if (changeGravity.WasPressedThisFrame()&&isGround)
        {
            animator.SetBool("isMove",false);
            animator.SetBool("isAttack",true);
            ChangeAttackState(AttackState.gravity);
            isReverseGravity=!isReverseGravity;
        }
    }

    private void NormalAttack()
    {
        animator.SetBool("isMove",false);
        animator.SetBool("isAttack",true);
        //this.transform.localScale=new Vector3(0.1f,0.1f,0.1f);
        playerController.NormalAttack(attackSize);
    }

    public void StartAttack()
    {
        switch (currentAttackState)
        {
            case AttackState.gravity:
                ChangeGravity();
                break;
            case AttackState.hammer:
                playerController.Attack(attackSize);
                break;
        }
    }

    public void StopAttack()
    {
        ChangeState(PlayerState.idle);
        ChangeAttackState(AttackState.idle);
        //this.transform.localScale=new Vector3(0.05f,0.05f,0.05f);
        animator.SetBool("isAttack",false);
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
        animator.SetBool("isMove",true);
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

    public void ChangeAttackState(AttackState state)
    {
        if(currentAttackState==state)return;
        currentAttackState=state;
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
            animator.SetBool("isGround",isHit);
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
            animator.SetBool("isGround",isHit);
        }
    }

    private void CheckSquat()//下入力検知
    {
        var InputValue=move.ReadValue<Vector2>();
        if(InputValue.y<0)isSquat=true;
    }

    public void Damage(int damage)
    {
        animator.SetBool("isDamage",true);
        HP-=damage;
    }

    public void FinishDamage()
    {
        animator.SetBool("isDamage",false);
    }
}
