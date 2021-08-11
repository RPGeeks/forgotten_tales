using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class LookAtPlayer : NetworkBehaviour
{
    //private NetworkManager networkManager = NetworkManager.singleton;
    //private List<PlayerController> playerControllers = networkManager.client.connection.playerController;
    public Transform target;
    public float lookRadius = 10f;

    void Start()
    {
        target = GameObject.Find("Character").transform;
    }

    void Update()
    {
        if (target == null)
        { 
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            Vector3 targetPostition = new Vector3(target.position.x,
                                            this.transform.position.y,
                                            target.position.z);
            this.transform.LookAt(targetPostition);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
