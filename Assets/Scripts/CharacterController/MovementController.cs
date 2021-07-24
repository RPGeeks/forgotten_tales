using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private float forwardSpeed = 1.2f;

    private Rigidbody rb;
    private CharacterInputFeed cif;

    private static Vector3 m_EulerAngleVelocity = new Vector3(0, 15, 0);

    public MovementController(Rigidbody rb, CharacterInputFeed cif)
    {
        this.rb = rb;
        this.cif = cif;
    }

    public void Step(float delta)
    {
        float shiftMultiplier;
        if (cif.IsSprinting())
        {
            shiftMultiplier = 1.5f;
        }
        else
        {
            shiftMultiplier = 1f;
        }

        if (cif.StartJump())
        {
            rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            // animationController.SwitchTo(ProcAnims.Jump);
        }

        if (cif.IsWalking())
        {
            rb.MovePosition(rb.position + rb.rotation * Vector3.forward * shiftMultiplier * delta * forwardSpeed);
        }

        if (cif.IsWalkingBackwards())
        {
            rb.MovePosition(rb.position - rb.rotation * Vector3.forward * shiftMultiplier * delta * forwardSpeed);
        }

        if (cif.IsTurningRight())
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * shiftMultiplier * delta);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        if (cif.IsTurningLeft())
        {
            Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * shiftMultiplier * delta);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
