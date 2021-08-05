using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInputFeed
{
    bool StartJump();
    bool JustStartedWalking();
    bool JustStoppedWalking();
    bool IsWalking();
    bool IsWalkingBackwards();
    bool IsStrafingLeft();
    bool IsStrafingRight();
    bool IsSprinting();
    bool IsCrouching();
    bool AttemptsAttack();
    float GetLookDirection();
}
