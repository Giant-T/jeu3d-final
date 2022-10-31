using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class Counter: MonoBehaviour
{
    protected TextMeshProUGUI textMeshPro;
    protected int count = 0;

    protected abstract void AddCounter();
    protected void RefreshCounter()
    {
        textMeshPro.text = count.ToString();
    }
}