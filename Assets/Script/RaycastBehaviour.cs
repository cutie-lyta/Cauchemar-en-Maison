using System;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    [SerializeField] private int _objDistance = 12;
    public event Action<GameObject> OnHit;

    private GameObject _currentlyCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // si on touche un objet en raycast (en pointant tout droit avec le forward)
        // on précise qu'il a été pointé (pour le test : à redéfinir)
        RaycastHit hit; // variable (pour l'instant vide) faisant office de pointeur afin de plus tard récupérer la data sur les objets touchés

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _objDistance))
        {
            bool pressed = Input.GetKeyDown("joystick button 0");
            pressed = pressed || Input.GetKeyDown("joystick button 6");
            /*int i = 1;
            while (i < 16 && !pressed)
            {
                pressed = pressed || Input.GetKeyDown($"joystick {i + 1} button 0");
                i++;
            }*/
            
            if (Input.GetMouseButtonDown(0) || pressed) { // on prend l'input de la commande du joueur (si le joueur fait un click gauche souris)
                
                Debug.Log($"Did hit {hit.collider.gameObject.tag}");
                Debug.Log($"Tag = {hit.collider.gameObject.tag}, : {hit.collider.gameObject.tag == "Displaced"}");
                print("invoking");
                OnHit?.Invoke(hit.collider.gameObject);
            }

            if (_currentlyCollider != hit.collider.gameObject)
            {
                _currentlyCollider?.SendMessage("OnRayCastExit", SendMessageOptions.DontRequireReceiver);
                hit.collider.gameObject.SendMessage("OnRayCastEnter", SendMessageOptions.DontRequireReceiver);
                _currentlyCollider = hit.collider.gameObject;
            }
            
        }
        else if (_currentlyCollider is not null)
        {
            _currentlyCollider.SendMessage("OnRayCastExit", SendMessageOptions.DontRequireReceiver);
            _currentlyCollider = null;
        }
    }
}
