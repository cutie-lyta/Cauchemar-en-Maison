using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ClockBehaviour _clockBehaviour;
    [SerializeField] private MovementBehaviour _movement;
    [SerializeField] private CameraMovementBehaviour _cameraMovement;

    [Header("Time (in seconds)")]
    [SerializeField] private float _loadingTime;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        _movement.enabled = false;
        _cameraMovement.enabled = false;

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
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        _movement.enabled = true;
        _cameraMovement.enabled = true;

        _clockBehaviour.TimerStart();
    }    
}
