using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class FollowTarget : NetworkBehaviour
{
    [SerializeField] private Transform target;
    private float targetDistance;
    private float allowedDistance = 4f;
    private NavMeshAgent nav;
    private Animator anim;
    private bool isSitting;

    // Called only on the server when the pet is created
    // Because only the server has authority over it
    public override void OnStartAuthority()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isSitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer && !isSitting)
        {
            targetDistance = Vector3.Distance(transform.position, target.position);

            if (targetDistance >= allowedDistance)
            {
                nav.isStopped = false;
                nav.SetDestination(target.position);
                anim.SetInteger("Walk", 1);
            }
            else
            {
                anim.SetInteger("Walk", 0);
                nav.isStopped = true;
            }
        }

    }

    // Set target to be followed
    public void SetTarget(Transform tTarget)
    {
        target = tTarget;
    }

    // Pet will stop following target and will sit
    public void StartSit()
    {
        isSitting = true;
        nav.isStopped = true;
        anim.SetInteger("Sit", 1);
    }

    // Pet will stop sitting and will resume following target
    public void StopSit()
    {
        isSitting = false;
        anim.SetInteger("Sit", 0);
    }

    public Animator getAnimator()
    {
        return anim;
    }
}
