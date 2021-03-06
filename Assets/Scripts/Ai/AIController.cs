using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

[RequireComponent(typeof(NavMeshAgent),typeof(SphereCollider))] 
public class AIController : NetworkBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public LayerMask playerLayerMask;

    private SphereCollider sightCollider;


    //Patroling
    [Header("Patroling")] 
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange =5f;

    //Attacking
    [Header("Attacking")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int attackValue = 5;
    //public float attackSphere = 0.5f;
    [SerializeField] private float timeBetweenAttacks=2f;
    [SerializeField] private bool alreadyAttacked;

    //States
    [Header("States")]
    [SerializeField] private float sightRange =14f;
    [SerializeField] private float attackRange =5f;
    [SerializeField] private float playerOutSightRange = 16f;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;

    [Header("Weapon fire")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform witchHandTransform;

    //Health
    [Header("Health")]
    [SerializeField] GameObject hitPrefab;
    [SerializeField] EnemyHealth _health;




    private void Start()
    {
        //target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        sightCollider = GetComponent<SphereCollider>();
        sightCollider.isTrigger = true;
        sightCollider.radius = sightRange;
        witchHandTransform = transform.Find("Hand");
        _health = GetComponent<EnemyHealth>();
        playerLayerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if(isServer == false)
        {
            return;
        }
        if (target != null) 
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if (distanceToPlayer < attackRange) 
            { 
                AttackPlayer();
                
            }
            else
            {
                ChasePlayer();
            }
        }

        else
        { 
            Patroling(); 
        }
        //if (Input.GetKeyDown(KeyCode.F)) { RpcTakeDamage(10f); }
    }

    private void Patroling()
    {

        if (!walkPointSet)
        { 
            SearchWalkPoint(); 
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f))
        {
            walkPointSet = true;
        }
    }

    
    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, target.position) > playerOutSightRange)
        {
            target = null;
            //Debug.Log("Target Lost!");
            return;
        }
        agent.SetDestination(target.position);
    }

    
    private void AttackPlayer()
    {
        Debug.Log("AttackPlayer");

        agent.SetDestination(transform.position);
        LookAtPlayer();
        attackPoint = target;
        if (!alreadyAttacked)
        {
            Collider[] playerColliders = Physics.OverlapSphere(witchHandTransform.position, sightRange, playerLayerMask);

            GameObject fireball = Instantiate(projectile,
            new Vector3(witchHandTransform.position.x, witchHandTransform.position.y, witchHandTransform.position.z),
            witchHandTransform.rotation) as GameObject;
            fireball.transform.parent = null;

            foreach (Collider playerCollider in playerColliders)
            {
                if (playerCollider.GetType() == typeof(SphereCollider))
                {
                    Physics.IgnoreCollision(playerCollider, fireball.GetComponent<SphereCollider>());
                }
            }
            Debug.Log("Spwaned Network fireball");

            NetworkServer.Spawn(fireball);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

/*    void ShootFireball()
    {
        Debug.Log("Shoot fireball");
        GameObject fireball = Instantiate(projectile,
                new Vector3(witchHandTransform.position.x, witchHandTransform.position.y, witchHandTransform.position.z),
                witchHandTransform.rotation) as GameObject;
        fireball.transform.parent = null;
        Physics.IgnoreCollision(target.GetComponent<SphereCollider>(), fireball.GetComponent<SphereCollider>());
        NetworkServer.Spawn(fireball);
    }*/

    
    void LookAtPlayer()
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                            transform.position.y,
                                            target.position.z);
        transform.LookAt(targetPostition);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && target == null)
        {
            target = other.gameObject.transform;
            //Debug.Log("Player encountered");
        }    
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        _health.CurrentHealth -= amount;
        //Instantiate(hitPrefab, impactPoint, transform.rotation);

        if (_health.CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, attackSphere);
    }
}
