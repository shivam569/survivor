using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform Destination;
    public GameObject Obstacle;
    // Start is called before the first frame update
    void Start()
    {
        var go = Instantiate(Obstacle) as GameObject;
        go.transform.position = new Vector3(Destination.position.x + 5, Destination.position.y, Destination.position.z - 15);
        var navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(Destination.position);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
