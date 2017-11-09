using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLiner : MonoBehaviour
{
    private LineRenderer lineRenderer;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Initialize(Vector3 posit1, Vector3 posit2, float lifeTime = 0.5f, float lineWidth = 0.05f)
    {

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, posit1);
            lineRenderer.SetPosition(1, posit2);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }

        Invoke("LineOff", lifeTime);
    }
    public void SetLineColor(Color color)
    {
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }



    private void LineOff()
    {
        gameObject.SetActive(false);
    }
}
