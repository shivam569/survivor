using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    public GameObject victory;
    public GameObject loss;
    // Start is called before the first frame update
    void Start()
    {
        if (DayNight.DayCounter >7)
        {
            victory.SetActive(true);
        }
        else if (DayNight.DayCounter < 8)
        {
            loss.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
