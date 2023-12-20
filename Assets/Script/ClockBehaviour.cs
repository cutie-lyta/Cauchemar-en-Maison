using System.Collections;
using UnityEngine;

public class ClockBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    [SerializeField] private float _minutes = 5f; 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_timerExecute());
    }

    IEnumerator _timerExecute()
    {
        
        _pointer.rotation = Quaternion.Euler(0, 0, _pointer.rotation.eulerAngles.z - 1);
        ObjectPositionner.Milliseconds += (ulong)((_minutes * 60 / 360) * 1000);
        
        if (ObjectPositionner.Milliseconds >= (_minutes*60*1000))
        {
            BroadcastMessage("TimerFinished");
            yield return null;
        }

        yield return new WaitForSeconds(_minutes*60 / 360);
        StartCoroutine(_timerExecute());
    }
}

