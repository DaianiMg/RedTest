using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using static BaseState;

public enum ComboState
{
    None,
    Punch_1,
    Punch_2,
    Punch_3
}

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody m_Rigidbody;
    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }

    [Header("Moviment")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float walkSpeedZ = 1.5f;
    public Vector2 MoveInput { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsLookLeft { get; private set; }

    [Header("Jump")]
    [SerializeField] private float jumpForce = 2.5f;
    public CheckGround checkGround;

    [Header("Attack")]
    private bool activateTimerToReset;
    private float defaultComboTimer = 0.4f;
    private float currentComboTimer;
    private ComboState currentComboState;
    [SerializeField] private int damageAttack = 5;
    [SerializeField] private int damageSpacialAttack = 10;

    [SerializeField] private PlayerDetector hitDetector;
    [SerializeField] private CooldownUi IsSpecial;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        StateMachine = new StateMachine();
    }

    private void Start()
    {
        StateMachine.ChangeState(new IdleState(this, Animator));
    }

    void Update()
    {
        StateMachine.Update();
        ResetComboState();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void Movement()
    {
        if (MoveInput.x > 0 && IsLookLeft)
        {
            Flip();
        }
        else if (MoveInput.x < 0 && !IsLookLeft)
        {
            Flip();
        }

        IsWalking = MoveInput.x != 0 ? true : false || MoveInput.y != 0 ? true : false;

        m_Rigidbody.linearVelocity = new Vector3(-MoveInput.x * walkSpeed,
            m_Rigidbody.linearVelocity.y, -MoveInput.y * walkSpeedZ);
    }

    private void Flip()
    {
        IsLookLeft = !IsLookLeft;
        transform.eulerAngles = IsLookLeft ? new Vector3(0f, 180f, 0f) : Vector3.zero;
    }

    public void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public void ComboAttack()
    {
        if (currentComboState == ComboState.Punch_3) return;

        currentComboState++;
        activateTimerToReset = true;
        currentComboTimer = defaultComboTimer;

        if (currentComboState == ComboState.Punch_1)
        {
            StateMachine.ChangeState(new AttackPuch1State(this, Animator));
        }
        if (currentComboState == ComboState.Punch_2)
        {
            StateMachine.ChangeState(new AttackPuch2State(this, Animator));
        }
        if (currentComboState == ComboState.Punch_3)
        {
            StateMachine.ChangeState(new AttackPuch3State(this, Animator));
        }

    }

    public void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0)
            {
                currentComboState = ComboState.None;

                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }


    #region Métodos Input System

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
        IsWalking = MoveInput.magnitude > 0;
        StateMachine.ChangeState(new LocomotionState(this, Animator));
    }

    //public void OnMove(InputAction.CallbackContext value)
    //{
    //    MoveInput = value.ReadValue<Vector2>();
    //    IsWalking = MoveInput.magnitude > 0;
    //    StateMachine.ChangeState(new LocomotionState(this, Animator));
    //}

    private void OnAttack(InputValue value)
    {
        ComboAttack();
        hitDetector.DetectHit(damageAttack, "normal");
    }

    //public void OnAttack(InputAction.CallbackContext value)
    //{
    //    ComboAttack();
    //    hitDetector.DetectHit(damageAttack);
    //}

    private void OnAttackSpecial(InputValue value)
    {
        if (value.isPressed && IsSpecial.IsPowerSpecial)
        {
            hitDetector.DetectHit(damageSpacialAttack, "special");
            StateMachine.ChangeState(new AttackSpecialState(this, Animator));
            IsSpecial.ResetCooldown();

        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && checkGround.IsGrounded)
        {
            Jump();
            StateMachine.ChangeState(new JumpState(this, Animator));
        }
    }

    //public void OnJump(InputAction.CallbackContext value)
    //{
    //    Jump();
    //    StateMachine.ChangeState(new JumpState(this, Animator));
    //}


    #endregion


}