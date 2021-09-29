using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   [SerializeField] Material _objectMaterial;
    Color _objectColor;
    bool _damaged = false;
    // Start is called before the first frame update

    private void Awake()
    {
        
        _objectColor = _objectMaterial.color;
    }

    public virtual void Damaged()
    {
        if(_damaged != true && gameObject.activeInHierarchy == true)
        {
            StartCoroutine(DamageSequence());
        }
    }

    //material flashing effect when the boss is damaged
    protected virtual IEnumerator DamageSequence()
    {
        //set boolean for detecting lockout
        _damaged = true;

        _objectMaterial.color = Color.red;
        Debug.Log(gameObject.name + " is damaged.");

        //wait for the required dduration
        yield return new WaitForSeconds(0.2f);

        _objectMaterial.color = _objectColor;
        Debug.Log(gameObject.name + " is recovered.");

        //set boolean to release lockout
        _damaged = false;

    }
}
