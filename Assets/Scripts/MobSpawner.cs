using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : NetworkBehaviour
{
    public GameObject[] enemyMobs;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            var instantiate = Instantiate(enemyMobs[Random.Range(0,9)], transform.position, transform.rotation);
            NetworkServer.Spawn(instantiate);
        }
    }

}
