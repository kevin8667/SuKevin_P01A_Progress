using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = .12f;

    Rigidbody _rb = null;
    public GameObject projectile;


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
            Debug.Log("fire");
            Instantiate(projectile, transform.position + new Vector3(0,0, 0.5f), Quaternion.identity);
        }

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
