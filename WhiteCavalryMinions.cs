using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCavalryMinions : MonoBehaviour {

    private float damage;
    private float baseDamage = 15f;
    private float attackRange = 2f;
    private float targetRange;
    private float waitTime;
    private Transform target;
    private NavMeshAgent agent;
    private GameObject townWaypoint;
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private LayerMask town;
    [SerializeField]
    private GameObject particles;

    private void Awake()
    {
        Instantiate(particles, transform.position, particles.transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("GetNearestEnemy", 0f, 0.5f);
        agent = GetComponent<NavMeshAgent>();
        townWaypoint = GameObject.FindGameObjectWithTag("Town");
    }

    private void Update()
    {
        targetRange = 20f + Mathf.Abs(transform.position.y);
        waitTime += Time.deltaTime;
        SetDestination();
        if (Physics.CheckSphere(transform.position, 0.5f, town))
        {
            GameManager.DecreasePopulation(500);
            GameManager.ReceiveCasualties(500);
            Destroy(gameObject);
        }
    }

    void SetDestination()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            float dist = Vector3.Distance(target.position, transform.position);

            if (dist <= attackRange)
            {
                Attack();
                agent.enabled = false;
            }
            else
            {
                agent.enabled = true;
                agent.SetDestination(target.position);
                //Perform Movement Animation
            }

        }
        else
        {
            if (!GetComponent<Rigidbody>())
            {
                agent.enabled = true;
                agent.SetDestination(townWaypoint.transform.position);
                //Perform Movement Animation
            }
        }
    }

    void GetNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetRange, playerMask);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (Collider col in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = col.gameObject;
            }
        }

        if (nearestEnemy != null)
        {
            float dist = Vector3.Distance(nearestEnemy.transform.position, transform.position);
            if (dist <= targetRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    void Attack()
    {
        if (waitTime >= 1f)
        {
            //Perform Attack Animation
            if (target.gameObject.layer == LayerMask.NameToLayer("PlayerSwordsmen"))
            {
                damage = Mathf.RoundToInt(baseDamage * 1.3f);
            }
            else
            {
                damage = baseDamage;
            }
            target.GetComponent<Health>().ReceiveDamage(damage);
            waitTime = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
