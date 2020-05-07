using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static bool isAlive;
    public float speed = 2.0f;
    public float jumpspeed;
    private CharacterController cc;
    private Vector3 moveDirection;
    public Animator am;
    private HealthSystem healthSystem;
    private HungerSystem hungerSystem;
    private Backpack backpack;
    public UIManager UIManager;
    float EnergyLevel = 100;
    float counter = 0;
    float counter1 = 0;
    public Text EnergyText;
    bool isRunning = false;
    public GameObject arm;
    public GameObject leg;
    Collider punch;
    Collider kick;
    public AudioClip Punching;
    public AudioClip Kicking;
    AudioSource soundPlayer;
    void Start()
    {
        Vector3 moveDirection = Vector3.zero;
    }
    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        isAlive = true;
        punch = arm.GetComponent<SphereCollider>();
        kick = leg.GetComponent<SphereCollider>();
        cc = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>(); 
        hungerSystem = GetComponent<HungerSystem>();
        hungerSystem.SetHealthSystem(healthSystem);
        backpack = GetComponent<Backpack>();
    }
    
    public  void Consume(Item item)
    {
        var foodData = item.Data as FoodData;
        Debug.Log(foodData);
        if (foodData != null)
        {
            healthSystem.IncreaseHealth(foodData.health);
            hungerSystem.DecreaseHungerLevel(foodData.hunger);
        }
        if (foodData == null)
        {
            healthSystem.IncreaseHealth(30);
            EnergyLevel = Mathf.Clamp(EnergyLevel + 50, 0, 100);
        }
    }
    public  void Drop(GameObject dropped)
    {
        var go = Instantiate(dropped) as GameObject;
        var spawnPoint = transform.position + (transform.forward * 10);
        spawnPoint.y += 1000;
        var ray = new Ray(spawnPoint, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
            spawnPoint.y = hit.point.y + go.transform.localScale.y * 0.5f;
        }
        go.transform.position = spawnPoint;
    }
    void DecreaseEnergy()
    {
        counter1 += 1;
        if (counter1 % 30==0)
        {
            EnergyLevel = Mathf.Clamp(EnergyLevel - 10, 0, 100);
        }
    }
    void PlayKick()
    {
        soundPlayer.PlayOneShot(Kicking);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*if (hit.gameObject.CompareTag("Food"))
        {
            var food = hit.gameObject.GetComponent<Food>();
            Destroy(hit.gameObject);
            healthSystem.IncreaseHealth(food.health); 
            hungerSystem.DecreaseHungerLevel(food.hunger);

        }*/
        if (hit.gameObject.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (backpack.AddItem(hit.gameObject))
                {
                    Destroy(hit.gameObject);
                }
            }
        }
        else if (hit.gameObject.CompareTag("Obstacle"))
        {
            var food = hit.gameObject.GetComponent<Obstacle>();
            healthSystem.DecreaseHealth(food.health);
        }
    }
    //void OnController
    /*void OnControllerCollider
    {
        if (other.gameObject.CompareTag("Food"))
        {
            collided = null;
            panel.SetActive(false);
        }
    }
    */
    void JumpUp()
    {
        moveDirection.y = jumpspeed;
        //Debug.Log(moveDirection.y);
    }
    void IncreaseEnergy()
    {
        counter += 1;
        if (counter%60==0)
            EnergyLevel = Mathf.Clamp(EnergyLevel + 10, 0, 100);
    }
    void DisableP()
    {
        punch.enabled = false;
    }
    void DisableK()
    {
        kick.enabled = false;
    }
    void Update()
    {
        if (isAlive == true)
        {
            EnergyText.text = "Energy: " + EnergyLevel.ToString();
            am.SetFloat("speed", cc.velocity.magnitude);
            if (cc.velocity.magnitude > 5)
            {
                punch.enabled = false;
                kick.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && EnergyLevel > 0)
            {
                speed *= 1.5f;
                am.SetBool("canRun", true);
                isRunning = true;
                punch.enabled = false;
                kick.enabled = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed /= 1.5f;
                am.SetBool("canRun", false);
                isRunning = false;
            }
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Invoke("JumpUp", 1);
                    am.SetBool("Jump", true);
                    punch.enabled = false;
                    kick.enabled = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                am.SetBool("Jump", false);
            }
            transform.Rotate(0, Input.GetAxis("Mouse X") * 60 * Time.deltaTime, 0);
            moveDirection.y -= 9.8f;
            cc.Move(moveDirection * speed * Time.deltaTime);
            var magnitude = new Vector2(cc.velocity.x, cc.velocity.y).magnitude;
            am.SetFloat("speed", magnitude);
            if (magnitude <= 0.1f)
            {
                am.SetBool("isPunch", false);
                am.SetBool("isKick", false);
                Invoke("IncreaseEnergy", 0);
            }
            if (isRunning)
                Invoke("DecreaseEnergy", 0);
            if (Input.GetKeyDown(KeyCode.B))
            {
                UIManager.ToggleInventory();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                punch.enabled = true;
                am.SetBool("isPunch", true);
                soundPlayer.PlayOneShot(Punching);
                EnergyLevel = Mathf.Clamp(EnergyLevel - 5, 0, 100);
                Invoke("DisableP", 0.8f);
            }
            if (Input.GetKeyDown(KeyCode.K) && EnergyLevel >= 70)
            {
                kick.enabled = true;
                am.SetBool("isKick", true);
                Invoke("PlayKick", 0.4f);
                
                EnergyLevel = Mathf.Clamp(EnergyLevel - 20, 0, 100);
                Invoke("DisableK", 0.8f);
            }
        }
        if (healthSystem.GetHealth() == 0)
        {
            am.SetBool("isDead", true);
            isAlive = false;
        }
    }
}
