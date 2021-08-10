using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : NetworkBehaviour
{
    private const float lifetime = 5f;

    void Start()
    {
        if( isServer)
        {
            Invoke(nameof(EndOfLife), lifetime);
        }
    }

    private void EndOfLife()
    {
        NetworkServer.Destroy(gameObject);
    }
}
