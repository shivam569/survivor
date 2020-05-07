using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private Queue<Task> pendingTasks = new Queue<Task>();
    private Task currentTask=null;
    /*private void Awake()
    {
        currentTask = null;
    }*/
    private void Start()
    {
        StartTask(new Task(this));
    }
    public void StartTask(Task task)
    {
        if (task == null)
            return;
        if (currentTask != null&& currentTask != task)
        {
            if (currentTask.End())
            {
                currentTask = task;
                currentTask.Start();
            }
            else
                pendingTasks.Enqueue(task);
        }
        else
        {
            currentTask = task;
            currentTask.Start();
        }
            
    }
    public void EndTask(Task task)
    {
        if (currentTask != null && currentTask == task)
            currentTask.End();
        else
            return;
    }
    public void OnTaskCompleted(Task task)
    {
        if(task!=null && task == currentTask)
        {
            var nextTask = pendingTasks.Dequeue();
            if (nextTask != null)
            {
                currentTask = nextTask;
                currentTask.Start();
            }
            else
                currentTask = null;
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
