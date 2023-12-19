using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public event Action<GameObject, Collision> OnCollisionEnterEvent;
    public event Action<GameObject, Collision> OnCollisionStayEvent;
    public GameObject Socket;

    private Color color;
    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(gameObject, other);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent?.Invoke(gameObject, collision);
    }

    public void OnRayCastEnter()
    {
        print($"OnRayCastEnter : {name}");
        color = this.GetComponentInChildren<Material>().color;
        this.GetComponent<Material>().color += new Color(50, 50, 50);
    }

    public void OnRayCastExit()
    {
        print($"OnRayCastExit : {name}");
        this.GetComponentInChildren<Material>().color = color;
    }

}
