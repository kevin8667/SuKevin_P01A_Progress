using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private Text _playerHP, _bossHP;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private EnemyHealth _bossHealth;

    Health newhealth;
    EnemyHealth newEnemyHealth;

    private void Awake()
    {
       newhealth = _playerHealth.GetComponent<Health>();
       newEnemyHealth = _bossHealth.GetComponent<EnemyHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerHP.text = newhealth.CurrentHealth.ToString();
        _bossHP.text = newEnemyHealth.CurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHP.text = newhealth.CurrentHealth.ToString();
        _bossHP.text = newEnemyHealth.CurrentHealth.ToString();
    }

}
