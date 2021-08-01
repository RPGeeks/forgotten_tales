using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    [SerializeField] private HumanoidRigidRig rigParts;

    [SerializeField] private HeadPrefabs headPrefabs;

    private Rigidbody rb;

    public CharacterInputFeed cif;

    private ProceduralAnimationController<HumanoidRigidRig> animationController;
    private MovementController movementController;

    private ProceduralAnimation<HumanoidRigidRig> walkAnim;
    private ProceduralAnimation<HumanoidRigidRig> idleAnim;
    private ProceduralAnimation<HumanoidRigidRig> attackAnim;

    // ---Head sync functionality---
    [SyncVar(hook = nameof(SetGender))]
    int genderIndex = 0;

    [SyncVar(hook = nameof(SetRace))]
    int raceIndex = 0;

    [Command]
    public void CmdSetGender(int _genderIndex)
    {
        this.genderIndex = _genderIndex;
    }

    [Command]
    public void CmdSetRace(int _raceIndex)
    {
        this.raceIndex = _raceIndex;
    }

    [Command]
    public void CmdSetGenderAndRace(int _genderIndex, int _raceIndex)
    {
        this.genderIndex = _genderIndex;
        this.raceIndex = _raceIndex;
    }

    // Clientside SyncVar hook
    void SetGender(int oldGender, int newGender)
    {
        ChangeHeadTo(newGender, raceIndex);
    }

    // Clientside SyncVar hook
    void SetRace(int oldRace, int newRace)
    {
        ChangeHeadTo(genderIndex, newRace);
    }
    // End of ---Head sync functionality---

    void Start()
    {
        CameraController camController;

        camController = Camera.main.GetComponent<CameraController>();

        rb = GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            cif = new LocalKeyboardCIF(camController);
            camController.SetCameraTarget(transform);
            HumanoidRigInitialPose.SetupInstance(rigParts);

            raceIndex = PlayerPrefs.GetInt("RaceSelected", 0);
            genderIndex = PlayerPrefs.GetInt("GenderSelected", 0);

            CmdSetGenderAndRace(genderIndex, raceIndex);

            ChangeHeadTo(genderIndex, raceIndex);
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

    int genderCache = 0;
    int raceCache = 0;
    public void ChangeHeadTo(int gender, int race)
    {
        if (gender == genderCache && race == raceCache) { return; }

        genderCache = gender;
        raceCache = race;

        GameObject newHeadPrefab = headPrefabs.GetHead((Gender)gender, (CharacterRace)race);

        GameObject newHead = Instantiate(newHeadPrefab, transform);
        Destroy(rigParts.head.gameObject);
        rigParts.head = newHead.transform;
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

        if ( isLocalPlayer)
        {
            movementController.Step(Time.deltaTime);
        }
    }
}
