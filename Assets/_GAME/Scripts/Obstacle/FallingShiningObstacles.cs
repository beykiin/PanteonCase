using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FallingShiningObstacles : MonoBehaviour
{

    [SerializeField] private Transform startPointPosition;
    [SerializeField] private Transform endPointPosition;
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private float rotateDegree = 360f;
    [SerializeField] private float resetDelay = 2f;

    private Vector3 _baseRotation = new Vector3(0, 0, 90);

    private void Start()
    {
        RollDown();    
    }

    private void RollDown()
    {
        transform.rotation = Quaternion.Euler(_baseRotation);
        transform.DOMove(endPointPosition.position, moveTime)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                StartCoroutine(ResetObstacle());
            });

        transform.DORotate(new Vector3(-rotateDegree, 0f, 0f), moveTime, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear);
    } 

    private IEnumerator ResetObstacle()
    {
        yield return new WaitForSeconds(resetDelay);


        transform.position = startPointPosition.position;
        RollDown();
    }
}
