using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private static float damage = 0f;
    private GameObject[] walls;
    [SerializeField]
    private LayerMask enemyMask;

    private void Awake()
    {
        name = "PlayerWall";
    }

    private void Start()
    {
        InvokeRepeating("WallDamage", 0f, 0.5f);
    }

    private void Update()
    {
        walls = GameObject.FindGameObjectsWithTag("PlayerWall");
    }

    void WallDamage()
    {
        if (damage > 0f)
        {
            foreach (GameObject wall in walls)
            {
                Collider[] hitColliders = Physics.OverlapBox(wall.transform.position, new Vector3(2f, 1f, 3.5f), Quaternion.identity, enemyMask);
                foreach (Collider col in hitColliders)
                {
                    col.GetComponent<Health>().ReceiveDamage(damage);
                }
            }
        }
    }

    public static void ReceiveDamageIncrease(float amount)
    {
        damage += amount;
    }

}
