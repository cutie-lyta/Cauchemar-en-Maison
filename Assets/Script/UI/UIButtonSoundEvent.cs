using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private AudioClip _enter;
    [SerializeField] private AudioClip _click;
    
    public void OnPointerEnter( PointerEventData ped )
    {
        var i = GetComponent<AudioSource>();
        if (!i)
        {
            i = gameObject.AddComponent<AudioSource>();
        }

        i.clip = _enter;
        i.Play();
    }

    public void OnPointerDown( PointerEventData ped ) {
        var i = GetComponent<AudioSource>();
        if (!i)
        {
            i = gameObject.AddComponent<AudioSource>();
        }

        i.clip = _click;
        i.Play();
    }    
}
