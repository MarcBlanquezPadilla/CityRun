using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [Header("REFERENCED")]
    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject restartLevel;

    public void OnEnable()
    {
        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);
        nextLevel.SetActive(false);
        restartLevel.SetActive(true);
    }

    public void Win()
    {
        nextLevel.SetActive(true);
        restartLevel.SetActive(false);
        int stars = ScoreManager.Instance.GetStars();
        if (stars == 1)
        {
            firstStar.SetActive(true);
        }
        else if (stars == 2)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        else if (stars == 3)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
    }
}
