using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public bool isZombie; 
    public Animator anim;
    private HealthSystem healthSystem;
    NavMeshAgent nav;
    bool isStand = false;
    float dist;
    bool isWalk = false;
    public GameObject player;
    public Transform Target;
    public GameObject minimapPointer;
    bool reached = false;
    public static float energyLevel;
    public static bool isHAttack;
    public static bool isLAttack;
    public static bool isAttack;
    public Text health;
    public Text energy;
    int hCounter;
    int lCounter;
    int pCounter;
    public GameObject deadBoard;
    public GameObject panel;
    public AudioClip lightZ;
    public AudioClip heavyZ;
    public AudioClip para;
    AudioSource soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();   
    }
    public void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        hCounter = 0;
        lCounter = 0;
        pCounter = 0;
        energyLevel = 100;
        isHAttack = false;
        isLAttack = false;
        healthSystem = GetComponent<HealthSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isStand)
            {
                minimapPointer.SetActive(true);
                isStand = true;
                anim.SetBool("isStand",true);
                if (!isZombie)
                {
                    Invoke("Walking", 8f);
                }
                else
                    Invoke("Walking", 3f);
            }
        }
    }
    void Walking()
    {
        energyLevel = 100;
        anim.SetBool("isWalk", true);
        isWalk = true;
        panel.SetActive(true);
    }
    void decreaseHEnergy()
    {
        hCounter++;
        if (hCounter % 30 == 0)
        {
            soundPlayer.PlayOneShot(heavyZ);
            energyLevel = Mathf.Clamp(energyLevel - 15, 0, 100);
        }
    }
    void decreaseLEnergy()
    {
        lCounter++;
        if (lCounter % 30 == 0)
        {
            energyLevel = Mathf.Clamp(energyLevel - 8, 0, 100);
            soundPlayer.PlayOneShot(lightZ);
        }
    }
    void Playsound()
    {
        pCounter++;
        if (pCounter % 30 == 0)
        {
            soundPlayer.PlayOneShot(para);
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
            //Debug.Log(dist);
            if (isWalk && healthSystem.GetHealth() > 0)
            {
                if (reached == false)
                {
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isIdle", false);
                    if (!isZombie)
                    {
                        anim.SetBool("isAttack", false);
                        isAttack = false;
                    }
                    else
                    {
                        isHAttack = false;
                        isLAttack = false;
                        anim.SetBool("isAttackL", false);
                        anim.SetBool("isAttackH", false);
                    }
                    nav.SetDestination(new Vector3(Target.position.x + 0.2f, Target.position.y, Target.position.z));
                    if (dist <= 3.5f)
                        reached = true;
                }
                else
                {
                    if (!isZombie)
                    {
                        anim.SetBool("isWalk", false);
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isAttack", true);
                        Invoke("Playsound",0);
                        isAttack = true;
                        if (dist > 3.5f)
                            reached = false;
                    }
                    else
                    {
                        if (energyLevel <= 50)
                        {
                            anim.SetBool("isWalk", false);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttackL", true);
                            Invoke("decreaseLEnergy", 0.4f);
                            isHAttack = false;
                            isLAttack = true;
                            if (dist > 3.5f)
                                reached = false;
                        }
                        else
                        {
                            anim.SetBool("isWalk", false);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttackH", true);
                            Invoke("decreaseHEnergy", 0.4f);
                            isHAttack = true;
                            isLAttack = false;
                            if (dist > 3.5f)
                                reached = false;
                        }
                    }
                }
                if (dist > 100)
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalk", false);
                    if (!isZombie)
                    {
                        anim.SetBool("isAttack", false);
                        isAttack = false;
                    }
                    else
                    {
                        anim.SetBool("isAttackL", false);
                        anim.SetBool("isAttackH", false);
                    }
                }
                if (energyLevel <= 50)
                {
                    anim.SetBool("isAttackH", false);
                    isHAttack = false;
                }
            }
            health.text = "Health: " + healthSystem.GetHealth().ToString();
            if (isZombie)
            {
                energy.text = "Energy: " + energyLevel.ToString();
            }
            if (healthSystem.GetHealth() == 0)
            {
                anim.SetBool("isDead", true);
                minimapPointer.SetActive(false);
                Invoke("Dead", 1.6f);
                panel.SetActive(false);
            }
        }
    }
}
