using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float yOffset;
    public GameObject followed;

    void Start()
    {
        SetPosition();
    }

    private void LateUpdate()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector3 position = followed.transform.position;

        position.y += yOffset;

        transform.position = position;
    }
}
