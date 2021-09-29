using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{

    Rigidbody _rb;

    [SerializeField] Material _material;
    
    [SerializeField] protected float _movementSpeed = 0.25f;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _projectile2;
    [SerializeField] private GameObject _damageZone;
    [SerializeField] private GameObject _shield;

    [SerializeField] protected GameObject _summonParticles;
    [SerializeField] private AudioClip _summonSound;

    private GameObject _NewSummonParticles;



    public int _numberOfProjectile = 20;
    public float _radius = 5f;

    EnemyHealth _heath;

    private bool _isAttacking = false;
    public bool _isShielded = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _material.color = new Vector4(0, 0.6272721f, 1, 1);
        _heath = gameObject.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation(_rb);
        if (Input.GetKeyDown("q") && _isShielded == false)
        {
            Instantiate(_shield, transform.position, Quaternion.identity);
            _isShielded = true;



        }

        if (_heath.CurrentHealth > _heath.MaxHealth * 0.5f && _isAttacking == false)
        {
            StartCoroutine(AttackPattern_1());
        }
        else if (_heath.CurrentHealth <= _heath.MaxHealth * 0.5f && _isAttacking == false)
        {
            StartCoroutine(AttackPattern_2());
        }

    }

    private void Rotation(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void RingProjectile()
    {
        for (int i = 0; i < _numberOfProjectile; i++)
        {
            float angle = i * Mathf.PI * 2 / _numberOfProjectile;
            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            Vector3 pos = new Vector3(x, 0.3f, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(_projectile, pos, rot);
        }
    }


    private void RingProjectileReverse()
    {
        for (int i = 0; i < _numberOfProjectile; i++)
        {
            float angle = i * Mathf.PI * 2 / _numberOfProjectile;
            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            Vector3 pos = new Vector3(x, 0.3f, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees+180, 0);
            Instantiate(_projectile2, pos, rot);
        }
    }

    private void Summon() 
    {
        Instantiate(_damageZone, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

    }

    private IEnumerator Attack_1()
    {
        RingProjectile();
        
        //wait for the required dduration
        yield return new WaitForSeconds(0.5f);

        RingProjectile();


    }

    private IEnumerator Attack_2()
    {
        RingProjectile();

        //wait for the required dduration
        yield return new WaitForSeconds(0.5f);

        RingProjectileReverse();

        yield return new WaitForSeconds(0.5f);

    }

    private IEnumerator AttackPattern_1()
    {
        _isAttacking = true;

        StartCoroutine(Attack_1());
        
        //wait for the required dduration
        yield return new WaitForSeconds(3f);
        StartCoroutine(SummonAttack());
        yield return new WaitForSeconds(3f);

        _isAttacking = false;
    }

    private IEnumerator AttackPattern_2()
    {
        _isAttacking = true;

        StartCoroutine(Attack_1());

        //wait for the required dduration
        yield return new WaitForSeconds(3f);

        StartCoroutine(Attack_2());

        yield return new WaitForSeconds(3f);

        _isAttacking = false;
    }

    private IEnumerator SummonAttack()
    {
        if (_summonSound != null)
        {
            AudioHelper.PlayClip2D(_summonSound, 1f);
        }
        _NewSummonParticles = Instantiate(_summonParticles, transform.position, Quaternion.identity) as GameObject;
        if (_NewSummonParticles)
        {
            Destroy(_NewSummonParticles, 1f);
        }
        Summon();
        yield return new WaitForSeconds(1f);
        Summon();
        yield return new WaitForSeconds(1f);
        Summon();

    }


    // add sheild below 50%
}
