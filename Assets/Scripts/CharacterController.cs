using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    Rigidbody rb;
    Vector3 m_EulerAngleVelocity;

    // SerializeField later
    float forwardSpeed = 3f;

    float shiftMultiplier = 1f;

    [SerializeField] GameObject leftFoot;
    [SerializeField] GameObject rightFoot;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        m_EulerAngleVelocity = new Vector3(0, 40, 0);
    }

    float lastSwayAngle = 0f;

    void setFeetHandsAngles(bool stop)
    {
        float angle = Mathf.Sin(t * 4f);
        float Xrot = 25f * angle;

        if (stop)
        {
            if ( Mathf.Abs(lastSwayAngle) < Mathf.Abs(angle))
            {
                return;
            }
        }

        leftFoot.transform.eulerAngles = new Vector3(
        Xrot,
        leftFoot.transform.eulerAngles.y,
        leftFoot.transform.eulerAngles.z
        );

        rightFoot.transform.eulerAngles = new Vector3(
        -Xrot,
        rightFoot.transform.eulerAngles.y,
        rightFoot.transform.eulerAngles.z
        );

        leftHand.transform.eulerAngles = new Vector3(
        Xrot,
        leftHand.transform.eulerAngles.y,
        leftHand.transform.eulerAngles.z
        );

        rightHand.transform.eulerAngles = new Vector3(
        Xrot,
        rightHand.transform.eulerAngles.y,
        rightHand.transform.eulerAngles.z
        );

        lastSwayAngle = angle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                shiftMultiplier = 2f;
            }
            else
            {
                shiftMultiplier = 1f;
            }

            t += Time.fixedDeltaTime * shiftMultiplier;

            bool swaying = false;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                t = 0;
            }

            if (Input.GetKey(KeyCode.W))
            {
                swaying = true;
                rb.MovePosition(rb.position + rb.rotation * Vector3.forward * shiftMultiplier * Time.fixedDeltaTime * forwardSpeed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                swaying = true;
                rb.MovePosition(rb.position - rb.rotation * Vector3.forward * shiftMultiplier * Time.fixedDeltaTime * forwardSpeed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * shiftMultiplier * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * shiftMultiplier * Time.fixedDeltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            setFeetHandsAngles(!swaying);
        }
        
    }
}
