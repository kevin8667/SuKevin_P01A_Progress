using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] Camera _cam1, _cam2;

    // Start is called before the first frame update
    void Start()
    {

        _cam1.enabled = true;
        _cam2.enabled = false;

    }


    public void SwitchCamera() 
    {
        _cam1.enabled = !_cam1.enabled;
        _cam2.enabled = !_cam2.enabled;
    }
}
