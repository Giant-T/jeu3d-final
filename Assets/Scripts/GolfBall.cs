using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private Vector3 lineEndPos = Vector3.zero;

    private void OnMouseDrag()
    {
        GetMousePosition();

        DrawLine();
    }

    private void DrawLine()
    {
        Vector3[] positions =
        {
            Vector3.zero,
            lineEndPos
        };

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("GolfLine")))
        {
            lineEndPos = new Vector3(-hit.point.x, 0, -hit.point.z);
        }
    }


}
