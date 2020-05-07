using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTemp : MonoBehaviour
{
    public delegate void PlayerDeath(int Score);
    public static event PlayerDeath onPlayerDeath;
    public Text myname;
    char[] letter = new char[] {'A','R','Y','A','N' };
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillAfterFew", 3);
        StartCoroutine("PrintName");
    }
    IEnumerator PrintName()
    {
        for(int i = 0; i < letter.Length; i++)
        {
            myname.text += letter[i];
            yield return new WaitForSeconds(1);
        }
    }
    void KillAfterFew()
    {
        onPlayerDeath(100);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
