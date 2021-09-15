using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody _rb;
    [SerializeField] protected float _travelSpeed = .25f;

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

    protected virtual void Move()
    {
        Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveOffset);
    }
}
