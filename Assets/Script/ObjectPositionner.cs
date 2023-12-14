using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionner : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycast;
    [SerializeField] private List<Objet> _objets;

    private void ReplaceObject(GameObject gameObject)
    {
        foreach (Objet objet in _objets)
        {
            if(objet.pointer == gameObject)
            {
                gameObject.transform.position = objet.transform.position;
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _raycast.OnHit += ReplaceObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
