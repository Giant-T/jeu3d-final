using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class GolfBall : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float power;
    public float stoppingMargin;
    public float heightLimit;
    private Rigidbody body;
    public event Action AddShot;
    private Vector3 lineEndPos = Vector3.zero;
    private bool canPutt = true;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
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
        if (canPutt)
        {
            GetMousePosition();

            DrawLine();
        }
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

    private void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("GolfLine")))
        {
            lineEndPos = new Vector3(transform.position.x - hit.point.x, 0, transform.position.z - hit.point.z);
        }
    }
}
