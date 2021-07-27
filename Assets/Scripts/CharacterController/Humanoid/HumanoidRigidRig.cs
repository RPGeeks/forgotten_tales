using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanoidRigidRig : IEntityRig
{
    public HumanoidRigidRig() { }

    public Transform head;
    public Transform chest;
    public Transform belt;
    public Transform pants;

    public Transform leftFoot;
    public Transform rightFoot;

    public Transform leftHand;
    public Transform rightHand;

    public Transform leftShoulder;
    public Transform rightShoulder;
}

public class HumanoidRigInitialPose
{
    public static HumanoidRigInitialPose instance = new HumanoidRigInitialPose();

    public static void SetupInstance(HumanoidRigidRig rig)
    {
        instance.head = rig.head.localPosition;
        instance.chest = rig.chest.localPosition;
        instance.belt = rig.belt.localPosition;
        instance.pants = rig.pants.localPosition;

        instance.leftFoot = rig.leftFoot.localPosition;
        instance.rightFoot = rig.rightFoot.localPosition;

        instance.leftHand = rig.leftHand.localPosition;
        instance.rightHand = rig.rightHand.localPosition;

        instance.leftShoulder = rig.leftShoulder.localPosition;
        instance.rightShoulder = rig.rightShoulder.localPosition;
    }

    public Vector3 head;
    public Vector3 chest;
    public Vector3 belt;
    public Vector3 pants;

    public Vector3 leftFoot;
    public Vector3 rightFoot;

    public Vector3 leftHand;
    public Vector3 rightHand;

    public Vector3 leftShoulder;
    public Vector3 rightShoulder;
}