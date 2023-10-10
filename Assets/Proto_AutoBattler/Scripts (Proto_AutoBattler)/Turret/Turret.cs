using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Dependencies
    [SerializeField] public Transform output;
    [SerializeField] public SpriteRenderer rangeTestingRenderer;

    // Editing variables
    [SerializeField] public float range;
    [SerializeField] public bool canShootThroughWalls;
    [SerializeField] public float delayBetweenShot;

    // Testing stuff
    [SerializeField] public bool hideRange = false;

    private bool isReadyToShoot = true;
    private Vector3 turretPosition;
    private GameObject currentTarget = null;
    private Vector3 currentTargetPosition = Vector3.zero;
    private float currentTargetDistance;

    private void Awake()
    {
        if (range < 2.5)
        {
            range = 2.5f;
        }

        if (!hideRange)
        {
            rangeTestingRenderer.transform.localScale = Vector3.one * range * 2;
            rangeTestingRenderer.enabled = true;
        }

        turretPosition = transform.position;
    }

    private void Update()
    {
        if (isReadyToShoot)
        {
            FindNewTarget();
            if (currentTarget != null)
            {
                ShootTarget();
            }
        }
    }

    private void FindNewTarget()
    {
        currentTarget = null;
        currentTargetDistance = float.PositiveInfinity;
        List<GameObject> enemyList = GameManager.Instance.GetEnemyList();
        foreach (var enemy in enemyList)
        {
            var enemyDistance = Vector3.Distance(turretPosition, enemy.transform.position);
            if (enemyDistance <= range)
            {
                if (currentTarget == null || enemyDistance < range)
                {
                    // The wall shooting stuff doesn't work for now 
                    if (canShootThroughWalls || !Physics.Linecast(turretPosition, enemy.transform.position))
                    {
                        currentTarget = enemy;
                        currentTargetDistance = enemyDistance;
                        currentTargetPosition = enemy.transform.position;
                    }
                }
            }
        }
    }

    private void ShootTarget()
    {
        // transform.LookAt(currentTarget.transform);
        transform.up = currentTarget.transform.position - turretPosition;
        // Shoot visual stuff

        // currentTarget.GetComponent<Enemy>().OnHit();

        isReadyToShoot = false;
        Invoke("FinishCooldown", delayBetweenShot);
    }

    private void FinishCooldown()
    {
        isReadyToShoot = true;
    }
}
