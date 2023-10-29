using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Vars
    [SerializeField][Min(0f)] public float MovementSpeed = 5;

    // Testing variable (DON'T EDIT MANUALLY)
    [SerializeField] private Objective obj;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        _rb.velocity = direction.normalized * MovementSpeed;
    }
    
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}