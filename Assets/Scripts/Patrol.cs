using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] WayPoints;
    private Queue<Transform> queue = new Queue<Transform>();
    public float speed = 5.0f;
    private Transform currentWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < WayPoints.Length; i++)
            queue.Enqueue(WayPoints[i]);
        currentWayPoint = queue.Dequeue();
        queue.Enqueue(currentWayPoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWayPoint.position) <= Mathf.Epsilon)
        {
            currentWayPoint = queue.Dequeue();
            queue.Enqueue(currentWayPoint);
        }
    }
}
