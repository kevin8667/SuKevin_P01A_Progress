using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{

    Rigidbody _rb;

    [SerializeField] Material _material;
    
    [SerializeField] protected float _movementSpeed = 0.25f;

    [SerializeField] private GameObject _projectile;
    public int _numberOfObject = 20;
    public float _radius = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _material.color = new Vector4(0, 0.6272721f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation(_rb);
        if (Input.GetKeyDown("q"))
        {
            RingProjectile();
        }

    }

    private void Rotation(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void RingProjectile()
    {
        for (int i = 0; i < _numberOfObject; i++)
        {
            float angle = i * Mathf.PI * 2 / _numberOfObject;
            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            Vector3 pos = new Vector3(x, 0.3f, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(_projectile, pos, rot);
        }
    }

}
