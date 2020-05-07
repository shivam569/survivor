using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpTask : Task
{
    private Animator am;
    float time;
    public StandUpTask(TaskManager taskManager, Animator animator, float animationTime)
        :base(taskManager)
    {
        am = animator;
        time = animationTime;
    }
    public override bool Start()
    {
        EnemyTasks.energyLevel = 100;
        taskManager.StartCoroutine(StartAnimating());
        return true;
    }
    IEnumerator StartAnimating()
    {
        am.SetBool("isStand", true);
        yield return new WaitForSeconds(time);
        isTaskComplete = true;
        taskManager.OnTaskCompleted(this);
    }
    public override bool End()
    {
        return isTaskComplete;
    }
}
