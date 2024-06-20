using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("INFORMATION")]
    [SerializeField] private int stars;
    [SerializeField] private int total;

    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        stars = 0;
    }


    public void UpdateStars(int s)
    {
        if (s == total)
        {
            stars = 3;
        }
        else if (s >= total * 0.75f)
        {
            stars = 2;
        }
        else if (s >= total * 0.5f)
        {
            stars = 1;
        }
        else
        {
            stars = 0;
        }
    }

    public int GetStars()
    {
        return stars;
    }

    public void SetTotal(int t)
    {
        total = t;
    }
}
