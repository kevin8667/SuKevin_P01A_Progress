using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    private GameObject _boss;

    private void Awake()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");

    }


    // Update is called once per frame
    void Update()
    {
        transform.position = _boss.transform.position;
    }
}
