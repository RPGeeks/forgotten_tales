using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HorseRidigRig : IEntityRig
{
    public HorseRidigRig() { }

    public Transform head;
    public Transform ears;
    public Transform foot_br;
    public Transform foot_fr;

    public Transform jaw;
    public Transform leg_br;
    public Transform leg_fr;
    public Transform neck;

    public Transform tail;
    public Transform torso_back;
    public Transform torso_front;
}