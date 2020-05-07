using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task 
{
    public bool isTaskComplete = false;
    protected TaskManager taskManager;
    public Task(TaskManager taskManager)
    {
        this.taskManager = taskManager; 
    }

    public virtual bool Start()
    {
        return true;
    }
    public virtual bool End()
    {
        return true;
    }
    public virtual bool Terminate()
    {
        //TODO
        return true;
    }
}
