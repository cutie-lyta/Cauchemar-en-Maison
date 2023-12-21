using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClockBehaviour : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private Transform _pointer;
    [SerializeField] private float _minutes = 5f;

    [Header("Images")]
    [SerializeField] private Image _clock;
    [SerializeField] private Image _flammes1;
    [SerializeField] private Image _flammes2;
    [SerializeField] private Image _flammes3;

    [Header("Sprites")]
    [SerializeField] private Sprite _normalClockSprite;
    [SerializeField] private Sprite _angryClockSprite;

    [Header("Animators")]
    [SerializeField] private Animator _clockAnimator;

    private bool _isAngry = false;

    // Start is called before the first frame update
    void Start()
    {
        this.TimerStart();
    }

    public void TimerStart()
    {
        ObjectPositionner.Milliseconds = 0;
        _clock.sprite = _normalClockSprite;

        _flammes1.gameObject.SetActive(false);
        _flammes2.gameObject.SetActive(false);
        _flammes3.gameObject.SetActive(false);

        StartCoroutine(_timerExecute());
    }

    IEnumerator _timerExecute()
    {
        
        _pointer.rotation = Quaternion.Euler(0, 0, _pointer.rotation.eulerAngles.z - 1);
        ObjectPositionner.Milliseconds += (ulong)((_minutes * 60 / 360) * 1000);

        if (!_isAngry && ObjectPositionner.Milliseconds >= (ulong)(_minutes * 60 * 1000 * 3/4 ))       //Gets Angry once 3/4 of the time is over
        {
            _isAngry = true;
            GetAngry();
        }
        
        if (ObjectPositionner.Milliseconds >= (ulong)(_minutes*60*1000))
        {
            FindObjectOfType<ObjectPositionner>().TimerFinished();
            yield return null;
        }

        yield return new WaitForSeconds(_minutes*60 / 360);
        StartCoroutine(_timerExecute());
    }

    private void GetAngry()
    {
        _clock.sprite = _angryClockSprite;

        _clockAnimator.SetTrigger("GetsAngry");

        _flammes1.gameObject.SetActive(true);
        _flammes2.gameObject.SetActive(true);
        _flammes3.gameObject.SetActive(true);
    }
}
