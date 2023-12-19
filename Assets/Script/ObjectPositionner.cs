using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionner : MonoBehaviour
{
    [SerializeField] private RaycastBehaviour _raycast;
    [SerializeField] private GameObject _poofPrefabs;
    [SerializeField] private GameObject _pffPrefabs;
    public List<Objet> Objets;
    private IEnumerator ReplaceObjectCoRoutine(GameObject gameObject)
    {
        print($"gameObject : {gameObject.name}");

        foreach (Objet objet in Objets)
        {
            print($"ObjetPointer = {objet.pointer.name}");

            if(objet.pointer == gameObject)
            {
                if(gameObject.GetComponent<Rigidbody>()) gameObject.GetComponent<Rigidbody>().isKinematic = true;

                GameObject pff = Instantiate(_pffPrefabs);
                pff.transform.position = gameObject.transform.position;
                 
                var anim = gameObject.AddComponent<ScaleUpDown>();
                anim.ScaleMax = 75;
                anim.EaseInTime = 8;
                anim.EaseOutTime = 15;
                anim.Play();
                
                print("ntm");

                while (anim.IsFinished != true)
                {
                    yield return new WaitForFixedUpdate();
                }
                
                print("ntm");
                
                gameObject.transform.position = objet.transform.position;
                gameObject.transform.rotation = objet.transform.rotation;

                GameObject poof = Instantiate(_poofPrefabs);
                poof.transform.position = objet.transform.position;

                yield break;
            }
        }
    }

    public void ReplaceObject(GameObject objet)
    {
        StartCoroutine(ReplaceObjectCoRoutine(objet));
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
