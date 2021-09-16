using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : Projectile
{
    private void FixedUpdate()
    {
        Move();

    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            
        }
        if (collision.gameObject.tag == "Player")
        {

            if (_impactSound != null)
            {
                AudioHelper.PlayClip2D(_impactSound, 1f);
            }
            _NewImpactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity) as GameObject;
            if (_NewImpactParticles)
            {
                Destroy(_NewImpactParticles, 1f);
            }
            Destroy(gameObject);



        }
    }

    protected override void Move()
    {
        Vector3 moveOffset = transform.forward * _travelSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveOffset);
    }
}
