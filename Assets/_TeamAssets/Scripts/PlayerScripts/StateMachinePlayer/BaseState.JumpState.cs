using UnityEngine;

public abstract partial class BaseState
{
    public class JumpState : BaseState 
    {

        private Timer AnimTimer;
        

        public JumpState(PlayerController player, Animator animator) : base(player, animator) 
        {
            AnimTimer = new Timer(animator, "Anim_Player_Jump_Up");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayAnimation(JumpHash);
        }

        public override void FixedUpdate()
        {
            AnimTimer.Update(Time.deltaTime);

            if (AnimTimer.IsFinished)
            {
                player.StateMachine.ChangeState(new IdleState(player, animator));
            }
        }
    }
}
