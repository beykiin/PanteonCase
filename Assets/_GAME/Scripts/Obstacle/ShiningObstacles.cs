using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ShiningObstacles : MonoBehaviour
{
    [SerializeField] private Transform[] aiStartPosition;
    [SerializeField] private AudioClip restartSound;
    [SerializeField] private TextMeshProUGUI spawnCountText;


    private AudioSource _audioSource;
    private static int spawnCount = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
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
            PlayRestartSound();
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


    private void PlayRestartSound()
    {
        if (restartSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(restartSound);
        }
    }


}
