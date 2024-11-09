using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class MoveCoin : MonoBehaviour, IInteractable
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private float moveTime = 1f;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        CoinMove();
    }

    

    private void CoinMove() { 
        transform.DOMoveY(transform.position.y + distance, moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void Interact()
    {
        transform.DOScale(0, 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
