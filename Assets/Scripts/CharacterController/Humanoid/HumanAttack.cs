using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : ProceduralAnimation<HumanoidRigidRig>
{
    private HumanoidRigidRig rig;
    private CharacterInputFeed cif;

    private float t = 0;

    public HumanAttack(HumanoidRigidRig rig, CharacterInputFeed cif)
    {
        this.rig = rig;
        this.cif = cif;
    }

    public override void OnAnimationEnd() { }

    public override void OnAnimationStart()
    {
        t = 0;
    }

    private void setHandAngle()
    {
        float angle = Mathf.Sin(t * 8f);
        float Xrot = - 90f * (1 + angle) / 2f;

        rig.rightHand.eulerAngles = new Vector3(
        Xrot,
        rig.rightHand.eulerAngles.y,
        rig.rightHand.eulerAngles.z
        );
    }

    public override void OnAnimationStep(float delta)
    {
        t += delta;
        setHandAngle();
    }

    public override bool Finished()
    {
        return t > Mathf.PI*(3f/2f)/8f;
    }
}
