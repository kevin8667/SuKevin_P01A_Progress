using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = .12f;
    private float _moveSpeedSlow;
    private float _moveSpeedTemp;

    Rigidbody _rb = null;
    
    [SerializeField] private GameObject _projectile;
    [SerializeField] protected GameObject _shootParticles;
    [SerializeField] protected AudioClip _shootSound;
    
    protected GameObject _NewShootParticles;

    [SerializeField] TimeManager _timeManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeedSlow = _moveSpeed / 2;
        _moveSpeedTemp = _moveSpeed;


    }

    private void FixedUpdate()
    {
        
        
        
    }

    // add invincible time for player

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (_shootSound != null)
            {
                AudioHelper.PlayClip2D(_shootSound, 1f);
            }
            Instantiate(_projectile, transform.position + new Vector3(0,0, 0.5f), Quaternion.identity);
            _NewShootParticles = Instantiate(_shootParticles, transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity) as GameObject;
            if (_NewShootParticles)
            {
                Destroy(_NewShootParticles, 1f);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

            _moveSpeed = _moveSpeedSlow;

        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeedTemp;
        }

        if (Input.GetKeyDown(KeyCode.RightShift)) 
        {
            _timeManager.SlowDown();
        }
        Move();
        MoveHorizontal();
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
