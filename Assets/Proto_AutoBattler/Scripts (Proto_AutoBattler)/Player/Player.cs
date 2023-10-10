using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float MovementSpeed = 5;

    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsTryingToBuyTurret())
            GameManager.Instance.WantNewTurret();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        _rb.velocity = MovementDirection() * MovementSpeed;
    }
    
    private Vector3 MovementDirection()
    {
        Vector3 direction = Vector3.zero;
        direction.y += IsMovingDown() ? -1 : 0;
        direction.y += IsMovingUp() ? 1 : 0;
        direction.x += IsMovingLeft() ? -1 : 0;
        direction.x += IsMovingRight() ? 1 : 0;
        return direction.normalized;
    }
    
    private bool IsMovingUp()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }
    
    private bool IsMovingLeft()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }
    
    private bool IsMovingRight()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }
    
    private bool IsMovingDown()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    private bool IsTryingToBuyTurret()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}