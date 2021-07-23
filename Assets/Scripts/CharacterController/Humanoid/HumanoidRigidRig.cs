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