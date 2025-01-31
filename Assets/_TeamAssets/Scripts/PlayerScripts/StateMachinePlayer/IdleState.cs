using UnityEngine;
public abstract partial class BaseState
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayAnimation(IdleHash);
        }

        public override void Update()
        {
            if (player.IsWalking)
            {
                player.StateMachine.ChangeState(new LocomotionState(player, animator));
            }

        }
    }
}
