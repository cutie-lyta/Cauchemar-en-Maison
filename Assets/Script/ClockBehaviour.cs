using System.Collections;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField] private Transform _pointer;
    [SerializeField] private float _timeBeforeTurn = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_timerExecute());
    }

    IEnumerator _timerExecute()
    {
        if (!_isPaused)
        {
            _pointer.rotation = Quaternion.Euler(0, 0, _pointer.rotation.eulerAngles.z - 1);
            if (_pointer.rotation.eulerAngles.z <= 0.05f)
            {
                BroadcastMessage("TimerFinished");
                yield return null;
            }
        }
        yield return new WaitForSeconds(_timeBeforeTurn);
        StartCoroutine(_timerExecute());
    }
}

