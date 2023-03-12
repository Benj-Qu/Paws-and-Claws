using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

public class HasRadius : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    private void OnEnable()
    {
        float scale_x = transform.localScale.x;
        float scale_y = transform.localScale.y;
        float scale_z = transform.localScale.z;
        scale_x = radius * 2;
        scale_y = radius * 2;
        transform.localScale = new Vector3(scale_x, scale_y, scale_z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRadius(float input)
    {
        radius = input;
    }

    public float GetRadius()
    {
        return radius;
    }
}
