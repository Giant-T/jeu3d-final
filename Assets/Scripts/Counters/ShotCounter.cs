using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShotCounter : Counter
{
    public GolfBall golfBall;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        golfBall.AddShot += AddCounter;
        RefreshCounter();
    }

    private void OnDestroy()
    {
        golfBall.AddShot -= AddCounter;
    }

    protected override void AddCounter()
    {
        count++;
        RefreshCounter();
    }
}
