using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public  void OnPlay()
    {
        SceneManager.LoadScene("Terrain");
    }
    public void onHome()
    {
        SceneManager.LoadScene("StartGame");
    }
    public void onRestart()
    {
        SceneManager.LoadScene("Terrain");
        DayNight.DayCounter = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
