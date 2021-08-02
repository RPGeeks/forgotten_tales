using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCharacter : MonoBehaviour
{
    [SerializeField] private HumanoidRigidRig rigParts;

    [SerializeField] private HeadPrefabs headPrefabs;
    
    public CharacterInputFeed cif;

    private ProceduralAnimationController<HumanoidRigidRig> animationController;

    private ProceduralAnimation<HumanoidRigidRig> idleAnim;

    void Start()
    {
        cif = new EmptyCIF();
        HumanoidRigInitialPose.SetupInstance(rigParts);

        animationController = new ProceduralAnimationController<HumanoidRigidRig>(cif, rigParts);

        idleAnim = new HumanIdle(rigParts, cif);

        animationController.SwitchTo(idleAnim);
    }

    public void ChangeHeadTo(Gender gender, CharacterRace race)
    {
        GameObject newHeadPrefab = headPrefabs.GetHead(gender, race);

        GameObject newHead = Instantiate(newHeadPrefab, transform);
        Destroy(rigParts.head.gameObject);
        rigParts.head = newHead.transform;
    }

    private void Update()
    {
        animationController.Step(Time.deltaTime);
    }
}
