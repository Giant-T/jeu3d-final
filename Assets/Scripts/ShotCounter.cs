using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShotCounter : MonoBehaviour
{
    public GolfBall golfBall;
        
    private TextMeshProUGUI textMesh;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        RefreshCounter();

        golfBall.AddShot += AddCounter;
    }

    private void OnDestroy()
    {
        golfBall.AddShot -= AddCounter;
    }

    private void AddCounter()
    {
        count++;
        RefreshCounter();
    }

    private void RefreshCounter()
    {
        textMesh.text = count.ToString();
    }
}
