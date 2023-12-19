using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public event Action<GameObject, Collision> OnCollisionEnterEvent;
    public event Action<GameObject, Collision> OnCollisionStayEvent;
    public GameObject Socket;
    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(gameObject, other);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent?.Invoke(gameObject, collision);
    }

}
