using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootFireball : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Transform enemyTransform;
    [Header("Attack settings")]
    [SerializeField] private float interpolationPeriod;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float timeToAttack;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform witchHandTransform;

    private void Start()
    {
        enemyObject = GameObject.Find("Witch");
        enemyTransform = enemyObject.transform;
        witchHandTransform = GameObject.Find("Hand").transform;
        timeToAttack = 0.0f;
        interpolationPeriod = 1f;
        playerTransform = transform;
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //{
            float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);

            if (distance <= lookRadius)
            {
                CmdLookAtPlayer();

                if (timeToAttack >= interpolationPeriod)
                {
                    CmdShootFireball();
                    timeToAttack = 0.0f;
                }
            }
            timeToAttack += UnityEngine.Time.deltaTime;
        //}
    }

    [Command]
    void CmdShootFireball()
    {
        GameObject fireball = Instantiate(projectile,
                new Vector3(witchHandTransform.position.x, witchHandTransform.position.y, witchHandTransform.position.z),
                witchHandTransform.rotation) as GameObject;
        fireball.transform.parent = null;
        NetworkServer.Spawn(fireball);
    }

    [Command]
    void CmdLookAtPlayer()
    {
        Vector3 targetPostition = new Vector3(playerTransform.position.x,
                                            enemyTransform.position.y,
                                            playerTransform.position.z);
        enemyTransform.LookAt(targetPostition);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyTransform.position, lookRadius);
    }
}
