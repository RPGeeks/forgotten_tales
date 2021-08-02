using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalKeyboardCIF : CharacterInputFeed
{
    private CameraController camController;

    public LocalKeyboardCIF(CameraController camController)
    {
        this.camController = camController;
    }

    public float GetLookDirection()
    {
        return camController.GetYAxisWorld();
    }

    public bool IsCrouching()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        return false;
    }

    public bool IsSprinting()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }

    public bool IsStrafingLeft()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.A))
        {
            return true;
        }
        return false;
    }

    public bool IsStrafingRight()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.D))
        {
            return true;
        }
        return false;
    }

    public bool IsWalking()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    public bool IsWalkingBackwards()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKey(KeyCode.S))
        {
            return true;
        }
        return false;
    }

    public bool JustStartedWalking()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    public bool JustStoppedWalking()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    public bool StartJump()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public bool AttemptsAttack()
    {
        if (!VirtualInput.IsGameInput) { return false; }

        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
