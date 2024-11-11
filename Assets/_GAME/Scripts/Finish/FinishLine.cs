using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject paintingWall;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera paintingCamera;
    [SerializeField] private GameObject uiPanel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            paintingWall.SetActive(true);

            
            mainCamera.gameObject.SetActive(false);
            paintingCamera.gameObject.SetActive(true);

            
            uiPanel.SetActive(true);
        }
    }

}
