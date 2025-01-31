using System;
using UnityEngine;

public class Timer
{
    private float timeRemaining; 

    public bool IsFinished => timeRemaining <= 0;

    public Timer(Animator animator, string animationName)
    {
        AnimationClip clip = GetAnimationClip(animator, animationName);


        if (clip != null)
        {
            timeRemaining = clip.length;
        }
        else
        {
            timeRemaining = 0.5f;
        }
    }
    
    public void Update(float deltaTime)
    {
        if (IsFinished) return;
        timeRemaining -= deltaTime;
    }

    public AnimationClip GetAnimationClip(Animator animator, string animationName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip;
            }
        }
        return null;
    }
}
