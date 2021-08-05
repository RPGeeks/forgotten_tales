using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character Input Feed that does nothing. Used for debugging / testing / development.
public class EmptyCIF : CharacterInputFeed
{
    public bool IsCrouching()
    {
        return false;
    }

    public bool IsSprinting()
    {
        return false;
    }

    public bool IsStrafingLeft()
    {
        return false;
    }

    public bool IsStrafingRight()
    {
        return false;
    }

    public bool IsWalking()
    {
        return false;
    }

    public bool IsWalkingBackwards()
    {
        return false;
    }

    public bool JustStartedWalking()
    {
        return false;
    }

    public bool JustStoppedWalking()
    {
        return false;
    }

    public bool StartJump()
    {
        return false;
    }

    public bool AttemptsAttack()
    {
        return false;
    }

    public float GetLookDirection()
    {
        return 0f;
    }
}
