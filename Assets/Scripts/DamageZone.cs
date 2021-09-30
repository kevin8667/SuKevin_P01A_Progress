using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _finalPosition;

    bool _isDropping = false;
    bool _isDropped = false;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDropping == false && _isDropped == false)
        {
            StartCoroutine(Move(_player));
        }
        else if (_isDropping == true && _isDropped == false)
        {
            StartCoroutine(Dropping(0.3f));
        }
        if (_isDropped == true) 
        {
            Destroy(gameObject, 3.5f);
        }
        
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamageable _damage = other.gameObject.GetComponent<IDamageable>();
            if (_damage != null)
            {
                _damage.TakeDamage(1);
            }
            Destroy(gameObject);

        }
    }

    //follows the player
    private IEnumerator Move(GameObject player)
    {
        
        transform.position = player.transform.position + new Vector3(0, 5, 0);
        yield return new WaitForSeconds(0.7f);
        _isDropping = true;
        _finalPosition = transform.position;

    }

    //drops the damagezone
    private IEnumerator Dropping(float duration)
    {
        float time = 0;
        Vector3 startPosition = _finalPosition;
        Vector3 targetPosition = startPosition - new Vector3 (0,2.5f,0);

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        _isDropping = false;
        _isDropped = true;

    }

}
