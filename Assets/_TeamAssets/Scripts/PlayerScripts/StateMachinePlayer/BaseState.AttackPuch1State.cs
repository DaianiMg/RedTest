using UnityEngine;

public abstract partial class BaseState
{
    public class AttackPuch1State : BaseState 
    {
        private Timer AnimTimer;
        public AttackPuch1State(PlayerController player, Animator animator) : base(player, animator) 
        {
            AnimTimer = new Timer(animator, "Anim_Player_Punch1");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayAnimation(Puch1Hash);            
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
