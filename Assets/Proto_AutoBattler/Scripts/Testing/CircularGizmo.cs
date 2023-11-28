using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircularGizmo : MonoBehaviour
{
    [Min(0f)] [SerializeField] public float radius;
    [Min(0f)] [SerializeField] public float thickness;
    [SerializeField] public Color circleColor;

    private void OnDrawGizmos()
    {
        Handles.color = circleColor;
        // DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness = 0.0f);
        Handles.DrawWireArc(transform.position, -Vector3.forward, Vector3.up, 360, radius, thickness);
    }
}
