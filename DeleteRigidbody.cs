using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeleteRigidbody : MonoBehaviour {

    private Rigidbody rb;
    private NavMeshAgent agent;
    private NavMeshObstacle obs;
    private float startPos;
    [SerializeField]
    private MonoBehaviour unitScript;
    [SerializeField]
    private GameObject groundImpactParticle;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if (gameObject.layer != LayerMask.NameToLayer("EnemyRider"))
        {
            agent = GetComponent<NavMeshAgent>();
        } else
        {
            obs = GetComponent<NavMeshObstacle>();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(groundImpactParticle, transform.position, groundImpactParticle.transform.rotation);
            Destroy(rb);
            if (gameObject.layer != LayerMask.NameToLayer("PlayerBallista"))
            {
                if (gameObject.layer != LayerMask.NameToLayer("EnemyRider"))
                {
                    agent.enabled = true;
                }
                else
                {
                    obs.enabled = true;
                }
            }
            
            if (unitScript != null)
            {
                unitScript.enabled = true;
            }
        }
    }
}
