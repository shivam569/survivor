using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTemp : MonoBehaviour
{
    private void Awake()
    {
        PlayerTemp.onPlayerDeath += onPlayerDeathN;
    }
    void Start()
    {
        InvokeRepeating("Change", 0, 1);
    }
    void Change()
    {
        GetComponent<ColourChangeTemp>().UpdateColour();
    }
    void onPlayerDeathN(int score)
    {
        Debug.Log("NPC: Player died");
        CancelInvoke("Change");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
