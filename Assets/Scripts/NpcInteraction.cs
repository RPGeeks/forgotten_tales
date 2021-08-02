using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NpcInteraction : NetworkBehaviour
{
    [SerializeField] private GameObject dogPrefab;
    private GameObject triggeringNpc;
    private bool triggering;


    // Update is called once per frame
    void Update()
    {
       // if (triggering)
       // {
          // print("Player is triggering with " + triggeringNpc);
       // }

        if (isClient && isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.G) && VirtualInput.IsGameInput)
            {
                CreateDoggy();
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            triggering = false;
            triggeringNpc = null;
        }
    }

    [Command]
    private void CreateDoggy()
    {
        GameObject doggy = Instantiate(dogPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        doggy.GetComponent<FollowTarget>().SetTarget(transform);
        NetworkServer.Spawn(doggy);
    }
}
