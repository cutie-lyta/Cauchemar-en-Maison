using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField] private Transform pointer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_timerExecute());
    }

    IEnumerator _timerExecute()
    {
        if (!_isPaused)
        {
            pointer.rotation = Quaternion.Euler(0, 0, pointer.rotation.eulerAngles.z - 1);
            if (pointer.rotation.eulerAngles.z <= 0.05f)
            {
                BroadcastMessage("TimerFinished");
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(_timerExecute());
    }
}

