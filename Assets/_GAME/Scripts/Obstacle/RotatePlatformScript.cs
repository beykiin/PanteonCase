using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RotatePlatformScript : MonoBehaviour
{
    [SerializeField] private float _rotateTime = 5f;
    [SerializeField] private float _rotateDegree = 360f;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {

        Vector3 currentRotation = transform.rotation.eulerAngles;

        transform.DORotate(new Vector3(currentRotation.x, currentRotation.y + _rotateDegree, currentRotation.z), _rotateTime, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
