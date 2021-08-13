using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private float forwardSpeed = 9f;
    private float distToGround = 2.6f;

    private Rigidbody rb;
    private CharacterInputFeed cif;

    private static Vector3 m_EulerAngleVelocity = new Vector3(0, 15, 0);

    public MovementController(Rigidbody rb, CharacterInputFeed cif)
    {
        this.rb = rb;
        this.cif = cif;

        rb.maxAngularVelocity = 0;
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
            if (Physics.Raycast(rb.transform.position, Vector3.down, distToGround))
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            } 
            
            // animationController.SwitchTo(ProcAnims.Jump);
        }

        if (cif.IsWalking())
        {
            
            /// TODO : replace rb.transform.rotation assignment with rb.MoveRotation()
            /// ( You have to compute the difference quaternion )
            Quaternion t = Quaternion.Euler(new Vector3(0f, cif.GetLookDirection(), 0f));
            rb.transform.rotation = t;


            rb.MovePosition(rb.position + rb.rotation * Vector3.forward * shiftMultiplier * delta * forwardSpeed);
        }

        if (cif.IsWalkingBackwards())
        {
            /// TODO : replace rb.transform.rotation assignment with rb.MoveRotation()
            /// ( You have to compute the difference quaternion )
            Quaternion t = Quaternion.Euler(new Vector3(0f, cif.GetLookDirection(), 0f));
            rb.transform.rotation = t;

            rb.MovePosition(rb.position - rb.rotation * Vector3.forward * delta * forwardSpeed);
        }

        if (cif.IsStrafingRight())
        {
            /// TODO : replace rb.transform.rotation assignment with rb.MoveRotation()
            /// ( You have to compute the difference quaternion )
            Quaternion t = Quaternion.Euler(new Vector3(0f, cif.GetLookDirection(), 0f));
            rb.transform.rotation = t;

            /*Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * delta);
            rb.MoveRotation(rb.rotation * deltaRotation);*/

            rb.MovePosition(rb.position + rb.rotation * Vector3.right * delta * forwardSpeed/2);
        }

        if (cif.IsStrafingLeft())
        {
            /// TODO : replace rb.transform.rotation assignment with rb.MoveRotation()
            /// ( You have to compute the difference quaternion )
            Quaternion t = Quaternion.Euler(new Vector3(0f, cif.GetLookDirection(), 0f));
            rb.transform.rotation = t;

            /*Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * delta);
            rb.MoveRotation(rb.rotation * deltaRotation);*/

            rb.MovePosition(rb.position - rb.rotation * Vector3.right * delta * forwardSpeed/2);
        }
    }
}
