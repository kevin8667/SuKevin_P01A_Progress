using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [SerializeField] Vector3 _positionToMoveTo;
    [SerializeField] Vector3 _positionToMoveBack;
    [SerializeField] Vector3 _switchPosition;
    [SerializeField] GameObject _orbitCenter;
    [SerializeField] float _anglePerUpdate;
    bool _isOrbiting = false;

    EnemyHealth _heath;

    private void Awake()
    {

        _heath = gameObject.GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        StartCoroutine(LerpPositionTo(_positionToMoveTo, 5));
    }

    private void Update()
    {
        if (_heath.CurrentHealth > _heath.MaxHealth * 0.5f) 
        {
            if (transform.position == _positionToMoveTo)
            {
                if (_heath.CurrentHealth <= _heath.MaxHealth * 0.5f)
                {
                    StartCoroutine(LerpPositionTo(_switchPosition, 5));
                }
                StartCoroutine(LerpPositionBack(_positionToMoveBack, 5));
                
            }
            else if (transform.position == _positionToMoveBack)
            {
                if (_heath.CurrentHealth <= _heath.MaxHealth * 0.5f)
                {
                    StartCoroutine(LerpPositionTo(_switchPosition, 5));
                }
                StartCoroutine(LerpPositionTo(_positionToMoveTo, 5));
                
            }
        }else if(_heath.CurrentHealth <= _heath.MaxHealth * 0.5f) 
        {
           if(_isOrbiting == false) 
            {
                StartCoroutine(LerpPositionTo(_switchPosition, 5));
                _isOrbiting = true;
            }

            Orbiting(_orbitCenter, _anglePerUpdate);
        }
    }



    private IEnumerator LerpPositionTo(Vector3 targetPosition, float duration)
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

    private IEnumerator LerpPositionBack(Vector3 targetPosition, float duration)
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

    private void Orbiting(GameObject orbitCenter, float angle)
    {
        transform.RotateAround(orbitCenter.transform.position, new Vector3(0, 1, 0), angle);
    }
}
