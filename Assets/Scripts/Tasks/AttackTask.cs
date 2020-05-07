using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTask : Task
{
    Animator am;
    bool isZombie;
    AudioClip heavyZ;
    AudioClip lightZ;
    AudioClip paras;
    AudioSource soundPlayer;
    public AttackTask(TaskManager taskManager, Animator anim, bool isZombie, AudioClip heavy, AudioClip light, AudioClip parasite, AudioSource x)
        : base(taskManager)
    {
        am = anim;
        this.isZombie = isZombie;
        heavyZ = heavy;
        lightZ = light;
        paras = parasite;
        soundPlayer = x;
    }
    void Playsound()
    {
        EnemyTasks.pCounter++;
        if (EnemyTasks.pCounter % 30 == 0)
        {
            soundPlayer.PlayOneShot(paras);
        }
    }
    void decreaseHEnergy()
    {
        EnemyTasks.hCounter++;
        if (EnemyTasks.hCounter % 30 == 0)
        {
            soundPlayer.PlayOneShot(heavyZ);
            EnemyTasks.energyLevel = Mathf.Clamp(EnemyTasks.energyLevel - 15, 0, 100);
        }
    }
    void decreaseLEnergy()
    {
        EnemyTasks.lCounter++;
        if (EnemyTasks.lCounter % 30 == 0)
        {
            EnemyTasks.energyLevel = Mathf.Clamp(EnemyTasks.energyLevel - 8, 0, 100);
            soundPlayer.PlayOneShot(lightZ);
        }
    }
    public override bool Start()
    {
        if (!isZombie)
        {
            am.SetBool("isWalk", false);
            am.SetBool("isIdle", false);
            am.SetBool("isAttack", true);
            EnemyTasks.isAttack = true;
            Playsound();
        }
        else
        {
            if (EnemyTasks.energyLevel <= 50)
            {
                am.SetBool("isWalk", false);
                am.SetBool("isIdle", false);
                am.SetBool("isAttackH", false);
                am.SetBool("isAttackL", true);
                EnemyTasks.isHAttack = false;
                EnemyTasks.isLAttack = true;
                decreaseLEnergy();
            }
            else
            {
                am.SetBool("isWalk", false);
                am.SetBool("isIdle", false);
                am.SetBool("isAttackH", true);
                EnemyTasks.isHAttack = true;
                EnemyTasks.isLAttack = false;
                decreaseHEnergy();
            }
        }
        return true;
    }
    public override bool End()
    {
        return true;
    }
}
