using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
    public float ScaleMax;

    public int EaseInTime;
    public int EaseOutTime;

    private float _timer;
    private float _scaleDefault;

    public bool IsFinished = true;
    
    public void Play()
    {
        IsFinished = false;
        StartCoroutine(Animate());
    }

    public IEnumerator Animate()
    {
        _scaleDefault = transform.localScale.x;
        
        float byHowManyUp = ScaleMax/EaseInTime;
        while (_timer < EaseInTime)
        {
            transform.localScale += new Vector3(byHowManyUp, byHowManyUp, byHowManyUp);
            _timer += 1;
            
            yield return new WaitForFixedUpdate();
        }

        _timer = 0;
        
        float byHowManyDown = (-transform.localScale.x)/EaseOutTime;
        while (_timer < EaseOutTime)
        {
            transform.localScale = transform.localScale + new Vector3(byHowManyDown, byHowManyDown, byHowManyDown);
            _timer += 1;
            yield return new WaitForFixedUpdate();
        }

        IsFinished = true;
        transform.localScale = new Vector3(_scaleDefault, _scaleDefault, _scaleDefault);
        yield return null;
    }
}
