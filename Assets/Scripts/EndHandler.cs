using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndHandler : MonoBehaviour
{
    public Goal goal;

    void Start()
    {
        gameObject.SetActive(false);

        goal.FinishGame += EndGame;
    }

    private void EndGame() {
        gameObject.SetActive(true);
    }
}
