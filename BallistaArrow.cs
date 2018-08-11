using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaArrow : MonoBehaviour {

    private float speed = 70f;
    private float damage = 40f;
    private float damageRange = 3f;
    private Vector3 target;
    [SerializeField]
    private LayerMask enemyMask;

    private void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRange, enemyMask);
            foreach (Collider col in hitColliders)
            {
                col.GetComponent<Health>().ReceiveDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void ReceiveTarget(Vector3 targetVector)
    {
        target = targetVector;
    }
}
