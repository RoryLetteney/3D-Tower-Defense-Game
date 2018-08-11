using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archers : MonoBehaviour {

    private float damage;
    private float baseDamage = PlayerUnitStats.archerDamage;
    private float attackRange;
    private float targetRange;
    private float waitTime;
    private Transform target;
    private Vector3 targetPos;
    private Vector3 startPosition;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject arrow;
    private GameObject arrowClone;
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask enemyMask;
    private bool riderWave;

    private void Start()
    {
        InvokeRepeating("GetNearestEnemy", 0f, 0.5f);
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        riderWave = WaveSpawner.riderWave;
        attackRange = 10f + Mathf.Abs(transform.position.y);
        targetRange = 20f + Mathf.Abs(transform.position.y);
        waitTime += Time.deltaTime;
        SetDestination();
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
                agent.SetDestination(startPosition);
            }            
            //Perform Movement Animation
        }
    }

    void GetNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetRange, enemyMask);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        if (!riderWave)
        {
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.layer != LayerMask.NameToLayer("EnemyRider"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = col.gameObject;
                    }
                }
            }
        }
        else
        {
            foreach (Collider col in hitColliders)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, col.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = col.gameObject;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= targetRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Attack()
    {
        if (waitTime >= 1f)
        {
            //Perform Attack Animation
            if (target.gameObject.layer == LayerMask.NameToLayer("EnemySwordsmen"))
            {
                damage = Mathf.RoundToInt(baseDamage * 0.7f);
            } else if (target.gameObject.layer == LayerMask.NameToLayer("EnemyCavalry"))
            {
                damage = Mathf.RoundToInt(baseDamage * 1.3f);
            } else
            {
                damage = baseDamage;
            }
            targetPos = target.position;
            arrowClone = Instantiate(arrow, firePoint.position, arrow.transform.rotation);
            arrowClone.GetComponent<Arrow>().ReceiveTarget(targetPos, target.gameObject, damage);
            waitTime = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
