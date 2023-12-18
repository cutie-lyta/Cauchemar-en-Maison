using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionner : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycast;
    [SerializeField] private List<Objet> _objets;
    [SerializeField] private GameObject _poofPrefabs;
    [SerializeField] private GameObject _pffPrefabs;

    [SerializeField] private SceneLoader _loader;

    private void ReplaceObject(GameObject gameObject)
    {
        Objet searchedObjet = new Objet();

        foreach (Objet objet in _objets)
        {
            if(objet.pointer == gameObject)
            {

                GameObject pff = Instantiate(_pffPrefabs);
                pff.transform.position = gameObject.transform.position;

                gameObject.transform.position = objet.transform.position;

                GameObject poof = Instantiate(_poofPrefabs);
                poof.transform.position = objet.transform.position;

                searchedObjet = objet;
                break;
            }
        }

        _objets.Remove(searchedObjet);

        CheckFinish();
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

    private void CheckFinish()
    {
        Debug.Log("Check");
        if (_objets.Count < 1)
        {
            _loader.LoadTheScene("TheEnd");
        }
    }
}
