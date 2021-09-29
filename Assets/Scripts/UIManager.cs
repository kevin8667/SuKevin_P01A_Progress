using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _fx;

    [SerializeField] private Image _playerHealthBarImage;
    [SerializeField] private Image _bossHealthBarImage;


    private void Awake()
    {
        _playerHealthBarImage.fillAmount = 1f;
        _bossHealthBarImage.fillAmount = 1f;

    }

    private void OnEnable()
    {
        Health.Damaged += OnDamaged;
        Health.TakingDamage += OnTakingDamage;
        EnemyHealth.EnemyTakingDamage += OnEnemyTakingDamage;
    }

    private void OnDisable()
    {
        Health.Damaged -= OnDamaged;
        Health.TakingDamage -= OnTakingDamage;
        EnemyHealth.EnemyTakingDamage -= OnEnemyTakingDamage;
    }

    private void OnDamaged()
    {
        StartCoroutine(DamageSequence());
    }

    //changes player's health bar
    private void OnTakingDamage(float amount)
    {
        _playerHealthBarImage.fillAmount -= amount;

        
    }

    //changes boss's health bar
    private void OnEnemyTakingDamage(float amount)
    {
        _bossHealthBarImage.fillAmount -= amount;
    }

    //screen flashing effect when player is damaged
    protected virtual IEnumerator DamageSequence()
    {
        
        _fx.SetActive(true);

        //wait for the required dduration
        yield return new WaitForSeconds(0.05f);

        _fx.SetActive(false);


    }


}
