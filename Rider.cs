using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rider : MonoBehaviour {
        
    private NavMeshAgent agent;
    private GameObject townWaypoint;
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
        agent = GetComponent<NavMeshAgent>();
        townWaypoint = GameObject.FindGameObjectWithTag("Town");
        agent.SetDestination(townWaypoint.transform.position);
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, 3f, town))
        {
            GameManager.DecreasePopulation(1000);
            GameManager.ReceiveCasualties(1000);
            Destroy(gameObject);
        }   
    }
}
