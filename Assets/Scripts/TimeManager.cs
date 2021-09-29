using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float _slowDownFactor = 0.05f;
    public float _slowdownLength = 2f;

    //return normal
    private void Update()
    {
        Time.timeScale += (1f / _slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    //slow down
    public void SlowDown()
    {
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
