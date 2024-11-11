using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Transform> aiTransforms;

    private int rank;


    private void Update()
    {
        UpdateRank();
    }

    private void UpdateRank()
    {
        int rankCounter = 10;

        foreach (var ai in aiTransforms)
        {
            if(playerTransform.position.z > ai.position.z)
            {
                rankCounter--;
            }
        }
        rank = rankCounter;
        rankText.text = GetRankString(rank);

    }

    string GetRankString(int rank)
    {
        switch (rank)
        {
            case 1: return "1st";
            case 2: return "2nd";
            case 3: return "3rd";
            default: return rank + "th";
        }
    }


}
