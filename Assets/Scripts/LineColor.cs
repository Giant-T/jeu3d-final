using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    public float maxLength;
    public Color minColor;
    public Color maxColor;

    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        float ratio = CalculateLineLengthRatio();
        ChangeLineColor(ratio);
    }

    private float CalculateLineLengthRatio()
    {
        float length = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));

        return length / maxLength;
    }

    private void ChangeLineColor(float ratio)
    {
        Color color = Color.Lerp(minColor, maxColor, ratio);
        lineRenderer.endColor = color;
        lineRenderer.startColor = color;
    }
}
