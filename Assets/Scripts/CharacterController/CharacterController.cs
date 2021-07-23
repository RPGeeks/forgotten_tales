using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    [SerializeField] private HumanoidRigidRig rigParts;

    Rigidbody rb;

    public CharacterInputFeed cif;

    private ProceduralAnimationController<HumanoidRigidRig> animationController;
    private MovementController movementController;

    private ProceduralAnimation<HumanoidRigidRig> walkAnim;

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

        animationController.SwitchTo(walkAnim);

        movementController = new MovementController(rb, cif);
    }

    float shiftMultiplier = 1f;

    private void Update()
    {
        animationController.Step(Time.deltaTime);
        movementController.Step(Time.fixedDeltaTime);
    }
}
