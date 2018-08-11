using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Vector3 targetPos;
    private GameObject target;
    private float damage;
    private float speed;

    private void Start()
    {
        if (name == "RiderArrow")
        {
            speed = 120f;
        } else
        {
            speed = 30f;
        }
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            target.GetComponent<Health>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
        
    }

    public void ReceiveTarget(Vector3 targetEnemy, GameObject targetGO, float dmg)
    {
        targetPos = targetEnemy;
        target = targetGO;
        damage = dmg;
    }
}
