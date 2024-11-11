using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinManager : MonoBehaviour
{

    [SerializeField] private RectTransform uiTarget;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform[] coinUIObjects;
    [SerializeField] private float moveTime = 1f;
    [SerializeField] private float delayBetweenCoins = 0.5f;
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private AudioClip collectCoinSound;

    private static int coinCount = 0;
    private AudioSource _audioSource;


    private void Awake()
    {
        
        foreach (var coin in coinUIObjects)
        {
            coin.gameObject.SetActive(false);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    
    public void OnCoinCollected(Vector3 coinPosition)
    {
        for (int i = 0; i < coinUIObjects.Length; i++)
        {
            
            coinUIObjects[i].gameObject.SetActive(true);

            coinUIObjects[i].anchoredPosition = Vector2.zero;



            Vector2 screenPos = Camera.main.WorldToScreenPoint(coinPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPoint);

            
            coinUIObjects[i].DOAnchorPos(uiTarget.anchoredPosition, moveTime)
            .SetDelay(i * delayBetweenCoins)
            .OnComplete(() =>
            {
                PlayCollectCoinSound();
                coinCount++;
                coinCountText.text = coinCount.ToString();
                

            });
            
        }

        
    }

    public void PlayCollectCoinSound() {
        if (collectCoinSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(collectCoinSound);
        }
    }
}
