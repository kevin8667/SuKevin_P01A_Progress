using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = .12f;

    Rigidbody _rb = null;
    [SerializeField] private GameObject _projectile;
    [SerializeField] protected GameObject _shootParticles;
    [SerializeField] protected AudioClip _shootSound;
    protected GameObject _NewShootParticles;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Move();
        MoveHorizontal();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (_shootSound != null)
            {
                AudioHelper.PlayClip2D(_shootSound, 1f);
            }
            Debug.Log("fire");
            Instantiate(_projectile, transform.position + new Vector3(0,0, 0.5f), Quaternion.identity);
            _NewShootParticles = Instantiate(_shootParticles, transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity) as GameObject;
            if (_NewShootParticles)
            {
                Destroy(_NewShootParticles, 1f);
            }
        }
        
    }

    void Move()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        _rb.MovePosition(_rb.position + moveDirection); ;
    }
    void MoveHorizontal()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        Vector3 moveDirection = transform.right * moveAmountThisFrame;
        _rb.MovePosition(_rb.position + moveDirection); ;
    }


}
