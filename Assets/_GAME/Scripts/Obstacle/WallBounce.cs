using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallBounce : MonoBehaviour
{
    [SerializeField] private float _bounce = 1.1f;
    [SerializeField] private float duration = 1.1f;

    private Tween _tween;
    public void BounceEffect()
    {
        Vector3 normalScale = transform.localScale;
        _tween.Kill(true);
        _tween = transform.DOPunchScale(Vector3.one, duration);

    }

}
