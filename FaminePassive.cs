using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaminePassive : MonoBehaviour {

    private float damage = 3f;
    private float range = 5f;
    [SerializeField]
    private LayerMask playerMask;

    private void Start()
    {
        InvokeRepeating("PassiveDamage", 0f, 0.5f);
    }

    void PassiveDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, playerMask);

        foreach(Collider col in hitColliders)
        {
            col.GetComponent<Health>().ReceiveDamage(damage);
        }
    }
}
