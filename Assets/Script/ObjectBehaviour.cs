using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public event Action<GameObject, Collision> OnCollisionEnterEvent;
    public GameObject Socket;
    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(gameObject, other);
    }
}
