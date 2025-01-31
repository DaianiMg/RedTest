using UnityEngine;

public abstract partial class BaseState
{
    
    public class AttackPuch2State : BaseState 
    {
        private Timer AnimTimer;
        public AttackPuch2State(PlayerController player, Animator animator) : base(player, animator) 
        {
            
            AnimTimer = new Timer(animator, "Anim_Player_Punch2");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayAnimation(Puch2Hash);
            
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
