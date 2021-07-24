using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInputFeed
{
    public abstract bool StartJump();

    public abstract bool JustStartedWalking();

    public abstract bool JustStoppedWalking();

    public abstract bool IsWalking();

    public abstract bool IsWalkingBackwards();

    public abstract bool IsTurningLeft();

    public abstract bool IsTurningRight();

    public abstract bool IsSprinting();

    public abstract bool IsCrouching();

    public abstract bool AttemptsAttack();
}
