using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChangeTemp : MonoBehaviour
{
    private Color[] colours = new Color[] { Color.blue,Color.red,Color.magenta, Color.cyan};
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    public void UpdateColour()
    {
        mesh.material.color = colours[Random.Range(0, 4)];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
