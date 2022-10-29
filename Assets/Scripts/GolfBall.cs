using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class GolfBall : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float power;
    public float maxStrength;
    public float strengthCoefficient = 1.0f;
    public float stoppingMargin;
    public float heightLimit;

    private Rigidbody body;
    private AudioSource audioSource;
    public event Action AddShot;
    private Vector3 lineEndPos = Vector3.zero;
    private bool canPutt = true;

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

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
        if (canPutt)
        {
            Putt();

            lineRenderer.enabled = false;
        }
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
        mousePosDiff *= strengthCoefficient;

        lineEndPos = new Vector3(mousePosDiff.x, 0, mousePosDiff.y);
        lineEndPos = lineEndPos.normalized * Mathf.Clamp(lineEndPos.magnitude, 0, maxStrength);
    }
}
