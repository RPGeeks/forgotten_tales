using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWalk : ProceduralAnimation<HumanoidRigidRig>
{
    private HumanoidRigidRig rig;
    private CharacterInputFeed cif;

    private float lastSwayAngle = 0f;
    private float t = 0;
    private float shiftMultiplier = 1.8f;

    public HumanWalk(HumanoidRigidRig rig, CharacterInputFeed cif)
    {
        this.rig = rig;
        this.cif = cif;
    }

    bool finished;

    private static readonly float epsilon = 0.01f;

    public override void OnAnimationEnd() { }

    public override void OnAnimationStart() { t = 0; finished = false; }

    private void setFeetHandsAngles(bool stop)
    {
        float angle = Mathf.Sin(t * 8f);
        float Xrot = 25f * angle;

        if (stop)
        {
            if ( Mathf.Abs(angle) <= epsilon)
            {
                finished = true;
            }

            if (Mathf.Abs(lastSwayAngle) < Mathf.Abs(angle))
            {
                return;
            }
        } else
        {
            finished = false;
        }

        rig.chest.localPosition = new Vector3(
        rig.chest.localPosition.x,
        rig.chest.localPosition.y,
        rig.chest.localPosition.z 
        );

        rig.leftFoot.eulerAngles = new Vector3(
        Xrot,
        rig.leftFoot.eulerAngles.y,
        rig.leftFoot.eulerAngles.z 
        );

        rig.rightFoot.eulerAngles = new Vector3(
        -Xrot,
        rig.rightFoot.eulerAngles.y,
        rig.rightFoot.eulerAngles.z
        );

        rig.leftHand.eulerAngles = new Vector3(
        Xrot,
        rig.leftHand.eulerAngles.y,
        rig.leftHand.eulerAngles.z
        );

        rig.rightHand.eulerAngles = new Vector3(
        Xrot,
        rig.rightHand.eulerAngles.y,
        rig.rightHand.eulerAngles.z
        );

        lastSwayAngle = angle;
    }

    public override void OnAnimationStep(float delta)
    {
        if (cif.IsSprinting())
        {
            shiftMultiplier = 1.5f;
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

    public override bool Finished()
    {
        return finished;
    }
}

