using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("For testing purposes (shouldn't be edited directly)")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material mat;
    [SerializeField] private Vector3 objectivePosition;
    [SerializeField] private bool isReady;
    [SerializeField] private int scrapValue;
    [SerializeField] private int currentHP;
    [SerializeField] private float baseSpeed;
    
    public void InitializeEnemy(SingleEnemyData data)
    {
        rb = GetComponent<Rigidbody2D>();
        mat = GetComponent<SpriteRenderer>().material;
        mat.color = data.baseColor;
        currentHP = data.baseHP;
        scrapValue = data.scrapValue;
        baseSpeed = data.baseSpeed;
        UpdateObjectivePosition();

        // We currently set the orientation only once, but we could play with that at some point...
        transform.Rotate(Vector3.forward * GetDegRotation());
        
        isReady = true;
    }


    public void OnHit()
    {
        currentHP -= 1;
        if (currentHP <= 0)
        {
            GameManager.Instance.OnEnemyKilled(gameObject, scrapValue);
            Destroy(gameObject);
        }

        mat.color /= 2;
    }
    
    private float GetDegRotation()
    {
        var delta = UnityEngine.Vector3.Normalize(objectivePosition - transform.position);
        return -(Mathf.Rad2Deg * Mathf.Atan2(delta.x, delta.y));
    }

    private void UpdateObjectivePosition()
    {
        objectivePosition = GameManager.Instance.GetCurrentObjectivePosition();
    }

    private void FixedUpdate()
    {
        if (isReady)
        {
            rb.velocity = transform.up * baseSpeed;
        }
    }
}
