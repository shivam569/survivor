using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttck : MonoBehaviour
{
    public bool isLeg;
    HealthSystem go;
    int zLeg;
    int zPun;
    int pPun;
    int pLeg;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Parasite"))
        {
            go = other.gameObject.GetComponent<HealthSystem>();
            if (!isLeg)
            {
                zLeg = 0;
                zPun = 0;
                pPun ++;
                pLeg =0;
                DecHealth(pPun, 8);
            }
            else
            {
                zLeg = 0;
                zPun = 0;
                pPun = 0;
                pLeg ++;
                DecHealth(pLeg, 12);
            }
        }
        else if (other.CompareTag("Zombie"))
        {
            go = other.gameObject.GetComponent<HealthSystem>();
            if (!isLeg)
            {
                zLeg = 0;
                zPun ++;
                pPun = 0;
                pLeg = 0;
                DecHealth(zPun, 5);
            }
            else
            {
                zLeg ++;
                zPun = 0;
                pPun = 0;
                pLeg = 0;
                DecHealth(zLeg, 10);
            }
        }
    }
    void DecHealth(int counter, int factor)
    {
        if (counter % 6 == 0)
        {
            go.DecreaseHealth(factor);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        zLeg=0;
        zPun=0;
        pPun=0;
        pLeg=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
