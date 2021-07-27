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

    public override bool IsTurningLeft()
    {
        return false;
    }

    public override bool IsTurningRight()
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
}
