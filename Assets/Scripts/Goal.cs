using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject nextStart;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!nextStart)
            {
                return;
            }
            GolfBall ball = other.gameObject.GetComponent<GolfBall>();
            
            Vector3 nextStartPos = nextStart.transform.position;
            nextStartPos.y += 0.2f;
            ball.EndHole();
            ball.transform.position = nextStartPos;
        }
    }
}
