using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public static float DayCounter;
    public float DayTimeLength;
    public float NightTimeLength;
    public bool IsDayTime;
    public bool IsNightTime;
    private bool EndOfday;
    private bool Nextday;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        DayCounter = 1;
    }
    void Update()
    {
        if (transform.eulerAngles.x > 0 && transform.eulerAngles.x < 180)
        {
            IsNightTime = false;
            IsDayTime = true;
        }
        else if (transform.eulerAngles.x > 180 && transform.eulerAngles.x < 360)
        {
            IsNightTime = true;
            IsDayTime = false;
        }
        if (IsDayTime)
            transform.Rotate(1 * DayTimeLength * Time.deltaTime, 0, 0);
        if (IsNightTime)
            transform.Rotate(1 * NightTimeLength * Time.deltaTime, 0, 0);
        if(Player.isAlive==true)
           IncreaseNumberOfDays();
    }
    void IncreaseNumberOfDays()
    {
        if (transform.eulerAngles.x > 270 && transform.eulerAngles.x < 280)
        {
            EndOfday = true;
        }
        else
        {
            Nextday = false;
            EndOfday = false;
        }
        if (EndOfday && !Nextday)
        {
            Nextday = true;
            DayCounter += 1;
        }
    }
}
