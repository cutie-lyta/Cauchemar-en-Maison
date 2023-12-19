using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public event Action<GameObject, Collision> OnCollisionEnterEvent;
    public event Action<GameObject, Collision> OnCollisionStayEvent;
    public GameObject Socket;

    private List<Color> color = new ();

    public bool Exited = false;
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
        Exited = false;
        print($"OnRayCastEnter : {name}");
        foreach(Material material in this.GetComponentInChildren<Renderer>().materials){
            color.Add(material.color);
            material.color += new Color(0.5f, 0.50f, 0.50f);
        }
    }

    public void OnRayCastExit()
    {
        Exited = true;
        print($"OnRayCastExit : {name}");
        foreach(Material material in this.GetComponentInChildren<Renderer>().materials){
            material.color = color[GetComponentInChildren<Renderer>().materials.ToList().IndexOf(material)];
        }
    }

}
