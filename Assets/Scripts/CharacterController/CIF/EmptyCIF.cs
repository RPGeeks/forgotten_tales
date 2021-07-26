using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character Input Feed that does nothing. Used for debugging / testing / development.
public class EmptyCIF : CharacterInputFeed
{
    public override bool IsCrouching()
    {
        return false;
    }

    public override bool IsSprinting()
    {
        return false;
    }

    public override bool IsStrafingLeft()
    {
        return false;
    }

    public override bool IsStrafingRight()
    {
        return false;
    }

    public override bool IsWalking()
    {
        return false;
    }

    public override bool IsWalkingBackwards()
    {
        return false;
    }

    public override bool JustStartedWalking()
    {
        return false;
    }

    public override bool JustStoppedWalking()
    {
        return false;
    }

    public override bool StartJump()
    {
        return false;
    }

    public override bool AttemptsAttack()
    {
        return false;
    }

    public override float GetLookDirection()
    {
        return 0f;
    }
}
