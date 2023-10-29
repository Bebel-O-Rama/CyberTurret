using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHolder : MonoBehaviour
{
    private Vector3 _objectivePosition;

    private void Awake()
    {
        _objectivePosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player")
        {

        }
    }
}
