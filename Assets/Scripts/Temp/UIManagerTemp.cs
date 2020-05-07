using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTemp : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Running";
        PlayerTemp.onPlayerDeath += onDeath;
    }
    void onDeath(int score)
    {
        text.text = "Player died " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
