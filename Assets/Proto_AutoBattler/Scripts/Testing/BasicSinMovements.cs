using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Serialization.ObjectGraphVisitors;
using UnityEditor.TerrainTools;
using UnityEngine;

public class BasicSinMovements : MonoBehaviour
{
    [Min(0)] [SerializeField] private Vector3 mvtRange;
    [Min(0)] [SerializeField] private float mvtSpeed;
    
    private Vector3 centerPosition;
    private Rigidbody2D rb;
    
    private Vector3 xyRange;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("You are trying to move without a Rigidbody2D the object : " + transform.name);
        }

        centerPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = centerPosition + Mathf.Sin(mvtSpeed * Time.time) * mvtRange;
        rb.MovePosition(centerPosition + newPosition * Time.deltaTime);
    }
}
