using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyTemp : MonoBehaviour
{
    Animator a;
    public Transform targettrans;
    TaskManager taskManager = null;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        taskManager = GetComponent<TaskManager>();
        Invoke("OnPlayerInRange", 3);
    }
    void OnPlayerInRange()
    {
        taskManager.StartTask(new StandUpTask(taskManager, GetComponent<Animator>(),6));
    }
    // Update is called once per frame
    void Update()
    {
        a.SetFloat("speed", a.velocity.magnitude);
    }
}
