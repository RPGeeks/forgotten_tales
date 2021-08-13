using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    public HumanoidRigidRig rigParts;

    private Rigidbody rb;

    public CharacterInputFeed cif;

    private ProceduralAnimationController<HumanoidRigidRig> animationController;
    private MovementController movementController;

    private ProceduralAnimation<HumanoidRigidRig> walkAnim;
    private ProceduralAnimation<HumanoidRigidRig> idleAnim;
    private ProceduralAnimation<HumanoidRigidRig> attackAnim;

    private CharacterOutfitSync characterOutfit;

    private float cooldownTime = 0.6f;
    private float nextAttack = 0;

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject arrowPrefab;

    void Start()
    {
        CameraController camController;

        camController = Camera.main.GetComponent<CameraController>();

        rb = GetComponent<Rigidbody>();

        characterOutfit = GetComponent<CharacterOutfitSync>();

        if (isLocalPlayer)
        {
            cif = new LocalKeyboardCIF(camController);
            camController.SetCameraTarget(transform);
            HumanoidRigInitialPose.SetupInstance(rigParts);

            characterOutfit.LocalInit();
        }
        else
        {
            cif = GetComponent<CIFSync>();// new NetworkedCIF();
        }

        animationController = new ProceduralAnimationController<HumanoidRigidRig>(cif, rigParts);

        walkAnim = new HumanWalk(rigParts, cif);
        attackAnim = new HumanAttack(rigParts, cif);
        idleAnim = new HumanIdle(rigParts, cif);

        animationController.SwitchTo(walkAnim);
        //animationController.SwitchTo(attackAnim);

        movementController = new MovementController(rb, cif);
    }

    private void Update()
    {
        if (Time.time > nextAttack)
        {
            if (cif.AttemptsAttack())
            {
                animationController.SwitchTo(attackAnim);

                if (isLocalPlayer)
                {
                    Debug.Log("Cmd attack - clientside");

                    if (characterOutfit.GetClassIndex() == (int)CharacterClass.Mage)
                    {
                        CmdMageAttack();
                    }

                    if (characterOutfit.GetClassIndex() == (int)CharacterClass.Archer)
                    {
                        Vector3 target;
                        if (BowRaycast(out target))
                        {
                            CmdArcheryAttack(target);
                        }
                    }


                }

                nextAttack = Time.time + cooldownTime;
            }
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

        if ( isLocalPlayer)
        {
            movementController.Step(Time.deltaTime);
        }
    }

    [Command]
    private void CmdMageAttack()
    {
        Debug.Log("Cmd attack - serverside");
        if (characterOutfit.GetClassIndex() == (int)CharacterClass.Mage)
        {
            Vector3 spawnPosition = transform.position + transform.rotation * Vector3.forward * 2f;
            GameObject fireball = Instantiate(fireballPrefab, spawnPosition, transform.rotation);
            NetworkServer.Spawn(fireball);

            fireball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 5, 10), ForceMode.Impulse);  
        }

    }

    [Command]
    private void CmdArcheryAttack(Vector3 raycastedTarget)
    {
        if (characterOutfit.GetClassIndex() == (int)CharacterClass.Archer)
        {
            Vector3 spawnPosition = transform.position + transform.rotation * Vector3.forward * 1f;
            GameObject arrow = Instantiate(arrowPrefab, spawnPosition, transform.rotation);
            NetworkServer.Spawn(arrow);

            arrow.GetComponent<ArrowController>().target = raycastedTarget;
        }
    }

    // Raycasts a ray from the camera middle point in the scene and returns contact point
    private bool BowRaycast(out Vector3 point)
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(
            new Vector3(Camera.main.scaledPixelWidth/2, Camera.main.scaledPixelHeight/2, 0));

        RaycastHit rayinfo;

        var results = Physics.RaycastAll(rayOrigin, 100f);

        foreach (var i in results)
        {
            if (i.collider != null && i.collider.gameObject != gameObject)
            {
                //Vector3 direction = rayinfo.point - Camera.main.transform.position;

                point = i.point;
                return true;
            }
        }

        point = Vector3.zero;
        return false;
    }

    /*
    private void OnDrawGizmos()
    {
        Vector3 point;
        if ( BowRaycast(out point) )
        {
            Gizmos.DrawSphere(point, 1);
        }
        
    }
    */
}
