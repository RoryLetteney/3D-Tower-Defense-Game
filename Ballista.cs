using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour {
    
    private float attackRange;
    private float waitTime;
    private Transform target;
    private Vector3 nullVector;
    [SerializeField]
    private GameObject ballistaArrow;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private LayerMask enemyMask;
    private bool riderWave;

    private void Awake()
    {
        name = "PlayerBallista";
    }

    private void Start()
    {
        InvokeRepeating("GetNearestEnemy", 0f, 0.5f);
    }

    private void Update()
    {
        riderWave = WaveSpawner.riderWave;
        attackRange = 70f + Mathf.Abs(transform.position.y);
        waitTime += Time.deltaTime;

        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            Attack();
        }
    }

    void GetNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyMask);
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

        if (nearestEnemy != null)
        {
            float dist = Vector3.Distance(nearestEnemy.transform.position, transform.position);
            if (dist <= attackRange)
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
        if (waitTime >= 4f)
        {
            GameObject ballistaArrowClone = Instantiate(ballistaArrow, firePoint.position, ballistaArrow.transform.rotation);
            ballistaArrowClone.GetComponent<BallistaArrow>().ReceiveTarget(target.position);
            waitTime = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
