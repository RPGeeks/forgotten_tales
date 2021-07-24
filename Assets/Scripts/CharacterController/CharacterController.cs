using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    [SerializeField] private HumanoidRigidRig rigParts;

    private Rigidbody rb;

    private CharacterInputFeed cif;

    private ProceduralAnimationController<HumanoidRigidRig> animationController;
    private MovementController movementController;

    private ProceduralAnimation<HumanoidRigidRig> walkAnim;
    private ProceduralAnimation<HumanoidRigidRig> idleAnim;
    private ProceduralAnimation<HumanoidRigidRig> attackAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            cif = new LocalKeyboardCIF();
        } else
        {
            cif = new EmptyCIF();
        }

        animationController = new ProceduralAnimationController<HumanoidRigidRig>(cif, rigParts);

        walkAnim = new HumanWalk(rigParts, cif);
        attackAnim = new HumanAttack(rigParts, cif);
        idleAnim = new HumanIdle(rigParts, cif);

        animationController.SwitchTo(walkAnim);
        //animationController.SwitchTo(attackAnim);

        movementController = new MovementController(rb, cif);

        Camera.main.GetComponent<CameraController>().SetCameraTarget(transform);
    }

    private void Update()
    {
        if (cif.AttemptsAttack())
        {
            animationController.SwitchTo(attackAnim);
        }

        if (animationController.GetCurrentAnim() == attackAnim)
        {
            if (animationController.Finished())
            {
                animationController.SwitchTo(walkAnim);
            }
        }

        if (animationController.GetCurrentAnim() == walkAnim)
        {
            if (!cif.IsWalking() && !cif.IsWalkingBackwards())
            {
                if ( animationController.Finished())
                    animationController.SwitchTo(idleAnim);
            }
        }

        if (animationController.GetCurrentAnim() == idleAnim)
        {
            if (cif.JustStartedWalking())
            {
                animationController.SwitchTo(walkAnim);
            }
        }
        

        animationController.Step(Time.deltaTime);
        movementController.Step(Time.deltaTime);
    }
}