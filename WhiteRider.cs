using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRider : MonoBehaviour {

    private float damage = 5f;
    private float attackRange = Mathf.Infinity;
    private float waitTime;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject arrow;
    private GameObject arrowClone;
    [SerializeField]
    private LayerMask playerMask;
    private Collider[] hitColliders;
    private GameObject[] attacks;

    private void Awake()
    {
        name = "whiteRider";
    }

    private void Start()
    {
        InvokeRepeating("GetTarget", 0f, 0.5f);
    }

    private void Update()
    {
        attacks = GameObject.FindGameObjectsWithTag("PlayerGround");
        waitTime += Time.deltaTime;
        if (hitColliders != null)
        {
            Attack();
        }
    }

    void GetTarget()
    {
        hitColliders = Physics.OverlapSphere(transform.position, attackRange, playerMask);
    }

    void Attack()
    {
        if (waitTime >= 5f)
        {
            //Perform Attack Animation
            for (int i = 0; i < attacks.Length; i++)
            {
                Transform target = hitColliders[i].transform;
                Vector3 targetPos = target.position;
                arrowClone = Instantiate(arrow, firePoint.position, arrow.transform.rotation);
                arrowClone.GetComponent<Arrow>().ReceiveTarget(targetPos, target.gameObject, damage);
            }
            waitTime = 0f;
        }
    }
}
