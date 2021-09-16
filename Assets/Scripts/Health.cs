using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour , IDamageable
{
    [SerializeField] protected AudioClip _killSound;
    [SerializeField] protected AudioClip _damagedSound;
    [SerializeField] protected ParticleSystem _killParticles;
    [SerializeField] int _maxHealth = 10;
    int _currentHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }


    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Boss Health: " + _currentHealth);
        if (_damagedSound != null)
        {
            AudioHelper.PlayClip2D(_damagedSound, 1f);
        }
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);

        if(_killParticles != null)
        {
            Instantiate(_killParticles, transform.position, Quaternion.identity);
        }
        if (_killSound != null)
        {
            AudioHelper.PlayClip2D(_killSound, 1f);
        }

    }
}
