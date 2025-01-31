using UnityEditor;
using UnityEngine;

public abstract partial class BaseState : IState
{
    protected readonly PlayerController player;
    protected readonly Animator animator;

    protected static readonly int IdleHash = Animator.StringToHash("Anim_Player_Idle");
    protected static readonly int WalkHash = Animator.StringToHash("Anim_Player_Walk");
    protected static readonly int JumpHash = Animator.StringToHash("Anim_Player_Jump_Up");
    //protected static readonly int JumpDownHash = Animator.StringToHash("Anim_Player_Jump_Down");
    protected static readonly int Puch1Hash = Animator.StringToHash("Anim_Player_Punch1");
    protected static readonly int Puch2Hash = Animator.StringToHash("Anim_Player_Punch2");
    protected static readonly int Puch3Hash = Animator.StringToHash("Anim_Player_Punch3");
    protected static readonly int SpecialHash = Animator.StringToHash("Anim_Player_Special");

    protected const float crossFadeDuration = 0.1f;

    protected BaseState(PlayerController player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    public virtual void FixedUpdate() { }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }

    public virtual void Update() { }

    protected void PlayAnimation (int animationHash, float trasitionDuration = crossFadeDuration)
    {
        animator.CrossFade(animationHash, trasitionDuration);
        animator.Play(animationHash);
    }
}
