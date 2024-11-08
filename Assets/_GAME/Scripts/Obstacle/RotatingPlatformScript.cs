using UnityEngine;
using DG.Tweening;

public class RotatingPlatformScript : MonoBehaviour
{
    [SerializeField] private float _rotateTime = 3f;
    [SerializeField] private float _rotateDegree = 360f;


    private void Start()
    {
        Rotating();
    }

    private void Rotating()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.DORotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + _rotateDegree), _rotateTime, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
