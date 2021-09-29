using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour , IDamageable
{
    public static event Action Damaged;
    public static event Action<float> TakingDamage;

    [SerializeField] protected AudioClip _killSound;
    [SerializeField] protected AudioClip _damagedSound;
    [SerializeField] protected ParticleSystem _killParticles;
    [SerializeField] protected int _maxHealth;
    protected int _currentHealth;

    public bool _isInvincible = false;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public bool IsInvicinble => _isInvincible;


    // Start is called before the first frame update
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }


    public virtual void TakeDamage(int amount)
    {
        if (_isInvincible == false) 
        {
            _currentHealth -= amount;
            Debug.Log(gameObject.name + ": " + _currentHealth);
            if (_damagedSound != null)
            {
                AudioHelper.PlayClip2D(_damagedSound, 1f);
            }
            if (_currentHealth <= 0)
            {
                Kill();
            }
            Damaged?.Invoke();
            TakingDamage?.Invoke((float)amount / _maxHealth);
            StartCoroutine(InvincibleTimer());
            StartCoroutine(InvincibleFlash());
        }
        
    }

    public virtual void Kill()
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

    private IEnumerator InvincibleTimer()
    {
        _isInvincible = true;

        //wait for the required dduration
        yield return new WaitForSeconds(0.5f);

        _isInvincible = false;

    }

    private IEnumerator InvincibleFlash() 
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.1f);

    }
}
