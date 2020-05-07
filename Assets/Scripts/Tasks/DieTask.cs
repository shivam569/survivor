using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTask : Task
{
    Animator am;
    public DieTask(TaskManager taskManager, Animator anim)
        : base(taskManager)
    {
        am = anim;
    }
    public override bool Start()
    {
        am.SetBool("isDead", true);
        return true;
    }

    public override bool End()
    {
        return true;
    }
    // Start is called before the first frame update

}
