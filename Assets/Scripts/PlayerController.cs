using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = .12f;
    [SerializeField] float turnSpeed = .3f;

    Rigidbody _rb = null;



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
     

    }

    void Move()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * moveSpeed;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        _rb.MovePosition(_rb.position + moveDirection); ;
    }
    void MoveHorizontal()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Horizontal") * moveSpeed;
        Vector3 moveDirection = transform.right * moveAmountThisFrame;
        _rb.MovePosition(_rb.position + moveDirection); ;
    }
}
