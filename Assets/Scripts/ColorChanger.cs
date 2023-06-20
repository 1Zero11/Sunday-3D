using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ColorChanger : MonoBehaviour
{
    public Material paint;

    private void Awake()
    {
        DataHoarder.color = paint.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        paint.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        DataHoarder.color = paint.color;
    }
}
