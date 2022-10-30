using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePolluter : MonoBehaviour
{
    public int numberOfObjects = 10;

    [Header("Model info")]
    public Mesh model;
    public Material[] materials;

    [Header("Generation Limits")]
    public float yOffset = 3f;
    public Vector3 lowerLimit;
    public Vector3 upperLimit;

    void Start()
    {
        InitializePollution();
    }

    private GameObject GenerateBaseObject(int index)
    {
        GameObject gameObject = new GameObject($"Donut{index}");

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = model;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = materials;
        
        return gameObject;
    }

    private void InitializePollution()
    {
        GameObject[] gameObjects = new GameObject[numberOfObjects];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = GenerateBaseObject(i);
            RandomizePollution(gameObjects[i]);
            gameObjects[i].transform.parent = transform;
        }
    }

    private void RandomizePollution(GameObject gameObject)
    {
        float x = Random.Range(lowerLimit.x, upperLimit.x);
        float z = Random.Range(lowerLimit.z, upperLimit.z);
        gameObject.transform.position = new Vector3(x, -yOffset, z);
        gameObject.transform.rotation = Random.rotation;
    }
}
