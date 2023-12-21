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
        var scalePercented1 = ScaleMax * transform.localScale.x / 100;
        print(scalePercented1);
        print(_scaleDefault);
        float byHowManyUp = scalePercented1/EaseInTime;
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

        transform.localScale = new Vector3(_scaleDefault, _scaleDefault, _scaleDefault);
        IsFinished = true;
        yield return null;
    }
}
