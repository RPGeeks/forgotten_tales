using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanIdle : ProceduralAnimation<HumanoidRigidRig>
{
    private HumanoidRigidRig rig;
    private CharacterInputFeed cif;

    private Vector3 initialHeadPos;
    private Vector3 initialChestPos;
    private Vector3 initialBeltPos;
    private Vector3 initialPantsPos;
    private Vector3 initialLeftHandPos;
    private Vector3 initialRightHandPos;

    private float t = 0;

    public HumanIdle(HumanoidRigidRig rig, CharacterInputFeed cif)
    {
        this.rig = rig;
        this.cif = cif;
    }

    public override void OnAnimationEnd() { }

    public override void OnAnimationStart() 
    {
        // TODO : Create an initial pose transform list that is globally-accessible 
        // ( So you can create mathematically-defined animations
        // based on the difference between your desired pose and the initial pose ).
        initialHeadPos = rig.head.localPosition;
        initialChestPos = rig.chest.localPosition;
        initialBeltPos = rig.belt.localPosition;
        initialPantsPos = rig.pants.localPosition;
        initialLeftHandPos = rig.leftHand.localPosition;
        initialRightHandPos = rig.rightHand.localPosition;

        t = 0;
    }

    private void setBodyAngles()
    {
        float angle = Mathf.Sin(t * 2f);
        float Yrot = 0.03f * angle;

        float angle1 = Mathf.Sin(t * 2f + 0.05f);
        float Yrot1 = 0.05f * angle1;

        rig.head.localPosition = new Vector3(
        rig.head.localPosition.x,
        Yrot1 + initialHeadPos.y,
        rig.head.localPosition.z
        );

        rig.chest.localPosition = new Vector3(
        rig.chest.localPosition.x,
        Yrot + initialChestPos.y,
        rig.chest.localPosition.z
        );

        rig.belt.localPosition = new Vector3(
        rig.belt.localPosition.x,
        Yrot + initialBeltPos.y,
        rig.belt.localPosition.z
        );

        rig.pants.localPosition = new Vector3(
        rig.pants.localPosition.x,
        Yrot + initialPantsPos.y,
        rig.pants.localPosition.z
        );

        rig.leftHand.localPosition = new Vector3(
        rig.leftHand.localPosition.x,
        Yrot + initialLeftHandPos.y,
        rig.leftHand.localPosition.z
        );

        rig.rightHand.localPosition = new Vector3(
        rig.rightHand.localPosition.x,
        Yrot + initialRightHandPos.y,
        rig.rightHand.localPosition.z
        );
    }

    public override void OnAnimationStep(float delta)
    {
        t += delta;
        setBodyAngles();
    }

    public override bool Finished()
    {
        return false;
    }
}