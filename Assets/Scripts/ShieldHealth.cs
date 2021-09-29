using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : Health , IDamageable
{

    [SerializeField] Material _objectMaterial;
    Color _objectColor;
    private GameObject _boss;

    private void Awake()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");
        _currentHealth = _maxHealth;
        _objectColor = _objectMaterial.color;

    }




    public override void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log(gameObject.name + ": " + _currentHealth);
        if (_damagedSound != null)
        {
            AudioHelper.PlayClip2D(_damagedSound, 1f);
        }

        if (_currentHealth == 2) 
        {
            _objectMaterial.color = new Color(0.5f, 0.3f, 0.4f, 0.3843f);
        }

        if (_currentHealth == 1) 
        {
            _objectMaterial.color = new Color(1f, 0.1f, 0.2f, 0.3843f);

        }
        if (_currentHealth <= 0)
        {
            _objectMaterial.color = _objectColor;
            Kill();
            Boss _newBoss = _boss.gameObject.GetComponent<Boss>();
            _newBoss._isShielded = false;

        }


    }


}
