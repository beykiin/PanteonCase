using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FallingShiningObstacles : MonoBehaviour
{

    [SerializeField] private Transform startPointPosition;
    [SerializeField] private Transform endPointPosition;
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float resetDelay = 2f;

    private void Start()
    {

        transform.rotation = Quaternion.Euler(90, 90, 0);
        Rolling();    
    }

    private void Rolling()
    {
        RollDown();
    }

    private void RollDown()
    {
        transform.DOMove(endPointPosition.position, moveTime)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                StartCoroutine(ResetObstacle());
            });

        transform.DORotate(new Vector3(0f, rotationSpeed, 0f), moveTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    } 

    private IEnumerator ResetObstacle()
    {
        yield return new WaitForSeconds(resetDelay);


        transform.position = startPointPosition.position;
        Rolling();
    }
}
