using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIFSync : NetworkBehaviour, CharacterInputFeed
{
    private static readonly float epsilon = 0.1f;

    CharacterInputFeed inputSource = null;

    void Start()
    {
        var character = GetComponent<CharacterController>();

        if ( character.isLocalPlayer )
        {
            if ( character.cif is LocalKeyboardCIF ){
                // TODO : component order issue. ( might be null )
                inputSource = (LocalKeyboardCIF)character.cif;
                Debug.Log("InputSource bound!");
            }
        }
    }

    public void Update()
    {
        if ( inputSource != null ) // local controller
        {
            if ( walkingForward != inputSource.IsWalking())
            {
                walkingForward = inputSource.IsWalking();
                CMDWalkingForward(walkingForward);
            }

            if (walkingBackward != inputSource.IsWalkingBackwards())
            {
                walkingBackward = inputSource.IsWalkingBackwards();
                CMDWalkingForward(walkingBackward);
            }

            if ( sprinting != inputSource.IsSprinting())
            {
                sprinting = inputSource.IsSprinting();
                CMDSprinting(sprinting);
            }

            if ( inputSource.StartJump())
            {
                CMDStartJump();
            }

            if ( strafingLeft != inputSource.IsStrafingLeft() )
            {
                strafingLeft = inputSource.IsStrafingLeft();
                CMDStrafingLeft(strafingLeft);
            }

            if ( strafingRight != inputSource.IsStrafingRight() )
            {
                strafingRight = inputSource.IsStrafingRight();
                CMDStrafingRight(strafingRight);
            }

            // TODO : create a condition for 360 to 0 turns. ( Because clock arithmetic .. )
            if ( Mathf.DeltaAngle(lookDirection, inputSource.GetLookDirection()) > epsilon)
            {
                lookDirection = inputSource.GetLookDirection();
                CMDLookDirection(lookDirection);
            }

            if ( inputSource.AttemptsAttack() )
            {
                CMDAttemptAttack();
            }

        } else // remote controller
        {
            // reset all 'immediate' input flags
            justStartedWalking = false;
            justStoppedWalking = false;
            startJump = false;
            attemptsAttack = false;
        }
    }

    bool startJump = false;
    [Command]
    public void CMDStartJump()
    {
        RPCStartJump();
    }

    [ClientRpc(includeOwner = false)]
    public void RPCStartJump()
    {
        startJump = true;
    }

    float lookDirection = 0f;
    [Command]
    public void CMDLookDirection(float direction)
    {
        RPCLookDirection(lookDirection);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCLookDirection(float direction)
    {
        lookDirection = direction;
    }


    bool walkingForward = false;
    [Command]
    public void CMDWalkingForward(bool walking)
    {
        RPCWalkingForward(walking);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCWalkingForward(bool walking)
    {
        if ( walkingForward != walking )
        {
            if ( walking == true)
            {
                justStartedWalking = true;
            }
            {
                justStoppedWalking = true;
            }
        }
        walkingForward = walking;
    }

    bool walkingBackward = false;
    [Command]
    public void CMDWalkingBackward(bool walking)
    {
        RPCWalkingBackward(walking);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCWalkingBackward(bool walking)
    {
        if (walkingForward != walking)
        {
            if (walking == true)
            {
                justStartedWalking = true;
            }
            {
                justStoppedWalking = true;
            }
        }
        walkingBackward = walking;
    }

    bool attemptsAttack = false;
    [Command]
    public void CMDAttemptAttack()
    {
        RPCAttemptAttack();
    }

    [ClientRpc(includeOwner = false)]
    public void RPCAttemptAttack()
    {
        attemptsAttack = true;
    }

    bool sprinting = false;
    [Command]
    public void CMDSprinting(bool _sprinting)
    {
        RPCSprinting(_sprinting);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCSprinting(bool _sprinting)
    {
        Debug.Log("Sprinting message " + _sprinting.ToString());
        sprinting = _sprinting;
    }

    bool strafingLeft = false;
    [Command]
    public void CMDStrafingLeft(bool strafing)
    {
        RPCStrafingLeft(strafing);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCStrafingLeft(bool strafing)
    {
        strafingLeft = strafing;
    }

    bool strafingRight = false;
    [Command]
    public void CMDStrafingRight(bool strafing)
    {
        RPCStrafingRight(strafing);
    }

    [ClientRpc(includeOwner = false)]
    public void RPCStrafingRight(bool strafing)
    {
        strafingRight = strafing;
    }

    public bool StartJump() { return startJump; }

    bool justStartedWalking = false;
    public bool JustStartedWalking() { return justStartedWalking; }

    bool justStoppedWalking = false;
    public bool JustStoppedWalking() { return justStoppedWalking; }

    public bool IsWalking() { return walkingForward; }

    public bool IsWalkingBackwards() { return walkingBackward; }

    public bool IsStrafingLeft() { return strafingLeft; }

    public bool IsStrafingRight() { return strafingRight;  }

    public bool IsSprinting() { return sprinting; }

    public bool IsCrouching() { return false; }

    public bool AttemptsAttack() { return attemptsAttack; }

    public float GetLookDirection() { return lookDirection; }
}
