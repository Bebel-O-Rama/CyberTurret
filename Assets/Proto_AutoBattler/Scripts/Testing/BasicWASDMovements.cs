using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWASDMovements : MonoBehaviour
{
    [SerializeField][Min(0f)] public float movementSpeed = 5;
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        _rb.velocity = direction.normalized * movementSpeed;
    }
}
