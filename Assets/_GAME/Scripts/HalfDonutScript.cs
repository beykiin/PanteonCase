using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HalfDonutScript : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 5f;
    [SerializeField] private float _moveTime = 2f;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        transform.DOMoveX(transform.position.x + _moveDistance, _moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
