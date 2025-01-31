using UnityEngine;

public abstract partial class BaseState
{
    public class AttackSpecialState : BaseState 
    {
        private Timer AnimTimer;
        public AttackSpecialState(PlayerController player, Animator animator) : base(player, animator) 
        {
            AnimTimer = new Timer(animator, "Anim_Player_Special");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayAnimation(SpecialHash);
        }
        public override void Update()
        {
            AnimTimer.Update(Time.deltaTime);

            if (AnimTimer.IsFinished)
            {
                player.StateMachine.ChangeState(new IdleState(player, animator));
            }
        }
    }
}
