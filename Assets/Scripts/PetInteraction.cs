using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PetInteraction : NetworkBehaviour
{
    [SerializeField] private GameObject dogPrefab;
    [SerializeField] private GameObject catPrefab;
    private bool isPetSpawned;
    private GameObject triggeringNpc;
    private bool triggering;


    // Update is called once per frameuni
    void Update()
    {

        if (isClient && isLocalPlayer)
        {
            if (triggering)
            {
                if (Input.GetKeyDown(KeyCode.F3))
                {
                    CmdSit();
                }
            }
            if (Input.GetKeyDown(KeyCode.F1) && !isPetSpawned)
            {
                CreateDog();
                isPetSpawned = true;
            }
            else if (Input.GetKeyDown(KeyCode.F2) && !isPetSpawned)
            {
                CreateCat();
                isPetSpawned = true;
            }
        }

    }

    [Command]
    private void CmdSit()
    {
        Sit();
    }

    [ClientRpc]
    private void Sit()
    {
        // Pet will sit if it's in Idle animation
        if (triggeringNpc.GetComponent<FollowTarget>().getAnimator().GetCurrentAnimatorStateInfo(0).IsName("DogIdle")
            || triggeringNpc.GetComponent<FollowTarget>().getAnimator().GetCurrentAnimatorStateInfo(0).IsName("CatIdle"))
        {
            triggeringNpc.GetComponent<FollowTarget>().StartSit();
        }
        // If Pet is already sitting it will get up and follow Player
        else if (triggeringNpc.GetComponent<FollowTarget>().getAnimator().GetCurrentAnimatorStateInfo(0).IsName("DogSit")
            || triggeringNpc.GetComponent<FollowTarget>().getAnimator().GetCurrentAnimatorStateInfo(0).IsName("CatSit"))
        {
            triggeringNpc.GetComponent<FollowTarget>().StopSit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pet")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pet")
        {
            triggering = false;
            triggeringNpc = null;
        }
    }

    [Command]
    private void CreateDog()
    {
        GameObject dog = Instantiate(dogPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        dog.GetComponent<FollowTarget>().SetTarget(gameObject);
        NetworkServer.Spawn(dog);
    }

    [Command]
    private void CreateCat()
    {
        GameObject cat = Instantiate(catPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        cat.GetComponent<FollowTarget>().SetTarget(gameObject);
        NetworkServer.Spawn(cat);
    }
}
