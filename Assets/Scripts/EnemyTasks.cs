using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTasks : MonoBehaviour
{
    TaskManager taskManager;
    bool isStand = false;
    float dist;
    public static int hCounter ;
    public static int lCounter ;
    public static int pCounter ;
    public static bool isWalk = false;
    public bool isZombie;
    private HealthSystem healthSystem;
    NavMeshAgent nav;
    public GameObject player;
    public Transform Target;
    public GameObject panel;
    public static bool isHAttack;
    public static bool isLAttack;
    public static bool isAttack;
    public Text health;
    public Text energy;
    public GameObject minimapPointer;
    public GameObject deadBoard;
    bool reached = false;
    public static float energyLevel;
    public AudioClip lightZ;
    public AudioClip heavyZ;
    public AudioClip para;
    AudioSource soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        lCounter = 0;
        hCounter = 0;
        pCounter = 0;
        isAttack = false;
        isHAttack = false;
        isLAttack = false;
        healthSystem = GetComponent<HealthSystem>();
        taskManager = GetComponent<TaskManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isStand)
            {
                minimapPointer.SetActive(true);
                isStand = true;
                if (!isZombie)
                {
                    Debug.Log("Parasite set active");
                    taskManager.StartTask(new StandUpTask(taskManager, GetComponent<Animator>(), 8));
                    taskManager.StartTask(new WalkTask(taskManager, GetComponent<Animator>(), GetComponent<NavMeshAgent>(), Target.position,panel));
                }
                else
                {
                    Debug.Log("Zombie set active");
                    taskManager.StartTask(new StandUpTask(taskManager, GetComponent<Animator>(), 4));
                    taskManager.StartTask(new WalkTask(taskManager, GetComponent<Animator>(), GetComponent<NavMeshAgent>(), Target.position,panel));
                }
            }
        }
    }
    void Dead()
    {
        deadBoard.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.isAlive == true)
        {
            dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (isWalk && healthSystem.GetHealth() > 0)
            {
                if (reached == false)
                {
                    if(isStand)
                        taskManager.StartTask(new WalkTask(taskManager, GetComponent<Animator>(), GetComponent<NavMeshAgent>(), Target.position, panel));
                    if (dist <= 3.5f)
                        reached = true;
                }
                else
                {
                    if (!isZombie)
                    {
                        taskManager.StartTask(new AttackTask(taskManager, GetComponent<Animator>(), isZombie, heavyZ, lightZ, para, soundPlayer));
                        if (dist > 3.5f)
                            reached = false;
                    }
                    else
                    {
                        if (energyLevel <= 50)
                        {
                            taskManager.StartTask(new AttackTask(taskManager, GetComponent<Animator>(), isZombie, heavyZ, lightZ, para, soundPlayer));
                            if (dist > 3.5f)
                                reached = false;
                        }
                        else
                        {
                            taskManager.StartTask(new AttackTask(taskManager, GetComponent<Animator>(), isZombie, heavyZ, lightZ, para, soundPlayer));
                            if (dist > 3.5f)
                                reached = false;
                        }
                    }
                }
                if (dist > 100)
                {
                    taskManager.StartTask(new IdleTask(taskManager, GetComponent<Animator>(), isZombie));
                }
            }
            health.text = "Health: " + healthSystem.GetHealth().ToString();
            if (isZombie)
            {
                energy.text = "Energy: " + energyLevel.ToString();
            }
            if (healthSystem.GetHealth() == 0)
            {
                taskManager.StartTask(new DieTask(taskManager, GetComponent<Animator>()));
                minimapPointer.SetActive(false);
                Invoke("Dead", 1.6f);
                panel.SetActive(false);
            }
        }
    }
}
