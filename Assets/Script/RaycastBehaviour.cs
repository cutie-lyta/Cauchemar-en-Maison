using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehaviour : MonoBehaviour
{
    [SerializeField] private int _objDistance = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { // on prend l'input de la commande du joueur (si le joueur fait un click gauche souris)
            RaycastHit hit; // variable (pour l'instant vide) faisant office de pointeur afin de plus tard récupérer la data sur les objets touchés

            // si on touche un objet en raycast (en pointant tout droit avec le forward)
            // on précise qu'il a été pointé (pour le test : à redéfinir)
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.distance <= _objDistance )
                {Debug.Log($"Did hit {hit.collider.name}");}
                
            }

        }

    }
}
