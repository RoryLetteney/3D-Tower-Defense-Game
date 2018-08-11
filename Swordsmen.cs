using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swordsmen : MonoBehaviour {

    private float damage;
    private float baseDamage = PlayerUnitStats.swordsmenDamage;
    private float attackRange = 2f;
    private float targetRange;
    private float waitTime;
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask enemyMask;
    private bool riderWave;

    private Vector3 startPosition;

    private void Start()
    {
        InvokeRepeating("GetNearestEnemy", 0f, 0.5f);
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    private void Update()
    {
        riderWave = WaveSpawner.riderWave;
        targetRange = 10f + Mathf.Abs(transform.position.y);
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

        } else
        {
            if (!GetComponent<Rigidbody>())
            {
                agent.enabled = true;
                agent.SetDestination(startPosition);
                //Perform Movement Animation
            }
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
        } else
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

        if (nearestEnemy != null)
        {
            float dist = Vector3.Distance(nearestEnemy.transform.position, transform.position);
            if (dist <= targetRange)
            {
                target = nearestEnemy.transform;
            } else
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
            if (target.gameObject.layer == LayerMask.NameToLayer("EnemyArcher"))
            {
                damage = Mathf.RoundToInt(baseDamage * 1.3f);
            }
            else if (target.gameObject.layer == LayerMask.NameToLayer("EnemyCavalry"))
            {
                damage = Mathf.RoundToInt(baseDamage * 0.7f);
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
