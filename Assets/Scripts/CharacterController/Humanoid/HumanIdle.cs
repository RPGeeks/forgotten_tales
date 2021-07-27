using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanIdle : ProceduralAnimation<HumanoidRigidRig>
{
    private HumanoidRigidRig rig;
    private CharacterInputFeed cif;

    private float t = 0;

    public HumanIdle(HumanoidRigidRig rig, CharacterInputFeed cif)
    {
        this.rig = rig;
        this.cif = cif;
    }

    public override void OnAnimationEnd() { }

    public override void OnAnimationStart() 
    {
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
        Yrot1 + HumanoidRigInitialPose.instance.head.y,
        rig.head.localPosition.z
        );

        rig.chest.localPosition = new Vector3(
        rig.chest.localPosition.x,
        Yrot + HumanoidRigInitialPose.instance.chest.y,
        rig.chest.localPosition.z
        );

        rig.belt.localPosition = new Vector3(
        rig.belt.localPosition.x,
        Yrot + HumanoidRigInitialPose.instance.belt.y,
        rig.belt.localPosition.z
        );

        rig.pants.localPosition = new Vector3(
        rig.pants.localPosition.x,
        Yrot + HumanoidRigInitialPose.instance.pants.y,
        rig.pants.localPosition.z
        );

        rig.leftHand.localPosition = new Vector3(
        rig.leftHand.localPosition.x,
        Yrot + HumanoidRigInitialPose.instance.leftHand.y,
        rig.leftHand.localPosition.z
        );

        rig.rightHand.localPosition = new Vector3(
        rig.rightHand.localPosition.x,
        Yrot + HumanoidRigInitialPose.instance.rightHand.y,
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