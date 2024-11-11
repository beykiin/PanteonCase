using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject paintingWall;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera paintingCamera;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private CharacterMovement playerCharacter;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("Player"))
        {

            PlayFinishSound();
            playerCharacter.StopMovement();
            paintingWall.SetActive(true);

            
            mainCamera.gameObject.SetActive(false);
            paintingCamera.gameObject.SetActive(true);

            gameCanvas.SetActive(false);
            uiPanel.SetActive(true);
        }
        else if (other.CompareTag("AI"))
        {
            other.GetComponent<AICharaters>()?.StopAI(true);
        }
    }

    private void PlayFinishSound()
    {
        if (finishSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(finishSound);
        }
    }

}
