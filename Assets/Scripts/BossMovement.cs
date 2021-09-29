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

    [SerializeField] private GameObject _enrageParticles;
    [SerializeField] private AudioClip _enrageSound;

    [SerializeField] CamManager _camManager;
    [SerializeField] TimeManager _timeManager;

    [SerializeField] Health _playerHealth;

    private GameObject _NewEnrageParticles;

    EnemyHealth _heath;

    public bool _isEnraged = false;
    public bool _isOrbiting = false;


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
            if (_isEnraged == false)
            {
                StartCoroutine(LerpPositionTo(_switchPosition, 5));
                StartCoroutine(EnrageSequence());
                _isEnraged = true;



            } else if(_isEnraged == true && _isOrbiting == true)
            {
                Orbiting(_orbitCenter, _anglePerUpdate);
            }      
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

    private IEnumerator EnrageSequence() 
    {
        

        yield return new WaitForSeconds(5f);

        _camManager.SwitchCamera();

        _playerHealth._isInvincible = true;

        yield return new WaitForSeconds(1f);

        _timeManager.SlowDown();

        _NewEnrageParticles = Instantiate(_enrageParticles, transform.position - new Vector3 (0, 0.5f, 0), Quaternion.LookRotation(transform.up)) as GameObject;
        
        if (_NewEnrageParticles)
        {
            Destroy(_NewEnrageParticles, 1f);
        }
        if (_enrageSound != null)
        {
            AudioHelper.PlayClip2D(_enrageSound, 1f);
        }

        yield return new WaitForSeconds(1f);

        _camManager.SwitchCamera();

        yield return new WaitForSeconds(1f);
        _isOrbiting = true;

        _playerHealth._isInvincible = false;
    }
}
