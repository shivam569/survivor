using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTask : Task 
{
    private NavMeshAgent agent;
    private Vector3 target;
    GameObject pane;
    private Animator am;
    public WalkTask(TaskManager taskManager, Animator am, NavMeshAgent nav, Vector3 tar, GameObject board)
        : base(taskManager)
    {
        agent = nav;
        target = tar;
        pane = board;
        this.am = am;
    }
    public override bool Start()
    {
        pane.SetActive(true);
        EnemyTasks.isWalk = true;
        am.SetBool("isWalk", true);
        am.SetBool("isIdle", false);
        am.SetBool("isAttack", false);
        am.SetBool("isAttackL", false);
        am.SetBool("isAttackH", false);
        agent.SetDestination(new Vector3(target.x + 0.2f, target.y, target.z));
        return true;
    }
}
