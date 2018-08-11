using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteArcherMinions : MonoBehaviour {

    private float damage;
    private float baseDamage = 13f;
    private float attackRange;
    private float targetRange;
    private float waitTime;
    private Transform target;
    private Vector3 targetPos;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject arrow;
    private GameObject arrowClone;
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
        attackRange = 10f + Mathf.Abs(transform.position.y);
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
            }
            //Perform Movement Animation
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
            if (target.gameObject.layer == LayerMask.NameToLayer("PlayerSwordsmen"))
            {
                damage = Mathf.RoundToInt(baseDamage * 0.7f);
            }
            else if (target.gameObject.layer == LayerMask.NameToLayer("PlayerCavalry"))
            {
                damage = Mathf.RoundToInt(baseDamage * 1.3f);
            }
            else
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
