using UnityEngine;

public abstract partial class BaseState
{
    public class LocomotionState : BaseState 
    {
        public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            PlayAnimation(WalkHash);
        }
        public override void FixedUpdate()
        {
            player.Movement();
            if (!player.IsWalking)
            {
                player.StateMachine.ChangeState(new IdleState(player, animator));
            }
        }
    }
}
