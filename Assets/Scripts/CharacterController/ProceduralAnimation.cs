using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityRig { }

public class ProceduralAnimationController<RigType> where RigType : IEntityRig
{
    public static EmptyAnimation<RigType> emptyAnim = new EmptyAnimation<RigType>();

    CharacterInputFeed cif;
    RigType targetRig;

    ProceduralAnimation<RigType> currentAnimation;

    public ProceduralAnimationController( CharacterInputFeed cif, RigType targetRig )
    {
        this.targetRig = targetRig;
        this.cif = cif;
        this.currentAnimation = emptyAnim;
    }

    public void Step(float delta)
    {
        currentAnimation.OnAnimationStep(delta);
    }

    public void SwitchTo(ProceduralAnimation<RigType> newState)
    {
        currentAnimation.OnAnimationEnd();
        currentAnimation = newState;
        currentAnimation.OnAnimationStart();
    }
}

public abstract class ProceduralAnimation<RigType>
{
    public abstract void OnAnimationStart();

    public abstract void OnAnimationStep(float delta);

    public abstract void OnAnimationEnd();

    public static void InterpolateTransforms( Transform t1, Transform t2, float t )
    {
        t1.localPosition = Vector3.Lerp(t1.localPosition, t2.localPosition, t);
        t1.localRotation = Quaternion.Lerp(t1.localRotation, t2.localRotation, t);
    }
}

// Animation that does nothing
public class EmptyAnimation<RigType> : ProceduralAnimation<RigType>
{
    public override void OnAnimationEnd() { }
    public override void OnAnimationStart() { }
    public override void OnAnimationStep(float delta) { }
}