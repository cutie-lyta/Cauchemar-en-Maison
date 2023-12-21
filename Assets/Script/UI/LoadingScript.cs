using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private ClockBehaviour _clockBehaviour;
    [SerializeField] private float _loadingTime;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(_loadingTime);

        _anim.SetTrigger("FadeOut");

        yield return null;
    }

    //Called by the Animator when the fading animation is finished
    private void FadeFinished()
    {
        _clockBehaviour.TimerStart();
    }    
}
