using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWalk : ProceduralAnimation<HumanoidRigidRig>
{
    private HumanoidRigidRig rig;
    private CharacterInputFeed cif;

    private float lastSwayAngle = 0f;
    private float t = 0;
    private float shiftMultiplier = 1f;

    public HumanWalk(HumanoidRigidRig rig, CharacterInputFeed cif)
    {
        this.rig = rig;
        this.cif = cif;
    }

    public override void OnAnimationEnd() { }

    public override void OnAnimationStart() { t = 0; }

    private void setFeetHandsAngles(bool stop)
    {
        float angle = Mathf.Sin(t * 4f);
        float Xrot = 25f * angle;

        if (stop)
        {
            if (Mathf.Abs(lastSwayAngle) < Mathf.Abs(angle))
            {
                return;
            }
        }

        rig.leftFoot.transform.eulerAngles = new Vector3(
        Xrot,
        rig.leftFoot.transform.eulerAngles.y,
        rig.leftFoot.transform.eulerAngles.z
        );

        rig.rightFoot.transform.eulerAngles = new Vector3(
        -Xrot,
        rig.rightFoot.transform.eulerAngles.y,
        rig.rightFoot.transform.eulerAngles.z
        );

        rig.leftHand.transform.eulerAngles = new Vector3(
        Xrot,
        rig.leftHand.transform.eulerAngles.y,
        rig.leftHand.transform.eulerAngles.z
        );

        rig.rightHand.transform.eulerAngles = new Vector3(
        Xrot,
        rig.rightHand.transform.eulerAngles.y,
        rig.rightHand.transform.eulerAngles.z
        );

        lastSwayAngle = angle;
    }

    public override void OnAnimationStep(float delta)
    {
        if (cif.IsSprinting())
        {
            shiftMultiplier = 2f;
        }
        else
        {
            shiftMultiplier = 1f;
        }

        t += delta * shiftMultiplier;

        if (cif.JustStartedWalking())
        {
            t = 0;
        }

        bool swaying = false;

        if (cif.IsWalking() || cif.IsWalkingBackwards())
        {
            swaying = true;
        }

        setFeetHandsAngles(!swaying);
    }
}

