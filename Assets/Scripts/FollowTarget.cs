using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class FollowTarget : NetworkBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent nav;

    // Called only on the server when the dog is created
    // Because only the server has authority over it
    public override void OnStartAuthority()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

   // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            nav.SetDestination(target.position);
        }
        
    }

    public void SetTarget(Transform tTarget) 
    {
        target = tTarget;
    }
}
