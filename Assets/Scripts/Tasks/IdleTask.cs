using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTask : Task
{
    Animator am;
    bool isZombie;
    public IdleTask(TaskManager taskManager, Animator anim, bool isZombie)
        : base(taskManager)
    {
        am = anim;
        this.isZombie = isZombie;
    }
    public override bool Start()
    {
        am.SetBool("isIdle", true);
        am.SetBool("isWalk", false);
        if (!isZombie)
        {
            am.SetBool("isAttack", false);
            EnemyTasks.isAttack = false;
        }
        else
        {
            am.SetBool("isAttackL", false);
            am.SetBool("isAttackH", false);
            EnemyTasks.isLAttack = false;
            EnemyTasks.isHAttack = false;
        }
        return true;
    } 
    public override bool End()
    {
        return true;
    }
}
