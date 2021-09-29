using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health _health = other.gameObject.GetComponent<Health>();
            if (_health != null)
            {
                _health.Kill();
            }

        }
    }
}
