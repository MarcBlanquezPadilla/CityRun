using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowersManager : MonoBehaviour
{
    [Header("REFERENCED")]
    [SerializeField] private List<Transform> SpawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform TakeFollowers;
    [SerializeField] private GameObject FollowersLimits;

    [Header("INFORMATION")]
    [SerializeField] private int totalFollowers;
    [SerializeField] private int numActiveFollowers;
    [SerializeField] private int nextSpawnId;

    public UnityEvent<int> UpdateActiveFollowers;

    private static FollowersManager _instance;
    public static FollowersManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FollowersManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        nextSpawnId = 0;
        numActiveFollowers = 0;
        UpdateActiveFollowers.Invoke(numActiveFollowers);
        totalFollowers = TakeFollowers.childCount;

        ScoreManager.Instance.SetTotal(totalFollowers);
    }

    public void ActivateFollower(GameObject takeFollower)
    {
        TakeFollowersBehaviour tfs = takeFollower.GetComponent<TakeFollowersBehaviour>();
        for (int i = 0; i < tfs.GetFollowersToSpawn(); i++)
        {
            GameObject follower = PoolingManager.Instance.GetPooledObject("Followers");
            if (follower != null)
            {
                FollowerBehaviour fs = follower.GetComponent<FollowerBehaviour>();
                follower.transform.position = SpawnPoint[nextSpawnId].position;
                fs.SetPlayer(Player);
                fs.SetPantsColor(tfs.GetPantsColor());
                fs.SetShirtColor(tfs.GetShirtColor());
                follower.SetActive(true);
                SumActiveFollowers();
                nextSpawnId++;
                if (nextSpawnId == SpawnPoint.Count)
                {
                    nextSpawnId = 0;
                }
            }
        }
    }

    public void ActivateOneFollower()
    {
        GameObject follower = PoolingManager.Instance.GetPooledObject("Followers");
        if (follower != null)
        {
            FollowerBehaviour fs = follower.GetComponent<FollowerBehaviour>();
            follower.transform.position = SpawnPoint[nextSpawnId].position;
            fs.SetPlayer(Player);
            follower.SetActive(true);
            SumActiveFollowers();
            nextSpawnId++;
            if (nextSpawnId == SpawnPoint.Count)
            {
                nextSpawnId = 0;
            }
        }
    }

    public void DisableAllFollowers()
    {
        int ActiveFollowers = PoolingManager.Instance.GetActive("Followers");
        GameObject follower;
        for (int i = 0; i < ActiveFollowers; i++)
        {
            follower = PoolingManager.Instance.GetActiveObject("Followers");
            if (follower != null)
            {
                follower.GetComponent<HealthBehaviour>().Hurt(1);
            }
        }
    }

    public void DisableOneFollower()
    {
        GameObject follower;
        follower = PoolingManager.Instance.GetActiveObject("Followers");
        if (follower != null)
        {
            follower.GetComponent<HealthBehaviour>().SetImmortal(false);
            follower.GetComponent<HealthBehaviour>().Hurt(1);
        }
    }

    public void SumActiveFollowers()
    {
        numActiveFollowers++;
        UpdateActiveFollowers.Invoke(numActiveFollowers);
    }

    public void LessActiveFollowers()
    {
        numActiveFollowers--;
        UpdateActiveFollowers.Invoke(numActiveFollowers);
    }

    public void SetImmortals(bool b)
    {
        List<GameObject> AllFollowers = PoolingManager.Instance.GetAllObjects("Followers");
        for (int i = 0; i < AllFollowers.Count; i++)
        {
            if (AllFollowers[i] != null)
            {
                AllFollowers[i].GetComponent<HealthBehaviour>().SetImmortal(b);
            }
        }
    }

    public int GetActiveFollowers()
    {
        return numActiveFollowers;
    }
}
