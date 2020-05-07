using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text hunger;
    public Text DaysCount;
    public HealthSystem HealthSystem;
    public HungerSystem HungerSystem;
    public InventoryUI InventoryUI;
    void Start()
    {
    }
    void Update()
    {
        DaysCount.text = DayNight.DayCounter.ToString();
        healthText.text = "Health: " + HealthSystem.GetHealth().ToString();
        hunger.text = "Hunger: " + HungerSystem.GetHungerLevel().ToString();
        if (DayNight.DayCounter == 8)
        {
            Invoke("End",3);
        }
        if (Player.isAlive == false && DayNight.DayCounter < 8)
        {
            Invoke("End", 4);
        }
    }
    void End()
    {
        SceneManager.LoadScene("EndGame");
    }
    public void ToggleInventory()
    {
        InventoryUI.Toggle();
    }
}
