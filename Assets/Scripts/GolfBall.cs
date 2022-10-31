using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class GolfBall : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float stoppingMargin;
    public float heightLimit;

    [Header("Strength")]
    public float power;
    public float maxStrength;
    public float strengthCoefficient = 1;

    private Rigidbody body;
    private AudioSource audioSource;
    public event Action AddShot;
    private Vector3 lineEndPos = Vector3.zero;
    private bool canPutt = true;
    private Vector3 lastPos;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (body.velocity.magnitude < stoppingMargin)
        {
            Stop();
        }
        else
        {
            if (transform.position.y < heightLimit)
            {
                transform.position = lastPos;
                Stop();
            }

            lineRenderer.enabled = false;
            canPutt = false;
        }
    }

    private void OnMouseDrag()
    {
        if (!canPutt)
            return;

        ConvertMousePos();
        DrawLine();
    }

    private void OnMouseUp()
    {
        if (!canPutt)
            return;

        Putt();
    }

    public void Stop()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        canPutt = true;
    }

    private void Putt()
    {
        audioSource.Play();

        body.AddForce(lineEndPos * power, ForceMode.Impulse);

        AddShot.Invoke();
        canPutt = false;
        lastPos = transform.position;
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

    private void ConvertMousePos()
    {
        Vector3 newMousePos = Input.mousePosition;
        Vector3 mousePosDiff = newMousePos - Camera.main.WorldToScreenPoint(transform.position);

        // Reduit le scale du vecteur pour pas qu'il soit completement enorme
        mousePosDiff.y /= Camera.main.pixelHeight;
        mousePosDiff.x /= Camera.main.pixelHeight;

        lineEndPos = new Vector3(mousePosDiff.x, 0, mousePosDiff.y);
        float magnitude = lineEndPos.magnitude * strengthCoefficient;
        lineEndPos = lineEndPos.normalized * Mathf.Clamp(magnitude, 0, maxStrength);
    }
}
