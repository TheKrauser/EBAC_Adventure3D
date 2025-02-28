using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour
{
    public Animator anim;
    public List<AnimationSetup> animationSetups;

    public void PlayAnimationByTrigger(AnimationType type)
    {
        var setup = animationSetups.Find(i => i.animationType == type);
        if (setup == null) return;

        anim.SetTrigger(setup.trigger);
    }
}

[System.Serializable]
public class AnimationSetup
{
    public AnimationType animationType;
    public string trigger;
}

public enum AnimationType
{
    NONE,
    IDLE,
    RUN,
    ATTACK,
    DEATH
}
