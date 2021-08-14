using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : NetworkBehaviour
{
    public Vector3 target;

    const float speed = 2f;
    const float aliveTime = 5f;

    private void Start()
    {
        // Set random up direction so the back fins are randomized ( not in the same orientation )
        Vector3 randomUpDirection = new Vector3(Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360));

        transform.LookAt(target, randomUpDirection);

        if (isServer)
        {
            Invoke(nameof(AutoDestroy), aliveTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
    }

    [Server]
    void AutoDestroy()
    {
        NetworkServer.Destroy(this.gameObject);
    }
}
