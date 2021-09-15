using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody _rb;
    [SerializeField] protected float _travelSpeed = .25f;
    [SerializeField] protected GameObject _impactParticles;
    [SerializeField] protected AudioClip _impactSound;
    protected GameObject _NewImpactParticles;

    protected float TravelSpeed 
    {
        get { return _travelSpeed; }
        set { _travelSpeed = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Boss")
        {
            if (_impactSound != null)
            {
                AudioHelper.PlayClip2D(_impactSound, 1f);
            }
            _NewImpactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity) as GameObject;
            if (_NewImpactParticles)
            {
                Destroy(_NewImpactParticles,1f);
            }
            Destroy(gameObject);
            
            
            
        }
    }

    protected virtual void Move()
    {
        Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveOffset);
    }

}
