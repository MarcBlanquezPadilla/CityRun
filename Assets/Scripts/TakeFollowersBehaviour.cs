using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFollowersBehaviour : MonoBehaviour
{
    [Header("PROPERTIES")]
    [SerializeField] private int followersToSpawn;

    [Header("REFERENCED")]
    [SerializeField] private GameObject skinnedBody;

    [Header("INFORMATION")]
    [SerializeField] private Material pantsMat;
    [SerializeField] private Material shirtMat;

    private void Awake()
    {
        pantsMat = skinnedBody.GetComponent<SkinnedMeshRenderer>().materials[0];
        shirtMat = skinnedBody.GetComponent<SkinnedMeshRenderer>().materials[4];

        SetRandomColor(pantsMat);
        SetRandomColor(shirtMat);
    }

    private void SetRandomColor(Material m)
    {
        m.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public Color GetPantsColor()
    {
        return pantsMat.color;
    }

    public Color GetShirtColor()
    {
        return shirtMat.color;
    }
    
    public int GetFollowersToSpawn()
    {
        return followersToSpawn;
    }
}
