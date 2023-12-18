using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionner : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycast;
    [SerializeField] private GameObject _poofPrefabs;
    [SerializeField] private GameObject _pffPrefabs;
    public List<Objet> Objets;
    private void ReplaceObject(GameObject gameObject)
    {
        foreach (Objet objet in Objets)
        {
            if(objet.pointer == gameObject)
            {
                if(gameObject.GetComponent<Rigidbody>()) gameObject.GetComponent<Rigidbody>().isKinematic = true;

                GameObject pff = Instantiate(_pffPrefabs);
                pff.transform.position = gameObject.transform.position;
                
                gameObject.transform.position = objet.transform.position;
                GameObject poof = Instantiate(_poofPrefabs);
                poof.transform.position = objet.transform.position;

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
