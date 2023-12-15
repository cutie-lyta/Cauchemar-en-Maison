using System.Collections;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour
{
    public bool IsPaused = false;

    [SerializeField] private Transform _pointer;
    [SerializeField] private float _minutes = 5f; 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_timerExecute());
    }

    IEnumerator _timerExecute()
    {
        if (!IsPaused)
        {
            _pointer.rotation = Quaternion.Euler(0, 0, _pointer.rotation.eulerAngles.z - 1);
            if (_pointer.rotation.eulerAngles.z <= 0.05f)
            {
                BroadcastMessage("TimerFinished");
                yield return null;
            }
        }
        yield return new WaitForSeconds(_minutes*60 / 360);
        StartCoroutine(_timerExecute());
    }
}

