using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject nextStart;

    public event Action FinishGame;
    private const float yOffset = 0.1f;
    private const float endTime = 2.5f;
    private bool ballHasEntered = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !ballHasEntered)
        {
            ballHasEntered = true;


            GolfBall ball = other.gameObject.GetComponent<GolfBall>();
            StartCoroutine("EndLevel", ball);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ballHasEntered)
        {
            ballHasEntered = false;
            StopCoroutine("EndLevel");
        }
    }

    private IEnumerator EndLevel(GolfBall ball)
    {
        yield return new WaitForSeconds(endTime);

        if (!nextStart)
        {
            FinishGame.Invoke();
        }
        else
        {
            Vector3 nextStartPos = nextStart.transform.position;
            nextStartPos.y += yOffset;
            ball.Stop();
            ball.transform.position = nextStartPos;
        }
    }
}
