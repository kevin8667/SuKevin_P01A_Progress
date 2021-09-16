using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [SerializeField] Vector3 _positionToMoveTo;
    [SerializeField] Vector3 _positionToMoveBack;


    void Start()
    {
        StartCoroutine(LerpPositionTo(_positionToMoveTo, 5));
    }

    private void Update()
    {
        if (transform.position == _positionToMoveTo)
        {
            StartCoroutine(LerpPositionBack(_positionToMoveBack, 5));

        }
        else if (transform.position == _positionToMoveBack)
        {
            StartCoroutine(LerpPositionTo(_positionToMoveTo, 5));
        }

        
    }



    IEnumerator LerpPositionTo(Vector3 targetPosition, float duration)
    {

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

    }

    IEnumerator LerpPositionBack(Vector3 targetPosition, float duration)
    {

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

    }
}
