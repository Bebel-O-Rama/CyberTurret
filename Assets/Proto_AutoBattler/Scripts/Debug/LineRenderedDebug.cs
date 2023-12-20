using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEditor.AssetImporters;
using UnityEngine;

public class LineRenderedDebug : MonoBehaviour
{
    [SerializeField] public AnimationCurve alphaCurve;
    [SerializeField] public Material attackMaterial;
    [SerializeField] public Material telegraphMaterial;
    
    private LineRenderer lr;
    private float lifespan;
    private LineType type;
    private Material material;
    private float timeSinceStart;
    private bool isFading;
    
    public enum LineType
    {
        Attack,
        Telegraph
    }

    public void StartLine(Vector3 startPos, Vector3 endPos, LineType t, float span = 0.5f, bool fade = true)
    {
        SetLineParameters(t, span, fade);

        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }

    public void StartCircle(LineType t, float span = 0.5f, bool fade = true)
    {
        SetLineParameters(t, span, fade);

    }
    
    public void StartCone(LineType t, float span = 0.5f, bool fade = true)
    {
        SetLineParameters(t, span, fade);

    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;
        
        var colorTemp = lr.material.color;
        colorTemp.a = Mathf.Lerp(0f, 1f, alphaCurve.Evaluate(timeSinceStart/lifespan));
        lr.material.color = colorTemp;
        
        if (timeSinceStart >= lifespan)       
        {
            Destroy(gameObject);
        }
    }

    private void SetLineParameters(LineType t, float span, bool fade)
    {
        lr = GetComponent<LineRenderer>();

        isFading = fade;
        if (span <= 0)
        {
            Debug.LogWarning("The lifespan of the debug line was <= 0");
            Destroy(gameObject);
        }
        lifespan = span;
        timeSinceStart = 0f;
        
        switch (t)
        {
            case LineType.Attack:
            {
                material = attackMaterial;
                break;
            }
            case LineType.Telegraph:
            {
                material = telegraphMaterial;
                break;
            }
        }

        if (material == null)
        {
            Debug.LogWarning("Something went wrong with one of the debug line, it's missing it's material.");
            Destroy(gameObject);
        }
    }
}
