using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShiningObstacles : MonoBehaviour
{
    [SerializeField] private Transform[] aiStartPosition;

    [SerializeField] private TextMeshProUGUI spawnCountText;
    private static int spawnCount = 0;


    private void Start()
    {
        UpdateSpawnCountUI();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AI"))
        {
            other.GetComponent<AICharaters>().ResetAI();
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterMovement>().ResetPlayer();

            spawnCount++;

            UpdateSpawnCountUI();
        }



    }

    private void UpdateSpawnCountUI()
    {
        if (spawnCountText != null)
        {
            spawnCountText.text = spawnCount.ToString();
        }
    }





}
